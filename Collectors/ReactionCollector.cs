using DCM.Events.Discord;
using DCM.Interfaces;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DCM.Collectors
{
    /// <summary>
    /// An observer for the <see cref="IMessage"/>. 
    /// It hooks the event when a <see cref="SocketReaction"/> was added to the <see cref="IMessage"/>
    /// 
    /// Instantiate this class by invoking the Extension Method for the <see cref="IMessage"/>.
    /// </summary>
    public class ReactionCollector : IDisposable // TODO: Implement Factory -> ICollector<IMessage, ReactionAddedEvent>
    {
        private readonly IEventEmitter _eventEmitter;
        private readonly IMessage _message;
        private readonly CancellationTokenSource _listenerCts = new();

        // TODO: Try getting the EventEmitter Instance on some better, other way.
        public ReactionCollector(IMessage message, IEventEmitter eventEmitter)
        {
            _eventEmitter = eventEmitter;
            _message = message;

            _eventEmitter.AddListener<ReactionAddedEvent>(OnReactionAdded);
        }

        public List<Func<SocketReaction, bool>> Filters { get; } = new();
        public List<Action<SocketReaction>> Listeners { get; } = new();
        public List<(Func<SocketReaction, Task> listener, bool awaitRequested)> AsyncListeners { get; } = new();
        public event Action<SocketReaction> ReactionAdded;

        public ReactionCollector WithFilter(Func<SocketReaction, bool> filterPredicate)
        {
            Filters.Add(filterPredicate ?? throw new ArgumentNullException(nameof(filterPredicate)));
            return this;
        }

        public ReactionCollector AddListener(Action<SocketReaction> listener)
        {
            Listeners.Add(listener ?? throw new ArgumentNullException(nameof(listener)));
            return this;
        }

        public ReactionCollector AddListener(Func<SocketReaction, Task> listener, bool awaitTask = true)
        {
            AsyncListeners.Add((listener ?? throw new ArgumentNullException(nameof(listener)), awaitTask));
            return this;
        }

        private void OnReactionAdded(ReactionAddedEvent eventArgs)
        {
            if (eventArgs.Message.Id != _message.Id) return;
            if (Filters.Any(filter => filter.Invoke(eventArgs.Reaction) == false)) return;

            ReactionAdded?.Invoke(eventArgs.Reaction);
            foreach (var listener in Listeners)
                listener.Invoke(eventArgs.Reaction);

            foreach (var (listener, awaitRequested) in AsyncListeners)
            {
                try
                {
                    var task = listener.Invoke(eventArgs.Reaction);
                    if (awaitRequested)
                        task.Wait(_listenerCts.Token);
                    else
                        Task.Run(async () => await task, _listenerCts.Token);
                } 
                catch (TaskCanceledException) { }
                catch (OperationCanceledException) { }
            }
        }

        public void Dispose()
        {
            _eventEmitter.RemoveListener<ReactionAddedEvent>(OnReactionAdded);
            _listenerCts.Cancel();
            _listenerCts.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

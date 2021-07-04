using DCM.Events.Discord;
using DCM.Interfaces;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // TODO: Try getting the EventEmitter Instance on some better, other way.
        public ReactionCollector(IEventEmitter eventEmitter, IMessage message)
        {
            _eventEmitter = eventEmitter;
            _message = message;

            _eventEmitter.AddListener<ReactionAddedEvent>(ReactionAddedEventHandler);
        }

        public List<Func<SocketReaction, bool>> Filters;
        public event Action<SocketReaction> ReactionAdded;

        public ReactionCollector WithFilter(Func<SocketReaction, bool> filterPredicate)
        {
            if (filterPredicate is not null) 
                Filters.Add(filterPredicate);

            return this;
        }

        // TODO: ReactionCollector OnReactionAdded(Action<SocketReaction> listener)
        // TODO: ReactionCollector OnReactionAdded(Func<SocketReaction, Task> listener)
        // TODO: Task<SocketReaction> WaitForReaction()

        private void ReactionAddedEventHandler(ReactionAddedEvent eventArgs)
        {
            if (eventArgs.Message.Id != _message.Id) return;
            if (Filters.Any(filter => filter?.Invoke(eventArgs.Reaction) ?? true == false)) return;

            ReactionAdded?.Invoke(eventArgs.Reaction);
        }

        public void Dispose()
        {
            _eventEmitter.RemoveListener<ReactionAddedEvent>(ReactionAddedEventHandler);
            GC.SuppressFinalize(this);
        }
    }
}

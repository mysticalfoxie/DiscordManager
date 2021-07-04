using DCM.Events.Discord;
using DCM.Interfaces;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCM.Collectors
{
    /// <summary>
    /// An observer for the <see cref="ISocketMessageChannel"/>. 
    /// It hooks the event when a <see cref="SocketReaction"/> was added to the <see cref="ISocketMessageChannel"/>
    /// 
    /// Instantiate this class by invoking the Extension Method for the <see cref="ISocketMessageChannel"/>.
    /// </summary>
    public class MessageCollector : IDisposable // TODO: Implement Factory -> ICollector<ISocketMessageChannel, MessageReceivedEvent>
    {
        private readonly IEventEmitter _eventEmitter;
        private readonly ISocketMessageChannel _channel;

        // TODO: Try getting the EventEmitter Instance on some better, other way.
        public MessageCollector(IEventEmitter eventEmitter, ISocketMessageChannel channel)
        {
            _eventEmitter = eventEmitter;
            _channel = channel;

            _eventEmitter.AddListener<MessageReceivedEvent>(MessageReceivedEventHandler);
        }

        public List<Func<SocketMessage, bool>> Filters;
        public event Action<SocketMessage> MessageReceived;

        public MessageCollector Where(Func<SocketMessage, bool> filterPredicate)
        {
            if (filterPredicate is not null) 
                Filters.Add(filterPredicate);

            return this;
        }

        // TODO: MessageCollector OnMessageReceived(Action<SocketMessage> listener)
        // TODO: MessageCollector OnMessageReceived(Func<SocketMessage, Task> listener)
        // TODO: Task<SocketMessage> WaitForMessage()

        private void MessageReceivedEventHandler(MessageReceivedEvent eventArgs)
        {
            if (eventArgs.Message.Channel.Id != _channel.Id) return;
            if (Filters.Any(filter => filter?.Invoke(eventArgs.Message) ?? true == false)) return;

            MessageReceived?.Invoke(eventArgs.Message);
        }

        public void Dispose()
        {
            _eventEmitter.RemoveListener<MessageReceivedEvent>(MessageReceivedEventHandler);
            GC.SuppressFinalize(this);
        }
    }
}

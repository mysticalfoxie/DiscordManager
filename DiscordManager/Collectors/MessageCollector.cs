using DCM.Events.Discord;
using DCM.Interfaces;
using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace DCM.Collectors
{
    /// <summary>
    /// An observer for the <see cref="IMessageChannel"/>. 
    /// It hooks the event when a <see cref="SocketMessage"/> was added to the <see cref="IMessageChannel"/>
    /// 
    /// Instantiate this class by invoking the Extension Method for the <see cref="IMessageChannel"/>.
    /// </summary>
    public class MessageCollector : CollectorBase<IMessageChannel, SocketMessage>, IDisposable
    {
        private readonly IMessageChannel _channel;
        private readonly IEventAggregator _eventEmitter;
        private readonly Subscription _subscription;

        public MessageCollector(IMessageChannel channel, IEventAggregator eventEmitter)
        {
            _channel = channel;
            _eventEmitter = eventEmitter;

            _subscription = _eventEmitter.Subscribe<MessageReceivedEvent>(OnMessageReceived);
        }

        private void OnMessageReceived(MessageReceivedEvent eventArgs)
            => OnEventEmitted(eventArgs.Message);

        protected override void OnEventEmitted(SocketMessage eventArgs)
        {
            if (eventArgs.Channel.Id != _channel.Id) return;

            base.OnEventEmitted(eventArgs);
        }

        // Overrides required for the return type of MessageCollector
        public override MessageCollector AddListener(Action<SocketMessage> listener)
        {
            base.AddListener(listener);
            return this;
        }

        public override MessageCollector AddListener(Func<SocketMessage, Task> listener, bool awaitListener = false)
        {
            base.AddListener(listener, awaitListener);
            return this;
        }

        public override MessageCollector WithFilter(Func<SocketMessage, bool> filterPredicate)
        {
            base.WithFilter(filterPredicate);
            return this;
        }

        public new void Dispose()
        {
            _subscription.Unsubscribe();
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

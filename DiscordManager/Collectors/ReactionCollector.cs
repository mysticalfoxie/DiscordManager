﻿using DCM.Events.Discord;
using DCM.Interfaces;
using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace DCM.Collectors
{
    /// <summary>
    /// An observer for the <see cref="IMessage"/>. 
    /// It hooks the event when a <see cref="SocketReaction"/> was added to the <see cref="IMessage"/>
    /// 
    /// Instantiate this class by invoking the Extension Method for the <see cref="IMessage"/>.
    /// </summary>
    public class ReactionCollector : CollectorBase<IMessage, SocketReaction>, IDisposable
    {
        private readonly IMessage _message;
        private readonly IEventAggregator _eventAggregator; 
        private readonly Subscription<ReactionAddedEvent> _subscription;

        public ReactionCollector(
            IMessage message, 
            IEventAggregator eventAggregator)
        {
            _message = message;
            _eventAggregator = eventAggregator;

            _subscription = _eventAggregator.Subscribe<ReactionAddedEvent>(OnReactionAdded);
        }

        private void OnReactionAdded(ReactionAddedEvent eventArgs)
            => OnEventEmitted(eventArgs.Reaction);

        protected override void OnEventEmitted(SocketReaction eventArgs)
        {
            if (eventArgs.MessageId != _message.Id) return;

            base.OnEventEmitted(eventArgs);
        }

        // Overrides required for the return type of ReactionCollector
        public override ReactionCollector AddListener(Action<SocketReaction> listener)
        {
            base.AddListener(listener);
            return this;
        }

        public override ReactionCollector AddListener(Func<SocketReaction, Task> listener, bool awaitListener = false)
        {
            base.AddListener(listener, awaitListener);
            return this;
        }

        public override ReactionCollector WithFilter(Func<SocketReaction, bool> filterPredicate)
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

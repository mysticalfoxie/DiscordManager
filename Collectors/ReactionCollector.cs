using DCM.Events.Discord;
using DCM.Interfaces;
using Discord;
using System;

namespace DCM.Collectors
{
    public class ReactionCollector : ICollector<IMessage, ReactionAddedEvent>, IDisposable
    {
        private readonly DiscordManager _client;
        private readonly IMessage _message;
        private readonly Func<ReactionAddedEvent, bool> _predicate;

        public ReactionCollector(IMessage message)
        {
            _client = client;
            _message = message;
            _predicate = predicate;

            _client.EventEmitter.AddListener<ReactionAddedEvent>(OnReactionAdded);
        }

        public event Action<ReactionAddedEvent> ReactionAdded;

        private void OnReactionAdded(ReactionAddedEvent eventArgs)
        {
            if (eventArgs.Message.Id != _message.Id) return;
            if (!_predicate?.Invoke(eventArgs) ?? false) return;
            ReactionAdded?.Invoke(eventArgs);
        }

        public void Dispose()
        {
            _client.EventEmitter.RemoveListener<ReactionAddedEvent>(OnReactionAdded);
            GC.SuppressFinalize(this);
        }
    }
}

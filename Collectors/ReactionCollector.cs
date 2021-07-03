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
    public class ReactionCollector : ICollector<IMessage, ReactionAddedEvent>, IDisposable
    {
        private readonly IMessage _message;
        private readonly DiscordSocketClient _discordClient;

        public ReactionCollector(IMessage message, DiscordSocketClient discordClient)
        {
            _message = message;
            _discordClient = discordClient;

            _discordClient.ReactionAdded += OnReactionAdded;
        }

        public List<Func<SocketReaction, bool>> Filters;
        public event Action<SocketReaction> ReactionAdded;

        private Task OnReactionAdded(
            Cacheable<IUserMessage, ulong> message, 
            ISocketMessageChannel channel,
            SocketReaction reaction)
        {
            if (message.Id != _message.Id) return Task.CompletedTask;
            if (Filters.Any(filter => filter?.Invoke(reaction) ?? true == false)) return Task.CompletedTask;

            ReactionAdded?.Invoke(reaction);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _discordClient.ReactionAdded -= OnReactionAdded;
            GC.SuppressFinalize(this);
        }
    }
}

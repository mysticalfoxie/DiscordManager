using DCM.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ReactionAddedEvent : IEvent
    {
        public ReactionAddedEvent(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            Message = message;
            Channel = channel;
            Reaction = reaction;
        }

        public Cacheable<IUserMessage, ulong> Message { get; }
        public ISocketMessageChannel Channel { get; }
        public SocketReaction Reaction { get; }
    }
}

using DCM.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ReactionAddedEvent : Event
    {
        public ReactionAddedEvent(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
        {
            Message = message;
            Channel = channel;
            Reaction = reaction;
        }

        public Cacheable<IUserMessage, ulong> Message { get; }
        public Cacheable<IMessageChannel, ulong> Channel { get; }
        public SocketReaction Reaction { get; }
    }
}

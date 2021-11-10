using DCM.Interfaces;
using Discord;

namespace DCM.Events.Discord
{
    public class ReactionsClearedEvent : IEvent
    {
        public ReactionsClearedEvent(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel)
        {
            Message = message;
            Channel = channel;
        }

        public Cacheable<IUserMessage, ulong> Message { get; }
        public Cacheable<IMessageChannel, ulong> Channel { get; }
    }
}

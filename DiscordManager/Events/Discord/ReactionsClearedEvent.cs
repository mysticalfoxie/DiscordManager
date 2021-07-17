using DCM.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ReactionsClearedEvent : IEvent
    {
        public ReactionsClearedEvent(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel)
        {
            Message = message;
            Channel = channel;
        }

        public Cacheable<IUserMessage, ulong> Message { get; }
        public ISocketMessageChannel Channel { get; }
    }
}

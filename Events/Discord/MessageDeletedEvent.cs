using DCM.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class MessageDeletedEvent : IEvent
    {
        public MessageDeletedEvent(Cacheable<IMessage, ulong> message, ISocketMessageChannel channel)
        {
            Message = message;
            Channel = channel;
        }

        public Cacheable<IMessage, ulong> Message { get; }
        public ISocketMessageChannel Channel { get; }
    }
}

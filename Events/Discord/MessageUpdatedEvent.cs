using DCM.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class MessageUpdatedEvent : IEvent
    {
        public MessageUpdatedEvent(Cacheable<IMessage, ulong> oldMessage, SocketMessage newMessage, ISocketMessageChannel channel)
        {
            OldMessage = oldMessage;
            NewMessage = newMessage;
            Channel = channel;
        }

        public Cacheable<IMessage, ulong> OldMessage { get; }
        public SocketMessage NewMessage { get; }
        public ISocketMessageChannel Channel { get; }
    }
}

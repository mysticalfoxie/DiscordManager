using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ChannelCreatedEvent : IEvent
    {
        public ChannelCreatedEvent(SocketChannel channel)
        {
            Channel = channel;
        }

        public SocketChannel Channel { get; }
    }
}

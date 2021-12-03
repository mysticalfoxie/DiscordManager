using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ChannelCreatedEvent : Event
    {
        public ChannelCreatedEvent(SocketChannel channel)
        {
            Channel = channel;
        }

        public SocketChannel Channel { get; }
    }
}

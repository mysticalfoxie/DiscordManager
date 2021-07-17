using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ChannelDestroyedEvent : IEvent
    {
        public ChannelDestroyedEvent(SocketChannel channel)
        {
            Channel = channel;
        }

        public SocketChannel Channel { get; }
    }
}

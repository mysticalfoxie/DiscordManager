using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    internal class ChannelCreatedEvent : IEvent
    {
        public ChannelCreatedEvent(SocketChannel channel)
        {
            Channel = channel;
        }

        public SocketChannel Channel { get; }
    }
}

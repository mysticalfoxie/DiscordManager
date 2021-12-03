using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ChannelUpdatedEvent : Event
    {
        public ChannelUpdatedEvent(SocketChannel oldChannel, SocketChannel newChannel)
        {
            OldChannel = oldChannel;
            NewChannel = newChannel;
        }

        public SocketChannel OldChannel { get; }
        public SocketChannel NewChannel { get; }
    }
}

using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ChannelUpdatedEvent : IEvent
{
    public ChannelUpdatedEvent(SocketChannel oldChannel, SocketChannel newChannel)
    {
        OldChannel = oldChannel;
        NewChannel = newChannel;
    }

    public SocketChannel OldChannel { get; }
    public SocketChannel NewChannel { get; }
}
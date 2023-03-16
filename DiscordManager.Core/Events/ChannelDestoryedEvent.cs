using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ChannelDestroyedEvent : IEvent
{
    public ChannelDestroyedEvent(SocketChannel channel)
    {
        Channel = channel;
    }

    public SocketChannel Channel { get; }
}
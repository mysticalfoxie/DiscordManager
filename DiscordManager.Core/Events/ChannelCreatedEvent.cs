using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ChannelCreatedEvent : IEvent
{
    public ChannelCreatedEvent(SocketChannel channel)
    {
        Channel = channel;
    }

    public SocketChannel Channel { get; }
}
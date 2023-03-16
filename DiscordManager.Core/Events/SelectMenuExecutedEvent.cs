using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class SelectMenuExecutedEvent : IEvent
{
    public SelectMenuExecutedEvent(SocketMessageComponent component)
    {
        Component = component;
    }

    public SocketMessageComponent Component { get; }
}
using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ButtonExecutedEvent : IEvent
{
    public ButtonExecutedEvent(SocketMessageComponent component)
    {
        Component = component;
    }

    public SocketMessageComponent Component { get; }
}
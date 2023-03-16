using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class RoleCreatedEvent : IEvent
{
    public RoleCreatedEvent(SocketRole role)
    {
        Role = role;
    }

    public SocketRole Role { get; }
}
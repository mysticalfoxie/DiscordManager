using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class RoleDeletedEvent : IEvent
{
    public RoleDeletedEvent(SocketRole role)
    {
        Role = role;
    }

    public SocketRole Role { get; }
}
using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class RoleUpdatedEvent : IEvent
{
    public RoleUpdatedEvent(SocketRole oldRole, SocketRole newRole)
    {
        OldRole = oldRole;
        NewRole = newRole;
    }

    public SocketRole OldRole { get; }
    public SocketRole NewRole { get; }
}
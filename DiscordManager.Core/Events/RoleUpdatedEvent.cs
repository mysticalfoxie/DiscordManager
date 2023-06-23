using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class RoleUpdatedEvent : IEvent
{
    public SocketRole OldRole { get; set; }
    public SocketRole NewRole { get; set; }
}
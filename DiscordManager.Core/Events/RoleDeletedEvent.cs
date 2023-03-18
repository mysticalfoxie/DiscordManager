using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class RoleDeletedEvent : IEvent
{
    public SocketRole Role { get; set; }
}
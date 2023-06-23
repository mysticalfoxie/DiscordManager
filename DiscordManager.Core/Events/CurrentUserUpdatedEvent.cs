using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class CurrentUserUpdatedEvent : IEvent
{
    public SocketSelfUser OldSelfUser { get; set; }
    public SocketSelfUser NewSelfUser { get; set; }
}
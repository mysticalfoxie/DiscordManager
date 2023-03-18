using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class PresenceUpdatedEvent : IEvent
{
    public SocketUser User { get; set; }
    public SocketPresence OldPresence { get; set; }
    public SocketPresence NewPresence { get; set; }
}
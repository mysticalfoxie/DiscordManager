using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class UserUpdatedEvent : IEvent
{
    public SocketUser OldUser { get; set; }
    public SocketUser NewUser { get; set; }
}
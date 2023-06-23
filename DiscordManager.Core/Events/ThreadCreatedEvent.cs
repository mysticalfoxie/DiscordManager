using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class ThreadCreatedEvent : IEvent
{
    public SocketThreadChannel Channel { get; set; }
}
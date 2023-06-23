using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class ApplicationCommandCreatedEvent : IEvent
{
    public SocketApplicationCommand Command { get; set; }
}
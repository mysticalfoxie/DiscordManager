using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class ApplicationCommandDeletedEvent : IEvent
{
    public SocketApplicationCommand Command { get; set; }
}
using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class InteractionCreatedEvent : IEvent
{
    public SocketInteraction Interaction { get; set; }
}
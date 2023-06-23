using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class ModalSubmittedEvent : IEvent
{
    public SocketModal Modal { get; set; }
}
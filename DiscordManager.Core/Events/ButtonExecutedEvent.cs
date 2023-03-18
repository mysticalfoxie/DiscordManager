using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class ButtonExecutedEvent : IEvent
{
    public SocketMessageComponent Component { get; set; }
}
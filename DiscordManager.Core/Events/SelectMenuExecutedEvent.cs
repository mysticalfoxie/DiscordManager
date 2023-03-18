using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class SelectMenuExecutedEvent : IEvent
{
    public SocketMessageComponent Component { get; set; }
}
using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class MessageReceivedEvent : IEvent
{
    public SocketMessage Message { get; set; }
}
using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class MessageCommandExecutedEvent : IEvent
{
    public SocketMessageCommand Command { get; set; }
}
using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class UserCommandExecutedEvent : IEvent
{
    public SocketUserCommand Command { get; set; }
}
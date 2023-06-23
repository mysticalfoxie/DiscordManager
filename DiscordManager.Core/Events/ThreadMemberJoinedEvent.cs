using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class ThreadMemberJoinedEvent : IEvent
{
    public SocketThreadUser User { get; set; }
}
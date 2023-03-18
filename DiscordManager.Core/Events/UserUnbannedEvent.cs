using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class UserUnbannedEvent : IEvent
{
    public SocketUser User { get; set; }
    public SocketGuild Guild { get; set; }
}
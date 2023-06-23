using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class UserBannedEvent : IEvent
{
    public SocketUser User { get; set; }
    public SocketGuild Guild { get; set; }
}
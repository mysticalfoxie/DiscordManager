using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class UserLeftEvent : IEvent
{
    public SocketGuild Guild { get; set; }
    public SocketUser User { get; set; }
}
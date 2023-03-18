using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class JoinedGuildEvent : IEvent
{
    public SocketGuild Guild { get; set; }
}
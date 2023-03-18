using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class LeftGuildEvent : IEvent
{
    public SocketGuild Guild { get; set; }
}
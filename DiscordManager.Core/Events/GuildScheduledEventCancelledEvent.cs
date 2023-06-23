using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class GuildScheduledEventCancelledEvent : IEvent
{
    public SocketGuildEvent GuildEvent { get; set; }
}
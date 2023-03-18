using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class GuildScheduledEventCreatedEvent : IEvent
{
    public SocketGuildEvent GuildEvent { get; set; }
}
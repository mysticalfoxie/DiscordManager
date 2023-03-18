using DCM.Core.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class GuildScheduledEventUpdatedEvent : IEvent
{
    public Cacheable<SocketGuildEvent, ulong> OldGuildEvent { get; set; }
    public SocketGuildEvent NewGuildEvent { get; set; }
}
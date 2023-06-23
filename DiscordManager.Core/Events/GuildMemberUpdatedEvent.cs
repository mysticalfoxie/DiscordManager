using DCM.Core.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class GuildMemberUpdatedEvent : IEvent
{
    public Cacheable<SocketGuildUser, ulong> OldGuildUser { get; set; }
    public SocketGuildUser NewGuildUser { get; set; }
}
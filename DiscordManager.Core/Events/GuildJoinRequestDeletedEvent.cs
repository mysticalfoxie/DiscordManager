using DCM.Core.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class GuildJoinRequestDeletedEvent : IEvent
{
    public Cacheable<SocketGuildUser, ulong> GuildUser { get; set; }
    public SocketGuild Guild { get; set; }
}
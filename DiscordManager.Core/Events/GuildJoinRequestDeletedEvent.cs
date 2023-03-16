using Discord;
using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class GuildJoinRequestDeletedEvent : IEvent
{
    public GuildJoinRequestDeletedEvent(Cacheable<SocketGuildUser, ulong> user, SocketGuild guild)
    {
        User = user;
        Guild = guild;
    }

    public Cacheable<SocketGuildUser, ulong> User { get; }
    public SocketGuild Guild { get; }
}
using Discord;
using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class GuildMemberUpdatedEvent : IEvent
{
    public GuildMemberUpdatedEvent(Cacheable<SocketGuildUser, ulong> oldUser, SocketGuildUser newUser)
    {
        OldUser = oldUser;
        NewUser = newUser;
    }

    public Cacheable<SocketGuildUser, ulong> OldUser { get; }
    public SocketGuildUser NewUser { get; }
}
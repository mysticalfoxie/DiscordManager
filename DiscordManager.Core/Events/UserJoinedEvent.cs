using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class UserJoinedEvent : IEvent
{
    public UserJoinedEvent(SocketGuildUser guildUser)
    {
        GuildUser = guildUser;
    }

    public SocketGuildUser GuildUser { get; }
}
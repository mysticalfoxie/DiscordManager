using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class UserBannedEvent : IEvent
{
    public UserBannedEvent(SocketUser user, SocketGuild guild)
    {
        User = user;
        Guild = guild;
    }

    public SocketUser User { get; }
    public SocketGuild Guild { get; }
}
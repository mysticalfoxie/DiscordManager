using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class UserLeftEvent : IEvent
{
    public UserLeftEvent(SocketGuild guild, SocketUser user)
    {
        User = user;
        Guild = guild;
    }

    public SocketUser User { get; }

    public SocketGuild Guild { get; }
}
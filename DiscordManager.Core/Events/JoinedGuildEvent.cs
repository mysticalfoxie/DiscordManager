using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class JoinedGuildEvent : IEvent
{
    public JoinedGuildEvent(SocketGuild guild)
    {
        Guild = guild;
    }

    public SocketGuild Guild { get; }
}
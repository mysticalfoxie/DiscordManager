using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class GuildUnavailableEvent : IEvent
{
    public GuildUnavailableEvent(SocketGuild guild)
    {
        Guild = guild;
    }

    public SocketGuild Guild { get; }
}
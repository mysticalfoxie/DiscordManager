using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class GuildAvailableEvent : IEvent
{
    public GuildAvailableEvent(SocketGuild guild)
    {
        Guild = guild;
    }

    public SocketGuild Guild { get; }
}
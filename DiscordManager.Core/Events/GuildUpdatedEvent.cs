using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class GuildUpdatedEvent : IEvent
{
    public GuildUpdatedEvent(SocketGuild oldGuild, SocketGuild newGuild)
    {
        OldGuild = oldGuild;
        NewGuild = newGuild;
    }

    public SocketGuild OldGuild { get; }
    public SocketGuild NewGuild { get; }
}
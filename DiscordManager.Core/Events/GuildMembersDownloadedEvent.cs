using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class GuildMembersDownloadedEvent : IEvent
{
    public GuildMembersDownloadedEvent(SocketGuild guild)
    {
        Guild = guild;
    }

    public SocketGuild Guild { get; }
}
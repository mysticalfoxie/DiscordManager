using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class LeftGuildEvent : IEvent
{
    public LeftGuildEvent(SocketGuild guild)
    {
        Guild = guild;
    }

    public SocketGuild Guild { get; }
}
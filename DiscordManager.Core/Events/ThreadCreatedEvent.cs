using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ThreadCreatedEvent : IEvent
{
    public ThreadCreatedEvent(SocketThreadChannel thread)
    {
        Thread = thread;
    }

    public SocketThreadChannel Thread { get; }
}
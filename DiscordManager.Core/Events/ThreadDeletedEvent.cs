using Discord;
using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ThreadDeletedEvent : IEvent
{
    public ThreadDeletedEvent(Cacheable<SocketThreadChannel, ulong> thread)
    {
        Thread = thread;
    }

    public Cacheable<SocketThreadChannel, ulong> Thread { get; }
}
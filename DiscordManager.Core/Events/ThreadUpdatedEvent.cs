using Discord;
using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ThreadUpdatedEvent : IEvent
{
    public ThreadUpdatedEvent(Cacheable<SocketThreadChannel, ulong> oldThread, SocketThreadChannel newThread)
    {
        OldThread = oldThread;
        NewThread = newThread;
    }

    public Cacheable<SocketThreadChannel, ulong> OldThread { get; }
    public SocketThreadChannel NewThread { get; }
}
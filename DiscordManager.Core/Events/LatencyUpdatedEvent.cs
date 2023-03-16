using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class LatencyUpdatedEvent : IEvent
{
    public LatencyUpdatedEvent(int oldLatency, int newLatency)
    {
        OldLatency = oldLatency;
        NewLatency = newLatency;
    }

    public int OldLatency { get; }
    public int NewLatency { get; }
}
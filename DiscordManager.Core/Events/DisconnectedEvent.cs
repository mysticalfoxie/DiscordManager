using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class DisconnectedEvent : IEvent
{
    public DisconnectedEvent(Exception exception)
    {
        Exception = exception;
    }

    public Exception Exception { get; }
}
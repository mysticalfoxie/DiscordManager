using Discord;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class LogEvent : IEvent
{
    public LogEvent(LogMessage message)
    {
        Message = message;
    }

    public LogMessage Message { get; }
}
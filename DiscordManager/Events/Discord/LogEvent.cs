using DCM.Interfaces;
using Discord;

namespace DCM.Events.Discord
{
    public class LogEvent : IEvent
    {
        public LogEvent(LogMessage message)
        {
            Message = message;
        }

        public LogMessage Message { get; }
    }
}

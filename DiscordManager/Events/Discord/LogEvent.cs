using DCM.Interfaces;
using Discord;

namespace DCM.Events.Discord
{
    public class LogEvent : Event
    {
        public LogEvent(LogMessage message)
        {
            Message = message;
        }

        public LogMessage Message { get; }
    }
}

using System;

namespace DCM.Events.Logging
{
    public class WarningEvent : Event
    {
        public WarningEvent(string message, string stackTrace = null)
        {
            Message = message;
            StackTrace = stackTrace ?? Environment.StackTrace;
        }

        public string Message { get; }
        public string StackTrace { get; }

        public static implicit operator WarningEvent(string message) => new(message);
    }
}

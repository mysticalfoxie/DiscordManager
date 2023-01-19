using System;

namespace DCM.Events.Logging
{
    public class InfoEvent : Event
    {
        public InfoEvent(string message, string stackTrace = null)
        {
            Message = message;
            StackTrace = stackTrace ?? Environment.StackTrace;
        }

        public string Message { get; }
        public string StackTrace { get; }

        public static implicit operator InfoEvent(string message) => new(message);
    }
}

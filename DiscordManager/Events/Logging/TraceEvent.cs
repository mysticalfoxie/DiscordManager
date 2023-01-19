using System;

namespace DCM.Events.Logging
{
    public class TraceEvent : Event
    {
        public TraceEvent(string message, string stackTrace = null)
        {
            Message = message;
            StackTrace = stackTrace ?? Environment.StackTrace;
        }

        public string Message { get; }
        public string StackTrace { get; }

        public static implicit operator TraceEvent(string message) => new(message);
    }
}

using DCM.Interfaces;
using System;

namespace DCM.Events.Logging
{
    public class ErrorEvent : IEvent
    {
        public ErrorEvent(Exception error, string stackTrace = null)
        {
            Exception = error;
            StackTrace = stackTrace ?? Environment.StackTrace;
        }

        public Exception Exception { get; }
        public string StackTrace { get; }
    }
}

using System;

namespace DCM.Events.Logging
{
    public class ErrorEvent : Event
    {
        public ErrorEvent(Exception error, string stackTrace = null)
        {
            Exception = error;
            StackTrace = stackTrace ?? Environment.StackTrace;
        }

        public Exception Exception { get; }
        public string StackTrace { get; }

        public override string ToString()
        {
            return Exception.ToString();
        }

        public static implicit operator ErrorEvent(string errorMessage) => new(new(errorMessage));
        public static implicit operator ErrorEvent(Exception exception) => new(exception);
    }
}

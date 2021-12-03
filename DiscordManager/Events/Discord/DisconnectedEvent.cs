using DCM.Interfaces;
using System;

namespace DCM.Events.Discord
{
    public class DisconnectedEvent : Event
    {
        public DisconnectedEvent(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; }
    }
}

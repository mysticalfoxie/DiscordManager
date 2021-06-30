using DCM.Interfaces;
using System;

namespace DCM.Events.Discord
{
    public class DisconnectedEvent : IEvent
    {
        public DisconnectedEvent(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; }
    }
}

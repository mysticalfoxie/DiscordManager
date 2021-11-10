using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ThreadCreatedEvent : IEvent
    {
        public ThreadCreatedEvent(SocketThreadChannel thread)
        {
            Thread = thread;
        }

        public SocketThreadChannel Thread { get; }
    }
}

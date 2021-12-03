using DCM.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ThreadDeletedEvent : Event
    {
        public ThreadDeletedEvent(Cacheable<SocketThreadChannel, ulong> thread)
        {
            Thread = thread;
        }

        public Cacheable<SocketThreadChannel, ulong> Thread { get; }
    }
}

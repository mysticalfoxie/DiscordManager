using DCM.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ThreadUpdatedEvent : Event
    {
        public ThreadUpdatedEvent(Cacheable<SocketThreadChannel, ulong> oldThread, SocketThreadChannel newThread)
        {
            OldThread = oldThread;
            NewThread = newThread;
        }

        public Cacheable<SocketThreadChannel, ulong> OldThread { get; }
        public SocketThreadChannel NewThread { get; }
    }
}

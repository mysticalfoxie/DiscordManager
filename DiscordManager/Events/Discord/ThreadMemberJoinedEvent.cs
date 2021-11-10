using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ThreadMemberJoinedEvent : IEvent
    {
        public ThreadMemberJoinedEvent(SocketThreadUser user)
        {
            User = user;
        }

        public SocketThreadUser User { get; }
    }
}

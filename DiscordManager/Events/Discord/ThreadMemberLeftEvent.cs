using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ThreadMemberLeftEvent : IEvent
    {
        public ThreadMemberLeftEvent(SocketThreadUser user)
        {
            User = user;
        }

        public SocketThreadUser User { get; }
    }
}

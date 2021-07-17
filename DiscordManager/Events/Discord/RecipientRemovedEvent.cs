using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class RecipientRemovedEvent : IEvent
    {
        public RecipientRemovedEvent(SocketGroupUser user)
        {
            User = user;
        }

        public SocketGroupUser User { get; }
    }
}

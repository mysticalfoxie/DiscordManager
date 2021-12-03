using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class RoleDeletedEvent : Event
    {
        public RoleDeletedEvent(SocketRole role)
        {
            Role = role;
        }

        public SocketRole Role { get; }
    }
}

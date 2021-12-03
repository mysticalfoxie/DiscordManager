using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class RoleCreatedEvent : Event
    {
        public RoleCreatedEvent(SocketRole role)
        {
            Role = role;
        }

        public SocketRole Role { get; }
    }
}

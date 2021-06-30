using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class RoleCreatedEvent : IEvent
    {
        public RoleCreatedEvent(SocketRole role)
        {
            Role = role;
        }

        public SocketRole Role { get; }
    }
}

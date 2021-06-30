using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class RoleUpdatedEvent : IEvent
    {
        public RoleUpdatedEvent(SocketRole oldRole, SocketRole newRole)
        {
            OldRole = oldRole;
            NewRole = newRole;
        }

        public SocketRole OldRole { get; }
        public SocketRole NewRole { get; }
    }
}

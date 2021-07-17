using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class InviteCreatedEvent : IEvent
    {
        public InviteCreatedEvent(SocketInvite invite)
        {
            Invite = invite;
        }

        public SocketInvite Invite { get; }
    }
}

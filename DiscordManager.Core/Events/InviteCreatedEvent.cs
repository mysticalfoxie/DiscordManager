using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class InviteCreatedEvent : IEvent
{
    public SocketInvite Invite { get; set; }
}
using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class InviteCreatedEvent : IEvent
{
    public InviteCreatedEvent(SocketInvite invite)
    {
        Invite = invite;
    }

    public SocketInvite Invite { get; }
}
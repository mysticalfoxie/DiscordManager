using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class InviteDeletedEvent : IEvent
{
    public SocketGuildChannel GuildChannel { get; set; }
    public string Invite { get; set; }
}
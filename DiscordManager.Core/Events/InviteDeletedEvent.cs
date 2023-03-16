using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class InviteDeletedEvent : IEvent
{
    public InviteDeletedEvent(SocketChannel originChannel, string inviteCode)
    {
        OriginChannel = originChannel;
        InviteCode = inviteCode;
    }

    public SocketChannel OriginChannel { get; }
    public string InviteCode { get; }
}
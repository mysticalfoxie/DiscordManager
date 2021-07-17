using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
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
}

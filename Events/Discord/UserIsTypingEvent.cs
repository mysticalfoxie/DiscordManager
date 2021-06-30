using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class UserIsTypingEvent : IEvent
    {
        public UserIsTypingEvent(SocketUser user, ISocketMessageChannel channel)
        {
            User = user;
            Channel = channel;
        }

        public SocketUser User { get; }
        public ISocketMessageChannel Channel { get; }
    }
}

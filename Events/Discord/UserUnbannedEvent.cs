using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class UserUnbannedEvent : IEvent
    {
        public UserUnbannedEvent(SocketUser user, SocketGuild guild)
        {
            User = user;
            Guild = guild;
        }

        public SocketUser User { get; }
        public SocketGuild Guild { get; }
    }
}

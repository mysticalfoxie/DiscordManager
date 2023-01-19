using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class UserLeftEvent : Event
    {
        public UserLeftEvent(SocketGuild guild, SocketUser user)
        {
            User = user;
            Guild = guild;
        }

        public SocketUser User { get; }

        public SocketGuild Guild { get; }
    }
}

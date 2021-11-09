using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class UserLeftEvent : IEvent
    {
        public UserLeftEvent(SocketGuildUser guildUser)
        {
            GuildUser = guildUser;
        }

        public SocketGuildUser GuildUser { get; }
    }
}

using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class UserJoinedEvent : IEvent
    {
        public UserJoinedEvent(SocketGuildUser guildUser)
        {
            GuildUser = guildUser;
        }

        public SocketGuildUser GuildUser { get; }
    }
}

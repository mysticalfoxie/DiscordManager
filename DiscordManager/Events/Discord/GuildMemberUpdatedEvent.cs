using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class GuildMemberUpdatedEvent : IEvent
    {
        public GuildMemberUpdatedEvent(SocketGuildUser oldUser, SocketGuildUser newUser)
        {
            OldUser = oldUser;
            NewUser = newUser;
        }

        public SocketGuildUser OldUser { get; }
        public SocketGuildUser NewUser { get; }
    }
}

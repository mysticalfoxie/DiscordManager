using DCM.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class GuildMemberUpdatedEvent : IEvent
    {
        public GuildMemberUpdatedEvent(Cacheable<SocketGuildUser, ulong> oldUser, SocketGuildUser newUser)
        {
            OldUser = oldUser;
            NewUser = newUser;
        }

        public Cacheable<SocketGuildUser, ulong> OldUser { get; }
        public SocketGuildUser NewUser { get; }
    }
}

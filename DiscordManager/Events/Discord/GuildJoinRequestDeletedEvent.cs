using DCM.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class GuildJoinRequestDeletedEvent : IEvent
    {
        public GuildJoinRequestDeletedEvent(Cacheable<SocketGuildUser, ulong> user, SocketGuild guild)
        {
            User = user;
            Guild = guild;
        }

        public Cacheable<SocketGuildUser, ulong> User { get; }
        public SocketGuild Guild { get; }
    }
}

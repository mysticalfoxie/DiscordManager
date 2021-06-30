using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class GuildUpdatedEvent : IEvent
    {
        public GuildUpdatedEvent(SocketGuild oldGuild, SocketGuild newGuild)
        {
            OldGuild = oldGuild;
            NewGuild = newGuild;
        }

        public SocketGuild OldGuild { get; }
        public SocketGuild NewGuild { get; }
    }
}

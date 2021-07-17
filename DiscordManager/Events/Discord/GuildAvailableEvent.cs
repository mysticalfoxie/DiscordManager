using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class GuildAvailableEvent : IEvent
    {
        public GuildAvailableEvent(SocketGuild guild)
        {
            Guild = guild;
        }

        public SocketGuild Guild { get; }
    }
}

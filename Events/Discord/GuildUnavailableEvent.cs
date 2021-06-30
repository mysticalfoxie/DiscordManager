using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class GuildUnavailableEvent : IEvent
    {
        public GuildUnavailableEvent(SocketGuild guild)
        {
            Guild = guild;
        }

        public SocketGuild Guild { get; }
    }
}

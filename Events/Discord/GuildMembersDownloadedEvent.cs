using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class GuildMembersDownloadedEvent : IEvent
    {
        public GuildMembersDownloadedEvent(SocketGuild guild)
        {
            Guild = guild;
        }

        public SocketGuild Guild { get; }
    }
}

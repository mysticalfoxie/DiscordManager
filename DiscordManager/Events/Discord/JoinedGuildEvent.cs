using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class JoinedGuildEvent : Event
    {
        public JoinedGuildEvent(SocketGuild guild)
        {
            Guild = guild;
        }

        public SocketGuild Guild { get; }
    }
}

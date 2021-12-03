using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class LeftGuildEvent : Event
    {
        public LeftGuildEvent(SocketGuild guild)
        {
            Guild = guild;
        }

        public SocketGuild Guild { get; }
    }
}

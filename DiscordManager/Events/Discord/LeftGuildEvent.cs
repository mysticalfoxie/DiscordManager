using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class LeftGuildEvent : IEvent
    {
        public LeftGuildEvent(SocketGuild guild)
        {
            Guild = guild;
        }

        public SocketGuild Guild { get; }
    }
}

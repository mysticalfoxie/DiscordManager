using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class UserLeftEvent : IEvent
    {
        public UserLeftEvent(SocketGuildUser guild)
        {
            Guild = guild;
        }

        public SocketGuildUser Guild { get; }
    }
}

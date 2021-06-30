using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class UserJoinedEvent : IEvent
    {
        public UserJoinedEvent(SocketGuildUser guild)
        {
            Guild = guild;
        }

        public SocketGuildUser Guild { get; }
    }
}

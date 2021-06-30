using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class UserUpdatedEvent : IEvent
    {
        public UserUpdatedEvent(SocketUser oldUser, SocketUser newUser)
        {
            OldUser = oldUser;
            NewUser = newUser;
        }

        public SocketUser OldUser { get; }
        public SocketUser NewUser { get; }
    }
}

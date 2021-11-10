using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class UserCommandExecutedEvent : IEvent
    {
        public UserCommandExecutedEvent(SocketUserCommand command)
        {
            Command = command;
        }

        public SocketUserCommand Command { get; }
    }
}

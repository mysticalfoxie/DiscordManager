using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class MessageCommandExecutedEvent : Event
    {
        public MessageCommandExecutedEvent(SocketMessageCommand command)
        {
            Command = command;
        }

        public SocketMessageCommand Command { get; }
    }
}

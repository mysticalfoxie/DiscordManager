using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class MessageReceivedEvent : IEvent
    {
        public MessageReceivedEvent(SocketMessage message)
        {
            Message = message;
        }

        public SocketMessage Message { get; }
    }
}

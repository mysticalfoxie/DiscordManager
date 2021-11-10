using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ApplicationCommandUpdatedEvent : IEvent
    {
        public ApplicationCommandUpdatedEvent(SocketApplicationCommand command)
        {
            Command = command;
        }

        public SocketApplicationCommand Command { get; set; }
    }
}
using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ApplicationCommandDeletedEvent : Event
    {
        public ApplicationCommandDeletedEvent(SocketApplicationCommand command)
        {
            Command = command;
        }

        public SocketApplicationCommand Command { get; }
    }
}

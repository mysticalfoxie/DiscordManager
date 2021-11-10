using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ApplicationCommandCreatedEvent : IEvent
    {
        public ApplicationCommandCreatedEvent(SocketApplicationCommand appCommand)
        {
            AppCommand = appCommand;
        }

        public SocketApplicationCommand AppCommand { get; set; }
    }
}
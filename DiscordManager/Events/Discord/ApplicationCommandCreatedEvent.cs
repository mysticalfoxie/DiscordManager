using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ApplicationCommandCreatedEvent : Event
    {
        public ApplicationCommandCreatedEvent(SocketApplicationCommand appCommand)
        {
            AppCommand = appCommand;
        }

        public SocketApplicationCommand AppCommand { get; set; }
    }
}
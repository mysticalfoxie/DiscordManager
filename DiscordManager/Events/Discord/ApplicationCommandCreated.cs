using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ApplicationCommandCreated : Event
    {
        public ApplicationCommandCreated(SocketApplicationCommand appCommand)
        {
            AppCommand = appCommand;
        }

        public SocketApplicationCommand AppCommand { get; set; }
    }
}
using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class SlashCommandExecutedEvent : IEvent
    {
        public SlashCommandExecutedEvent(SocketSlashCommand command)
        {
            Command = command;
        }

        public SocketSlashCommand Command { get; }
    }
}

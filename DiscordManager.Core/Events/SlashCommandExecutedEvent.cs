using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class SlashCommandExecutedEvent : IEvent
{
    public SocketSlashCommand Command { get; set; }
}
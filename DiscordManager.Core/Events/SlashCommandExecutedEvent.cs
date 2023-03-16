using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class SlashCommandExecutedEvent : IEvent
{
    public SlashCommandExecutedEvent(SocketSlashCommand command)
    {
        Command = command;
    }

    public SocketSlashCommand Command { get; }
}
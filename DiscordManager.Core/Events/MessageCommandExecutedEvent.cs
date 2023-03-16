using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class MessageCommandExecutedEvent : IEvent
{
    public MessageCommandExecutedEvent(SocketMessageCommand command)
    {
        Command = command;
    }

    public SocketMessageCommand Command { get; }
}
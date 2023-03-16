using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class UserCommandExecutedEvent : IEvent
{
    public UserCommandExecutedEvent(SocketUserCommand command)
    {
        Command = command;
    }

    public SocketUserCommand Command { get; }
}
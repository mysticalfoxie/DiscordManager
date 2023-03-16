using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ApplicationCommandDeletedEvent : IEvent
{
    public ApplicationCommandDeletedEvent(SocketApplicationCommand command)
    {
        Command = command;
    }

    public SocketApplicationCommand Command { get; }
}
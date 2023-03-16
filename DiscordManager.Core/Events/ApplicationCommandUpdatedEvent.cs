using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ApplicationCommandUpdatedEvent : IEvent
{
    public ApplicationCommandUpdatedEvent(SocketApplicationCommand command)
    {
        Command = command;
    }

    public SocketApplicationCommand Command { get; set; }
}
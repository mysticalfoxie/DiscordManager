using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ApplicationCommandCreatedEvent : IEvent
{
    public ApplicationCommandCreatedEvent(SocketApplicationCommand appCommand)
    {
        AppCommand = appCommand;
    }

    public SocketApplicationCommand AppCommand { get; }
}
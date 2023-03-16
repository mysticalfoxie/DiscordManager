using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ApplicationCommandCreated : IEvent
{
    public ApplicationCommandCreated(SocketApplicationCommand appCommand)
    {
        AppCommand = appCommand;
    }

    public SocketApplicationCommand AppCommand { get; set; }
}
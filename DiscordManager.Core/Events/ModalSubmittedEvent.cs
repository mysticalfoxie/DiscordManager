using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ModalSubmittedEvent : IEvent
{
    public ModalSubmittedEvent(SocketModal modal)
    {
        Modal = modal;
    }

    public SocketModal Modal { get; }
}
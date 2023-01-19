using Discord.WebSocket;

namespace DCM.Events.Discord;

public class ModalSubmittedEvent : Event
{
    public ModalSubmittedEvent(SocketModal modal)
    {
        Modal = modal;
    }

    public SocketModal Modal { get; }
}

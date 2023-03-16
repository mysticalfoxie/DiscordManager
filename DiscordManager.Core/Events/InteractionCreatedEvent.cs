using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class InteractionCreatedEvent : IEvent
{
    public InteractionCreatedEvent(SocketInteraction interaction)
    {
        Interaction = interaction;
    }

    public SocketInteraction Interaction { get; }
}
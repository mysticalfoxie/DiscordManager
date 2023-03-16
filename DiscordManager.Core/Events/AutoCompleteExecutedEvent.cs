using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class AutoCompleteExecutedEvent : IEvent
{
    public AutoCompleteExecutedEvent(SocketAutocompleteInteraction interaction)
    {
        Interaction = interaction;
    }

    public SocketAutocompleteInteraction Interaction { get; }
}
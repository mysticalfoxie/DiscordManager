using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class AutocompleteExecutedEvent : IEvent
{
    public SocketAutocompleteInteraction Interaction { get; set; }
}
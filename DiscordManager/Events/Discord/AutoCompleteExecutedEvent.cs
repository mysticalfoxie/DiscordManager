using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class AutoCompleteExecutedEvent : Event
    {
        public AutoCompleteExecutedEvent(SocketAutocompleteInteraction interaction)
        {
            Interaction = interaction;
        }

        public SocketAutocompleteInteraction Interaction { get; }
    }
}

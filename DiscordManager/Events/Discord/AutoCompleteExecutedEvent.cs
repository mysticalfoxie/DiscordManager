using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class AutoCompleteExecutedEvent : IEvent
    {
        public AutoCompleteExecutedEvent(SocketAutocompleteInteraction interaction)
        {
            Interaction = interaction;
        }

        public SocketAutocompleteInteraction Interaction { get; }
    }
}

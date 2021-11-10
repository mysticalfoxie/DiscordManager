using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class InteractionCreatedEvent : IEvent
    {
        public InteractionCreatedEvent(SocketInteraction interaction)
        {
            Interaction = interaction;
        }

        public SocketInteraction Interaction { get; }
    }
}

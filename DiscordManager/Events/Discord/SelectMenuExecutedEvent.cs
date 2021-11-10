using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class SelectMenuExecutedEvent : IEvent
    {
        public SelectMenuExecutedEvent(SocketMessageComponent component)
        {
            Component = component;
        }

        public SocketMessageComponent Component { get; }
    }
}

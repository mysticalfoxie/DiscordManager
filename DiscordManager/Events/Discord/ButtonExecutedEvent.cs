using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class ButtonExecutedEvent : Event
    {
        public ButtonExecutedEvent(SocketMessageComponent component)
        {
            Component = component;
        }

        public SocketMessageComponent Component { get; }
    }
}

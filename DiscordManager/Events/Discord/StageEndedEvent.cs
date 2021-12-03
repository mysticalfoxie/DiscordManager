using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class StageEndedEvent : Event
    {
        public StageEndedEvent(SocketStageChannel channel)
        {
            Channel = channel;
        }

        public SocketStageChannel Channel { get; }
    }
}

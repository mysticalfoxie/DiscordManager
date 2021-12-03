using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class StageStartedEvent : Event
    {
        public StageStartedEvent(SocketStageChannel channel)
        {
            Channel = channel;
        }

        public SocketStageChannel Channel { get; }
    }
}

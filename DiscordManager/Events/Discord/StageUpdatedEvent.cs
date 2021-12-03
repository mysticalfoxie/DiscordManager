using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class StageUpdatedEvent : Event
    {
        public StageUpdatedEvent(SocketStageChannel oldChannel, SocketStageChannel newChannel)
        {
            OldChannel = oldChannel;
            NewChannel = newChannel;
        }

        public SocketStageChannel OldChannel { get; }
        public SocketStageChannel NewChannel { get; }
    }
}

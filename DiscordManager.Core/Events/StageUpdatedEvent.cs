using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class StageUpdatedEvent : IEvent
{
    public SocketStageChannel OldStageChannel { get; set; }
    public SocketStageChannel NewStageChannel { get; set; }
}
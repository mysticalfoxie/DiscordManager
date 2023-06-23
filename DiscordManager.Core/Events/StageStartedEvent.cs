using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class StageStartedEvent : IEvent
{
    public SocketStageChannel Channel { get; set; }
}
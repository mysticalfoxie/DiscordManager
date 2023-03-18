using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class SpeakerAddedEvent : IEvent
{
    public SocketStageChannel Channel { get; set; }
    public SocketGuildUser User { get; set; }
}
using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class VoiceServerUpdatedEvent : IEvent
{
    public SocketVoiceServer VoiceServer { get; set; }
}
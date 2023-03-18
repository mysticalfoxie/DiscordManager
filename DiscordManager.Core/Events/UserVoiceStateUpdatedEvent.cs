using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class UserVoiceStateUpdatedEvent : IEvent
{
    public SocketUser User { get; set; }
    public SocketVoiceState OldVoiceState { get; set; }
    public SocketVoiceState NewVoiceState { get; set; }
}
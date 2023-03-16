using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class UserVoiceStateUpdatedEvent : IEvent
{
    public UserVoiceStateUpdatedEvent(SocketUser user, SocketVoiceState oldState, SocketVoiceState newState)
    {
        User = user;
        OldState = oldState;
        NewState = newState;
    }

    public SocketUser User { get; }
    public SocketVoiceState OldState { get; }
    public SocketVoiceState NewState { get; }
}
using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
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
}

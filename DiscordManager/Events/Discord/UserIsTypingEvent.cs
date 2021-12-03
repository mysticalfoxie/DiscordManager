using DCM.Interfaces;
using Discord;

namespace DCM.Events.Discord
{
    public class UserIsTypingEvent : Event
    {
        public UserIsTypingEvent(Cacheable<IUser, ulong> user, Cacheable<IMessageChannel, ulong> channel)
        {
            User = user;
            Channel = channel;
        }

        public Cacheable<IUser, ulong> User { get; }
        public Cacheable<IMessageChannel, ulong> Channel { get; }
    }
}

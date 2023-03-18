using DCM.Core.Interfaces;
using Discord;

namespace DCM.Core.Events;

public class UserIsTypingEvent : IEvent
{
    public Cacheable<IUser, ulong> User { get; set; }
    public Cacheable<IMessageChannel, ulong> Channel { get; set; }
}
using Discord;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class UserIsTypingEvent : IEvent
{
    public UserIsTypingEvent(Cacheable<IUser, ulong> user, Cacheable<IMessageChannel, ulong> channel)
    {
        User = user;
        Channel = channel;
    }

    public Cacheable<IUser, ulong> User { get; }
    public Cacheable<IMessageChannel, ulong> Channel { get; }
}
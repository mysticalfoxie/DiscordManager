using Discord;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ReactionsClearedEvent : IEvent
{
    public ReactionsClearedEvent(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel)
    {
        Message = message;
        Channel = channel;
    }

    public Cacheable<IUserMessage, ulong> Message { get; }
    public Cacheable<IMessageChannel, ulong> Channel { get; }
}
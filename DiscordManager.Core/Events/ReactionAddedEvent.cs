using Discord;
using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class ReactionAddedEvent : IEvent
{
    public ReactionAddedEvent(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel,
        SocketReaction reaction)
    {
        Message = message;
        Channel = channel;
        Reaction = reaction;
    }

    public Cacheable<IUserMessage, ulong> Message { get; }
    public Cacheable<IMessageChannel, ulong> Channel { get; }
    public SocketReaction Reaction { get; }
}
using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class SpeakerAddedEvent : IEvent
{
    public SpeakerAddedEvent(SocketStageChannel channel, SocketGuildUser user)
    {
        Channel = channel;
        User = user;
    }

    public SocketStageChannel Channel { get; }
    public SocketGuildUser User { get; }
}
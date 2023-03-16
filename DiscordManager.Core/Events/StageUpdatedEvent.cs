using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class StageUpdatedEvent : IEvent
{
    public StageUpdatedEvent(SocketStageChannel oldChannel, SocketStageChannel newChannel)
    {
        OldChannel = oldChannel;
        NewChannel = newChannel;
    }

    public SocketStageChannel OldChannel { get; }
    public SocketStageChannel NewChannel { get; }
}
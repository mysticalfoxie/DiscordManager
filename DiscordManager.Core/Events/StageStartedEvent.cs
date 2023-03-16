using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class StageStartedEvent : IEvent
{
    public StageStartedEvent(SocketStageChannel channel)
    {
        Channel = channel;
    }

    public SocketStageChannel Channel { get; }
}
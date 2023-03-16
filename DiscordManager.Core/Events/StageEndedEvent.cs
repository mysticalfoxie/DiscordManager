using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class StageEndedEvent : IEvent
{
    public StageEndedEvent(SocketStageChannel channel)
    {
        Channel = channel;
    }

    public SocketStageChannel Channel { get; }
}
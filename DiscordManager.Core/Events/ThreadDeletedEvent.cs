using DCM.Core.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class ThreadDeletedEvent : IEvent
{
    public Cacheable<SocketThreadChannel, ulong> ThreadChannel { get; set; }
}
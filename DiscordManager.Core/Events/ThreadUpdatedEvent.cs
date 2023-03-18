using DCM.Core.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class ThreadUpdatedEvent : IEvent
{
    public Cacheable<SocketThreadChannel, ulong> OldThreadChannel { get; set; }
    public SocketThreadChannel NewThreadChannel { get; set; }
}
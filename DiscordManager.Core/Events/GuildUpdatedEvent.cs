using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class GuildUpdatedEvent : IEvent
{
    public SocketGuild OldGuild { get; set; }
    public SocketGuild NewGuild { get; set; }
}
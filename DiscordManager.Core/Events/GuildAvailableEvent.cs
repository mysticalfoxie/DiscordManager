using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class GuildAvailableEvent : IEvent
{
    public SocketGuild Guild { get; set; }
}
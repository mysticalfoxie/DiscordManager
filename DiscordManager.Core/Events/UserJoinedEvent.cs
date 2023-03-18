using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class UserJoinedEvent : IEvent
{
    public SocketGuildUser GuildUser { get; set; }
}
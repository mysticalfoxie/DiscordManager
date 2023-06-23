using DCM.Core.Interfaces;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class GuildScheduledEventUserAddEvent : IEvent
{
    public Cacheable<SocketUser, RestUser, IUser, ulong> User { get; set; }
    public SocketGuildEvent GuildEvent { get; set; }
}
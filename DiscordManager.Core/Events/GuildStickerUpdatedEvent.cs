using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class GuildStickerUpdatedEvent : IEvent
{
    public SocketCustomSticker OldSticker { get; set; }
    public SocketCustomSticker NewSticker { get; set; }
}
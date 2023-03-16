using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class GuildStickerUpdatedEvent : IEvent
{
    public GuildStickerUpdatedEvent(SocketCustomSticker oldSticker, SocketCustomSticker newSticker)
    {
        OldSticker = oldSticker;
        NewSticker = newSticker;
    }

    public SocketCustomSticker OldSticker { get; }
    public SocketCustomSticker NewSticker { get; }
}
using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class GuildStickerDeletedEvent : IEvent
{
    public GuildStickerDeletedEvent(SocketCustomSticker sticker)
    {
        Sticker = sticker;
    }

    public SocketCustomSticker Sticker { get; }
}
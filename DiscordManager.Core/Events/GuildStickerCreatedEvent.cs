using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Events;

public class GuildStickerCreatedEvent : IEvent
{
    public GuildStickerCreatedEvent(SocketCustomSticker sticker)
    {
        Sticker = sticker;
    }

    public SocketCustomSticker Sticker { get; }
}
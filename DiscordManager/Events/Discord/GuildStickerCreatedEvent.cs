using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class GuildStickerCreatedEvent : Event
    {
        public GuildStickerCreatedEvent(SocketCustomSticker sticker)
        {
            Sticker = sticker;
        }

        public SocketCustomSticker Sticker { get; }
    }
}

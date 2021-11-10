using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class GuildStickerDeletedEvent : IEvent
    {
        public GuildStickerDeletedEvent(SocketCustomSticker sticker)
        {
            Sticker = sticker;
        }

        public SocketCustomSticker Sticker { get; }
    }
}

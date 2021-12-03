using DCM.Interfaces;
using Discord.WebSocket;

namespace DCM.Events.Discord
{
    public class GuildStickerUpdatedEvent : Event
    {
        public GuildStickerUpdatedEvent(SocketCustomSticker oldSticker, SocketCustomSticker newSticker)
        {
            OldSticker = oldSticker;
            NewSticker = newSticker;
        }

        public SocketCustomSticker OldSticker { get; }
        public SocketCustomSticker NewSticker { get; }
    }
}

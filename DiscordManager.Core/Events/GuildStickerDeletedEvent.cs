using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Events;

public class GuildStickerDeletedEvent : IEvent
{
    public SocketCustomSticker Sticker { get; set; }
}
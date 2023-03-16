using Discord.WebSocket;

namespace DiscordManager.Core.Interfaces;

public interface IDiscordConfigService
{
    DiscordSocketConfig Config { get; }
}
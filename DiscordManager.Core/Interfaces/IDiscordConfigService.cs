using Discord.WebSocket;

namespace DCM.Core.Interfaces;

public interface IDiscordConfigService
{
    DiscordSocketConfig Config { get; }
}
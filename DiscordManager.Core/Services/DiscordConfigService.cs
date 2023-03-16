using Discord.WebSocket;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Services;

public class DiscordConfigService : IDiscordConfigService
{
    public DiscordSocketConfig Config { get; } = new();
}
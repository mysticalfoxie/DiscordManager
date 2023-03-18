using DCM.Core.Interfaces;
using Discord.WebSocket;

namespace DCM.Core.Services;

public class DiscordConfigService : IDiscordConfigService
{
    public DiscordSocketConfig Config { get; } = new();
}
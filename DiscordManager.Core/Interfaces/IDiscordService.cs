using Discord.WebSocket;

namespace DCM.Core.Interfaces;

public interface IDiscordService
{
    Task StartAsync();
    Task StopAsync();
    DiscordSocketClient Client { get; set; }
}
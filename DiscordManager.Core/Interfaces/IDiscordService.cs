namespace DiscordManager.Core.Interfaces;

internal interface IDiscordService
{
    Task StartAsync(string loginToken);
    Task StopAsync();
}
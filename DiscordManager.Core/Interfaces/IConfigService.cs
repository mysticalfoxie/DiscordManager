using DCM.Core.Models;
using Discord.WebSocket;

namespace DCM.Core.Interfaces;

public interface IConfigService
{
    void AddConfig<T>(FileInfo file) where T : class;
    void AddConfig<T>(T config) where T : class;
    T ReadConfig<T>() where T : class;
    ulong? DefaultGuild { get; set; }
    JsonDiscordConfig DiscordConfig { get; set; }
    DiscordSocketConfig GetDiscordConfig();
}
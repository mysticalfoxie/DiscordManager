using DCM.Core.Models;
using Discord.WebSocket;

namespace DCM.Core.Interfaces;

public interface IConfigService
{
    ulong? DefaultGuild { get; set; }
    JsonDiscordConfig DiscordConfig { get; set; }
    void AddConfig<T>(FileInfo file) where T : class;
    void AddConfig<T>(T config) where T : class;
    DiscordSocketConfig GetDiscordConfig();
    T ReadConfig<T>() where T : class;
}
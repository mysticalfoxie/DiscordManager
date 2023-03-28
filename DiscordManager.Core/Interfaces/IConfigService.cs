using DCM.Core.Models;
using Discord.WebSocket;

namespace DCM.Core.Interfaces;

public interface IConfigService
{
    ulong? DefaultGuild { get; set; }
    DCMDiscordConfig DiscordConfig { get; set; }

    void AddDCMConfig<T>(T config) where T : DCMConfig;
    void AddPluginConfig<T>(T config) where T : class;
    void AddDiscordConfig<T>(T config) where T : DCMDiscordConfig;
    void AddGuildConfig<T>(T config) where T : DCMDiscordConfig;

    DiscordSocketConfig GetDiscordConfig();
    T ReadConfig<T>() where T : class;
}
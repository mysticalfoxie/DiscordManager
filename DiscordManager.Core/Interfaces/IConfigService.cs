using DCM.Core.Models;
using Discord.WebSocket;

namespace DCM.Core.Interfaces;

public interface IConfigService
{
    DCMGlobalConfig GlobalConfig { get; }
    DCMGuildConfig GuildConfig { get; }
    DCMDiscordConfig DiscordConfig { get; }

    void AddConfig<T>(T config) where T : class;

    void AddDCMConfig<T>(T config) where T : DCMGlobalConfig;
    void AddDiscordConfig<T>(T config) where T : DCMDiscordConfig;
    void AddGuildConfig<T>(T config) where T : DCMGuildConfig;
    Task LoadPluginConfigs(IEnumerable<DCMPlugin> plugins);
    Task<T> ReadConfig<T>(string filename) where T : class;

    DiscordSocketConfig ReadSocketConfig();
}
using System.Reflection;
using DCM.Core.Interfaces;
using DCM.Core.Models;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace DCM.Core.Services;

public class ConfigService : IConfigService
{
    // TODO: ServiceInjection Support for PluginRefs
    private readonly Dictionary<Type, object> _configurations = new();

    public DCMGlobalConfig GlobalConfig { get; set; }
    public DCMGuildConfig GuildConfig { get; set; }
    public DCMDiscordConfig DiscordConfig { get; set; }

    public void AddConfig<T>(T config) where T : class
    {
        SafeAddConfig(config: config);
    }

    public void AddDCMConfig<T>(T config) where T : DCMGlobalConfig
    {
        SafeAddConfig(config: config);
        GlobalConfig = config;
    }

    public void AddDiscordConfig<T>(T config) where T : DCMDiscordConfig
    {
        SafeAddConfig(config: config);
        DiscordConfig = config;
    }

    public void AddGuildConfig<T>(T config) where T : DCMGuildConfig
    {
        SafeAddConfig(config: config);
        GuildConfig = config;
    }

    public async Task<T> ReadConfig<T>(string filename) where T : class
    {
        var json = await File.ReadAllTextAsync(path: filename);
        return JsonConvert.DeserializeObject<T>(value: json);
    }

    public DiscordSocketConfig ReadSocketConfig()
    {
        if (DiscordConfig is null)
            return new DiscordSocketConfig();

        var config = new DiscordSocketConfig();

        if (!string.IsNullOrWhiteSpace(value: DiscordConfig.GatewayHost))
            config.GatewayHost = DiscordConfig.GatewayHost;

        if (DiscordConfig.ConnectionTimeout.HasValue)
            config.ConnectionTimeout = DiscordConfig.ConnectionTimeout.Value;

        if (DiscordConfig.ShardId.HasValue)
            config.ShardId = DiscordConfig.ShardId.Value;

        if (DiscordConfig.TotalShards.HasValue)
            config.TotalShards = DiscordConfig.TotalShards.Value;

        if (DiscordConfig.AlwaysDownloadDefaultStickers.HasValue)
            config.AlwaysDownloadDefaultStickers = DiscordConfig.AlwaysDownloadDefaultStickers.Value;

        if (DiscordConfig.AlwaysResolveStickers.HasValue)
            config.AlwaysResolveStickers = DiscordConfig.AlwaysResolveStickers.Value;

        if (DiscordConfig.MessageCacheSize.HasValue)
            config.MessageCacheSize = DiscordConfig.MessageCacheSize.Value;

        if (DiscordConfig.LargeThreshold.HasValue)
            config.LargeThreshold = DiscordConfig.LargeThreshold.Value;

        if (DiscordConfig.AlwaysDownloadUsers.HasValue)
            config.AlwaysDownloadUsers = DiscordConfig.AlwaysDownloadUsers.Value;

        if (DiscordConfig.HandlerTimeout.HasValue)
            config.HandlerTimeout = DiscordConfig.HandlerTimeout.Value;

        if (DiscordConfig.IdentifyMaxConcurrency.HasValue)
            config.IdentifyMaxConcurrency = DiscordConfig.IdentifyMaxConcurrency.Value;

        if (DiscordConfig.MaxWaitBetweenGuildAvailablesBeforeReady.HasValue)
            config.MaxWaitBetweenGuildAvailablesBeforeReady =
                DiscordConfig.MaxWaitBetweenGuildAvailablesBeforeReady.Value;

        if (DiscordConfig.GatewayIntents.HasValue)
            config.GatewayIntents = (GatewayIntents)DiscordConfig.GatewayIntents.Value;

        if (DiscordConfig.LogGatewayIntentWarnings.HasValue)
            config.LogGatewayIntentWarnings = DiscordConfig.LogGatewayIntentWarnings.Value;

        if (DiscordConfig.SuppressUnknownDispatchWarnings.HasValue)
            config.SuppressUnknownDispatchWarnings = DiscordConfig.SuppressUnknownDispatchWarnings.Value;

        if (DiscordConfig.DefaultRetryMode.HasValue)
            config.DefaultRetryMode = DiscordConfig.DefaultRetryMode.Value;

        if (DiscordConfig.LogLevel.HasValue)
            config.LogLevel = DiscordConfig.LogLevel.Value;

        if (DiscordConfig.UseSystemClock.HasValue)
            config.UseSystemClock = DiscordConfig.UseSystemClock.Value;

        if (DiscordConfig.UseInteractionSnowflakeDate.HasValue)
            config.UseInteractionSnowflakeDate = DiscordConfig.UseInteractionSnowflakeDate.Value;

        if (DiscordConfig.FormatUsersInBidirectionalUnicode.HasValue)
            config.FormatUsersInBidirectionalUnicode = DiscordConfig.FormatUsersInBidirectionalUnicode.Value;

        if (DiscordConfig.APIOnRestInteractionCreation.HasValue)
            config.APIOnRestInteractionCreation = DiscordConfig.APIOnRestInteractionCreation.Value;

        return config;
    }

    private void SafeAddConfig<T>(T config) where T : class
    {
        if (_configurations.ContainsKey(typeof(T)))
            throw new AmbiguousMatchException("Cannot add the same configuration type twice!");

        _configurations.Add(typeof(T), value: config);
    }
}
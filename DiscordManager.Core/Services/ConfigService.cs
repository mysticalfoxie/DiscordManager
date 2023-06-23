using System.Reflection;
using DCM.Core.Attributes;
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
    private readonly ICredentialsService _credentials;

    public ConfigService(
        ICredentialsService credentials)
    {
        _credentials = credentials;
    }

    public DCMGlobalConfig GlobalConfig { get; private set; }
    public DCMGuildConfig GuildConfig { get; private set; }
    public DCMDiscordConfig DiscordConfig { get; private set; }

    public void AddConfig<T>(T config) where T : class
    {
        SafeAddConfig(config);
    }

    public void AddDCMConfig<T>(T config) where T : DCMGlobalConfig
    {
        SafeAddConfig(config);
        GlobalConfig = config;

        _credentials.LoginToken = GlobalConfig.LoginToken;
    }

    public void AddDiscordConfig<T>(T config) where T : DCMDiscordConfig
    {
        SafeAddConfig(config);
        DiscordConfig = config;
    }

    public void AddGuildConfig<T>(T config) where T : DCMGuildConfig
    {
        SafeAddConfig(config);
        GuildConfig = config;
    }

    public async Task LoadPluginConfigs(IEnumerable<DCMPlugin> plugins)
    {
        foreach (var plugin in plugins)
        {
            var attribute = plugin.GetType().GetCustomAttribute<PluginConfigAttribute>();
            if (attribute is null)
                continue;

            plugin.PluginConfig = await GetPluginConfig(attribute, plugin);
        }
    }

    public async Task<T> ReadConfig<T>(string filename) where T : class
    {
        return await ReadConfig(typeof(T), filename) as T;
    }

    public DiscordSocketConfig ReadSocketConfig()
    {
        if (DiscordConfig is null)
            return new DiscordSocketConfig();

        var config = new DiscordSocketConfig();

        if (!string.IsNullOrWhiteSpace(DiscordConfig.GatewayHost))
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

    public static async Task<object> ReadConfig(Type type, string filename)
    {
        var json = await File.ReadAllTextAsync(filename);
        return JsonConvert.DeserializeObject(json, type);
    }

    private static async Task<DCMPluginConfig> GetPluginConfig(PluginConfigAttribute attribute, DCMPlugin plugin)
    {
        if (attribute.File is not null && attribute.Type is not null)
            return (DCMPluginConfig)await ReadConfig(attribute.Type, attribute.File.FullName);

        if (attribute.Value is not null && attribute.Type is not null)
            return (DCMPluginConfig)attribute.Value;

        if (attribute.Filepath is null || attribute.Type is null)
            throw new InvalidOperationException("Attribute is faulty configured.");

        var filepath = GetPluginFilepath(plugin, attribute.Filepath);
        var config = (DCMPluginConfig)await ReadConfig(attribute.Type, filepath);

        var keyValues = config
            .GetType()
            .GetProperties()
            .Where(x => x.CanRead && x.CanWrite)
            .Select(x => new
            {
                Key = x.Name,
                Value = x.GetMethod!.Invoke(config, Array.Empty<object>())
            });

        foreach (var keyValue in keyValues)
            config.Entries.Add(keyValue.Key, keyValue.Value);

        return config;
    }

    private static string GetPluginFilepath(DCMPlugin plugin, string attribute)
    {
        if (Path.IsPathFullyQualified(attribute))
            return attribute;

        var assembly = plugin.GetType().Assembly;
        var assemblyFile = new FileInfo(assembly.Location);
        var pluginFolder = assemblyFile.Directory!.FullName;
        return Path.Combine(pluginFolder, attribute);
    }

    private void SafeAddConfig<T>(T config) where T : class
    {
        if (_configurations.ContainsKey(typeof(T)))
            throw new AmbiguousMatchException("Cannot add the same configuration type twice!");

        _configurations.Add(typeof(T), config);
    }
}
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using DCM.Core.Interfaces;
using DCM.Core.Models;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DCM.Core.Services;

public class ConfigService : IConfigService
{
    private readonly IDependencyService _dependencyService;

    public ConfigService(
        IDependencyService dependencyService)
    {
        _dependencyService = dependencyService;
    }

    public ulong? DefaultGuild { get; set; }
    public JsonDiscordConfig DiscordConfig { get; set; }
    public Dictionary<Type, FileInfo> Configs { get; } = new();
    public Dictionary<Type, object> Instances { get; } = new();


    [SuppressMessage("ReSharper", "RedundantAssignment")]
    public void AddConfig<T>(FileInfo file) where T : class
    {
        if (Configs.ContainsKey(typeof(T)))
            throw new AmbiguousMatchException("Cannot add the same type for a configuration twice!");

        Configs.Add(typeof(T), value: file);
        var config = ReadConfig<T>();
        _dependencyService.Services
            .Configure<T>(options => options = config);
    }

    [SuppressMessage("ReSharper", "RedundantAssignment")]
    public void AddConfig<T>(T instance) where T : class
    {
        if (Instances.ContainsKey(typeof(T)))
            throw new AmbiguousMatchException("Cannot add the same type for a configuration twice!");

        Instances.Add(typeof(T), value: instance);
        var config = ReadConfig<T>();
        _dependencyService.Services
            .Configure<T>(options => options = config);
    }

    public DiscordSocketConfig GetDiscordConfig()
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

    public T ReadConfig<T>() where T : class
    {
        if (!Configs.ContainsKey(typeof(T)))
            throw new InvalidOperationException("Could not find the configuration type.");

        var value = Configs[typeof(T)];
        var json = File.ReadAllText(path: value.FullName);

        return JsonConvert.DeserializeObject<T>(value: json);
    }
}
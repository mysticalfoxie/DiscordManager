using DCM.Core.Interfaces;
using DCM.Core.Models;

namespace DCM.Core.Builder;

public class DefaultConfigBuilder<TConfig> : IBuilder<DefaultConfig> where TConfig : class
{
    private readonly DefaultConfig _defaultConfig = new();
    private readonly JsonDiscordConfig _discordConfig = new();
    private readonly TConfig _config;

    public DefaultConfigBuilder(TConfig config)
    {
        _config = config;
    }


    public DefaultConfigBuilder<TConfig> HasLoginToken(Func<TConfig, string> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _defaultConfig.LoginToken = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasDefaultGuild(Func<TConfig, ulong> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _defaultConfig.DefaultGuild = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasGatewayHost(Func<TConfig, string> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.GatewayHost = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasConnectionTimeout(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.ConnectionTimeout = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasShardId(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.ShardId = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasTotalShards(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.TotalShards = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasAlwaysDownloadDefaultStickers(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.AlwaysDownloadDefaultStickers = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasAlwaysResolveStickers(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.AlwaysResolveStickers = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasMessageCacheSize(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.MessageCacheSize = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasLargeThreshold(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.LargeThreshold = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasAlwaysDownloadUsers(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.AlwaysDownloadUsers = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasHandlerTimeout(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.HandlerTimeout = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasIdentifyMaxConcurrency(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.IdentifyMaxConcurrency = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasMaxWaitBetweenGuildAvailablesBeforeReady(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.MaxWaitBetweenGuildAvailablesBeforeReady = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasGatewayIntents(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.GatewayIntents = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasLogGatewayIntentWarnings(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.LogGatewayIntentWarnings = selector(arg: _config);
        return this;
    }

    public DefaultConfigBuilder<TConfig> HasSuppressUnknownDispatchWarnings(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.SuppressUnknownDispatchWarnings = selector(arg: _config);
        return this;
    }

    public DefaultConfig Build()
    {
        return _defaultConfig;
    }
}
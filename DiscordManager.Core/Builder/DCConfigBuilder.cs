using DCM.Core.Interfaces;
using DCM.Core.Models;
using Discord;

namespace DCM.Core.Builder;

public class DiscordConfigBuilder<TConfig> : IBuilder<DCMConfig> where TConfig : class
{
    private readonly TConfig _config;
    private readonly DCMConfig _dcmConfig = new();
    private readonly DCMDiscordConfig _discordConfig = new();

    public DiscordConfigBuilder(TConfig config)
    {
        _config = config;
    }


    public DCMConfig Build()
    {
        return _dcmConfig;
    }

    public DiscordConfigBuilder<TConfig> HasAlwaysDownloadDefaultStickers(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.AlwaysDownloadDefaultStickers = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasAlwaysDownloadUsers(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.AlwaysDownloadUsers = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasAlwaysResolveStickers(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.AlwaysResolveStickers = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasAPIOnRestInteractionCreation(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.APIOnRestInteractionCreation = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasConnectionTimeout(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.ConnectionTimeout = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasDefaultRetryMode(Func<TConfig, RetryMode?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.DefaultRetryMode = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasFormatUsersInBidirectionalUnicode(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.FormatUsersInBidirectionalUnicode = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasGatewayHost(Func<TConfig, string> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.GatewayHost = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasGatewayIntents(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.GatewayIntents = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasHandlerTimeout(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.HandlerTimeout = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasIdentifyMaxConcurrency(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.IdentifyMaxConcurrency = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasLargeThreshold(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.LargeThreshold = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasLogGatewayIntentWarnings(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.LogGatewayIntentWarnings = selector(arg: _config);
        return this;
    }


    public DiscordConfigBuilder<TConfig> HasLoginToken(Func<TConfig, string> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _dcmConfig.LoginToken = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasLogLevel(Func<TConfig, LogSeverity?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.LogLevel = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasMaxWaitBetweenGuildAvailablesBeforeReady(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.MaxWaitBetweenGuildAvailablesBeforeReady = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasMessageCacheSize(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.MessageCacheSize = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasShardId(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.ShardId = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasSuppressUnknownDispatchWarnings(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.SuppressUnknownDispatchWarnings = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasTotalShards(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.TotalShards = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasUseInteractionSnowflakeDate(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.UseInteractionSnowflakeDate = selector(arg: _config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasUseSystemClock(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.UseSystemClock = selector(arg: _config);
        return this;
    }
}
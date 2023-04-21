using DCM.Core.Interfaces;
using DCM.Core.Models;
using Discord;

namespace DCM.Core.Builders;

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

        _discordConfig.AlwaysDownloadDefaultStickers = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasAlwaysDownloadUsers(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.AlwaysDownloadUsers = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasAlwaysResolveStickers(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.AlwaysResolveStickers = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasAPIOnRestInteractionCreation(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.APIOnRestInteractionCreation = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasConnectionTimeout(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.ConnectionTimeout = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasDefaultRetryMode(Func<TConfig, RetryMode?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.DefaultRetryMode = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasFormatUsersInBidirectionalUnicode(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.FormatUsersInBidirectionalUnicode = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasGatewayHost(Func<TConfig, string> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.GatewayHost = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasGatewayIntents(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.GatewayIntents = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasHandlerTimeout(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.HandlerTimeout = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasIdentifyMaxConcurrency(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.IdentifyMaxConcurrency = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasLargeThreshold(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.LargeThreshold = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasLogGatewayIntentWarnings(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.LogGatewayIntentWarnings = selector(_config);
        return this;
    }


    public DiscordConfigBuilder<TConfig> HasLoginToken(Func<TConfig, string> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _dcmConfig.LoginToken = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasLogLevel(Func<TConfig, LogSeverity?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.LogLevel = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasMaxWaitBetweenGuildAvailablesBeforeReady(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.MaxWaitBetweenGuildAvailablesBeforeReady = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasMessageCacheSize(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.MessageCacheSize = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasShardId(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.ShardId = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasSuppressUnknownDispatchWarnings(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.SuppressUnknownDispatchWarnings = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasTotalShards(Func<TConfig, int?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.TotalShards = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasUseInteractionSnowflakeDate(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.UseInteractionSnowflakeDate = selector(_config);
        return this;
    }

    public DiscordConfigBuilder<TConfig> HasUseSystemClock(Func<TConfig, bool?> selector)
    {
        if (selector is null)
            throw new ArgumentNullException(nameof(selector));

        _discordConfig.UseSystemClock = selector(_config);
        return this;
    }
}
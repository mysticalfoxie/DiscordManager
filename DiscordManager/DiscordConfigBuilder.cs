using Discord;
using Discord.Net.Rest;
using Discord.Net.Udp;
using Discord.Net.WebSockets;
using Discord.WebSocket;

namespace DCM
{
    public interface IDiscordConfigBuilder
    {
        /// <summary>
        ///     Sets whether or not all users should be downloaded as guilds come available.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         By default, the Discord gateway will only send offline members if a guild has less than a certain number
        ///         of members (determined by <see cref="LargeThreshold"/> in this library). This behavior is why
        ///         sometimes a user may be missing from the WebSocket cache for collections such as
        ///         <see cref="Discord.WebSocket.SocketGuild.Users"/>.
        ///     </para>
        ///     <para>
        ///         This property ensures that whenever a guild becomes available (determined by
        ///         <see cref="Discord.WebSocket.BaseSocketClient.GuildAvailable"/>), incomplete user chunks will be
        ///         downloaded to the WebSocket cache.
        ///     </para>
        ///     <para>
        ///         For more information, please see
        ///         <see href="https://discord.com/developers/docs/topics/gateway#request-guild-members">Request Guild Members</see>
        ///         on the official Discord API documentation.
        ///     </para>
        ///     <note>
        ///         Please note that it can be difficult to fill the cache completely on large guilds depending on the
        ///         traffic. If you are using the command system, the default user TypeReader may fail to find the user
        ///         due to this issue. This may be resolved at v3 of the library. Until then, you may want to consider
        ///         overriding the TypeReader and use
        ///         <see cref="DiscordRestClient.GetUserAsync(ulong,Discord.RequestOptions)"/>
        ///         or <see cref="DiscordRestClient.GetGuildUserAsync"/>
        ///         as a backup.
        ///     </note>
        /// </remarks>
        IDiscordConfigBuilder AlwaysDownloadUsers(bool alwaysDownloadUsers = true);

        /// <summary>
        /// 	Sets whether or not rate-limits should use the system clock.
        /// </summary>
        /// <remarks>
        ///		If set to <c>false</c>, we will use the X-RateLimit-Reset-After header
        ///		to determine when a rate-limit expires, rather than comparing the
        ///		X-RateLimit-Reset timestamp to the system time.
        ///
        ///		This should only be changed to false if the system is known to have
        /// 	a clock that is out of sync. Relying on the Reset-After header will
        ///		incur network lag.
        ///
        ///		Regardless of this property, we still rely on the system's wall-clock
        ///		to determine if a bucket is rate-limited; we do not use any monotonic
        ///		clock. Your system will still need a stable clock.
        /// </remarks>
        IDiscordConfigBuilder UseSystemClock(bool useSystemClock = true);

        /// <summary>
        ///     Sets the provider used to generate new WebSocket connections.
        /// </summary>
        IDiscordConfigBuilder UseWebSocketProvider(WebSocketProvider provider);

        /// <summary>
        ///     Sets the time, in milliseconds, to wait for a connection to complete before aborting.
        /// </summary>
        IDiscordConfigBuilder WithConnectionTimeout(int timeout);

        /// <summary>
        ///     Sets how a request should act in the case of an error, by default.
        /// </summary>
        /// <returns>
        ///     The currently set <see cref="RetryMode"/>.
        /// </returns>
        IDiscordConfigBuilder WithDefaultRetryMode(RetryMode mode);

        /// <summary>
        ///     Sets the WebSocket host to connect to. If <c>null</c>, the client will use the
        ///     /gateway endpoint.
        /// </summary>
        IDiscordConfigBuilder WithGatewayHost(string host);

        /// <summary>
        ///    Sets gateway intents to limit what events are sent from Discord.
        ///    The default is <see cref="GatewayIntents.AllUnprivileged"/>.
        /// </summary>
        /// <remarks>
        ///     For more information, please see
        ///     <see href="https://discord.com/developers/docs/topics/gateway#gateway-intents">GatewayIntents</see>
        ///     on the official Discord API documentation.
        /// </remarks>
        IDiscordConfigBuilder WithGatewayIntents(GatewayIntents intents);

        /// <summary>
        ///     Sets the timeout for event handlers, in milliseconds, after which a warning will be logged.
        ///     Setting this property to <c>null</c>disables this check.
        /// </summary>
        IDiscordConfigBuilder WithHandlerTimeout(int? timeout);

        /// <summary>
        ///     Sets the maximum identify concurrency.
        /// </summary>
        /// <remarks>
        ///     This information is provided by Discord.
        ///     It is only used when using a <see cref="DiscordShardedClient"/> and auto-sharding is disabled.
        /// </remarks>
        IDiscordConfigBuilder WithIdentifyMaxConcurrency(int concurrency);

        IDiscordConfigBuilder WithInteractionSnowflakeDate(bool value);

        /// <summary>
        ///     Sets the max number of users a guild may have for offline users to be included in the READY
        ///     packet. The maximum value allowed is 250.
        /// </summary>
        IDiscordConfigBuilder WithLargeThreshold(int threshold);

        /// <summary>
        ///     Sets the minimum log level severity that will be sent to the Log event.
        /// </summary>
        /// <returns>
        ///     The currently set <see cref="LogSeverity"/> for logging level.
        /// </returns>
        IDiscordConfigBuilder WithLogLevel(LogSeverity severity);

        /// <summary>
        ///     Sets the maximum wait time in milliseconds between GUILD_AVAILABLE events before firing READY.
        ///     If zero, READY will fire as soon as it is received and all guilds will be unavailable.
        /// </summary>
        /// <remarks>
        ///     <para>This property is measured in milliseconds; negative values will throw an exception.</para>
        ///     <para>If a guild is not received before READY, it will be unavailable.</para>
        /// </remarks>
        /// <returns>
        ///     A <see cref="int"/> representing the maximum wait time in milliseconds between GUILD_AVAILABLE events
        ///     before firing READY.
        /// </returns>
        /// <exception cref="System.ArgumentException">Value must be at least 0.</exception>
        IDiscordConfigBuilder WithMaxWaitBetweenGuildAvailablesBeforeReady(int timeInMS);

        /// <summary>
        ///     Sets the number of messages per channel that should be kept in cache. Setting this to zero
        ///     disables the message cache entirely.
        /// </summary>
        IDiscordConfigBuilder WithMessageCacheSize(int size);

        /// <summary> 
        /// Sets the provider used to generate new REST connections. 
        /// </summary>
        IDiscordConfigBuilder WithRestClientProvider(RestClientProvider provider);

        /// <summary>
        ///     Sets the ID for this shard. Must be less than <see cref="TotalShards"/>.
        /// </summary>
        IDiscordConfigBuilder WithShardId(int? shardId);

        /// <summary>
        ///     Sets the total number of shards for this application.
        /// </summary>
        IDiscordConfigBuilder WithTotalShards(int? count);

        /// <summary>
        ///     Sets the provider used to generate new UDP sockets.
        /// </summary>
        IDiscordConfigBuilder WithUdpSocketProvider(UdpSocketProvider provider);
    }

    internal class DiscordConfigBuilder : IDiscordConfigBuilder
    {
        private readonly DiscordSocketConfig _config;

        public DiscordConfigBuilder(DiscordSocketConfig config)
            => _config = config;

        public IDiscordConfigBuilder AlwaysDownloadUsers(bool alwaysDownloadUsers = true)
        {
            _config.AlwaysDownloadUsers = alwaysDownloadUsers;
            return this;
        }

        public IDiscordConfigBuilder WithConnectionTimeout(int timeout)
        {
            _config.ConnectionTimeout = timeout;
            return this;
        }

        public IDiscordConfigBuilder WithDefaultRetryMode(RetryMode mode)
        {
            _config.DefaultRetryMode = mode;
            return this;
        }

        public IDiscordConfigBuilder WithInteractionSnowflakeDate(bool value)
        {
            _config.UseInteractionSnowflakeDate = value;
            return this;
        }

        public IDiscordConfigBuilder WithGatewayHost(string host)
        {
            _config.GatewayHost = host;
            return this;
        }

        public IDiscordConfigBuilder WithGatewayIntents(GatewayIntents intents)
        {
            _config.GatewayIntents = intents;
            return this;
        }

        public IDiscordConfigBuilder WithHandlerTimeout(int? timeout)
        {
            _config.HandlerTimeout = timeout;
            return this;
        }

        public IDiscordConfigBuilder WithLargeThreshold(int threshold)
        {
            _config.LargeThreshold = threshold;
            return this;
        }

        public IDiscordConfigBuilder WithIdentifyMaxConcurrency(int concurrency)
        {
            _config.IdentifyMaxConcurrency = concurrency;
            return this;
        }

        public IDiscordConfigBuilder WithLogLevel(LogSeverity severity)
        {
            _config.LogLevel = severity;
            return this;
        }

        public IDiscordConfigBuilder WithMaxWaitBetweenGuildAvailablesBeforeReady(int timeInMS)
        {
            _config.MaxWaitBetweenGuildAvailablesBeforeReady = timeInMS;
            return this;
        }

        public IDiscordConfigBuilder WithMessageCacheSize(int size)
        {
            _config.MessageCacheSize = size;
            return this;
        }

        public IDiscordConfigBuilder WithRestClientProvider(RestClientProvider provider)
        {
            _config.RestClientProvider = provider;
            return this;
        }

        public IDiscordConfigBuilder WithShardId(int? shardId)
        {
            _config.ShardId = shardId;
            return this;
        }

        public IDiscordConfigBuilder WithTotalShards(int? count)
        {
            _config.TotalShards = count;
            return this;
        }

        public IDiscordConfigBuilder WithUdpSocketProvider(UdpSocketProvider provider)
        {
            _config.UdpSocketProvider = provider;
            return this;
        }

        public IDiscordConfigBuilder UseSystemClock(bool useSystemClock = true)
        {
            _config.UseSystemClock = useSystemClock;
            return this;
        }

        public IDiscordConfigBuilder UseWebSocketProvider(WebSocketProvider provider)
        {
            _config.WebSocketProvider = provider;
            return this;
        }
    }
}

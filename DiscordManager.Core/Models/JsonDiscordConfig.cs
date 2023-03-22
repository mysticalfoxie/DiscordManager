namespace DCM.Core.Models;

public class JsonDiscordConfig
{
    public string GatewayHost { get; set; }

    public int? ConnectionTimeout { get; set; }

    public int? ShardId { get; set; }

    public int? TotalShards { get; set; }

    public bool? AlwaysDownloadDefaultStickers { get; set; }

    public bool? AlwaysResolveStickers { get; set; }

    public int? MessageCacheSize { get; set; }

    public int? LargeThreshold { get; set; }

    public bool? AlwaysDownloadUsers { get; set; }

    public int? HandlerTimeout { get; set; }

    public int? IdentifyMaxConcurrency { get; set; }

    public int? MaxWaitBetweenGuildAvailablesBeforeReady { get; set; }

    public int? GatewayIntents { get; set; }

    public bool? LogGatewayIntentWarnings { get; set; }

    public bool? SuppressUnknownDispatchWarnings { get; set; }
}
namespace DCM.Core.Models;

public class DefaultConfig
{
    public string LoginToken { get; set; }
    public ulong? DefaultGuild { get; set; }
    public JsonDiscordConfig DiscordConfig { get; set; }
}
namespace DCM.Core.Models;

public class DCMConfig
{
    public string LoginToken { get; set; }
    public ulong? DefaultGuild { get; set; }
    public DCMDiscordConfig DiscordConfig { get; set; }
}
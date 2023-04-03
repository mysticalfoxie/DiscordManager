namespace DCM.Core.Models;

public class DCMConfig
{
    public string LoginToken { get; set; }
    public DCMDiscordConfig Discord { get; set; }
    public DCMGuildConfig Guild { get; set; }
}
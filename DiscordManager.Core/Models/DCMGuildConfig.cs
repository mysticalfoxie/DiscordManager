namespace DCM.Core.Models;

public class DCMGuildConfig
{
    public ulong Id { get; set; }
    public ICollection<DCMGuildConfigIdMapping> TextChannels { get; set; }
    public ICollection<DCMGuildConfigIdMapping> VoiceChannels { get; set; }
    public ICollection<DCMGuildConfigIdMapping> GuildUsers { get; set; }
    public ICollection<DCMGuildConfigIdMapping> Roles { get; set; }
}

public class DCMGuildConfigIdMapping
{
    public ulong Id { get; set; }
    public string Name { get; set; }
}
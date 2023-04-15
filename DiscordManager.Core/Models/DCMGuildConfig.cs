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

public class DCMGuildConfigValue<TValue> : DCMGuildConfigIdMapping
{
    public DCMGuildConfigValue(TValue value, DCMGuildConfigIdMapping entry)
    {
        Id = entry.Id;
        Name = entry.Name;
        Value = value;
    }

    public TValue Value { get; set; }
}
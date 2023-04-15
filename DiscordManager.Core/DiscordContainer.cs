using Discord;

namespace DCM.Core;

public abstract class DiscordContainer : ServiceContainer
{
    public IGuild Guild { get; internal set; }
    public Dictionary<string, IGuildUser> GuildUsers { get; } = new();
    public Dictionary<string, IRole> GuildRoles { get; } = new();
    public Dictionary<string, ITextChannel> GuildTextChannels { get; } = new();
    public Dictionary<string, IVoiceChannel> GuildVoiceChannels { get; } = new();

    public IGuild GetRequiredGuild(ulong id)
    {
        return DiscordService.GetRequiredGuild(id);
    }
}
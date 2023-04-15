using Discord;

namespace DCM.Core;

public abstract class DiscordContainer : ServiceContainer
{
    public IGuild Guild { get; internal set; }
    public Dictionary<string, IGuildUser> GuildUsers { get; internal set; }
    public Dictionary<string, IRole> GuildRoles { get; internal set; }
    public Dictionary<string, ITextChannel> GuildTextChannels { get; internal set; }
    public Dictionary<string, IVoiceChannel> GuildVoiceChannels { get; internal set; }

    public async Task<IGuild> GetGuildAsync(ulong id)
    {
        var guild = await Client.GetGuildAsync(id);
        if (guild is null)
            throw new NullReferenceException(nameof(guild));

        return guild;
    }
    //
    // public async Task<
}
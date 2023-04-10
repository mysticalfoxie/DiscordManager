using Discord;

namespace DCM.Core;

public abstract class DiscordContainer : ServiceContainer
{
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
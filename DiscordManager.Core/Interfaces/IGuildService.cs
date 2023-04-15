using Discord;

namespace DCM.Core.Interfaces;

public interface IGuildService
{
    IGuild Guild { get; }
    IReadOnlyCollection<IGuildUser> Users { get; }
    IReadOnlyCollection<IRole> Roles { get; }
    IReadOnlyCollection<ITextChannel> TextChannels { get; }
    IReadOnlyCollection<IVoiceChannel> VoiceChannels { get; }
    Task Load();
    DiscordContainer PropagateContainerFromCache(DiscordContainer container);
}
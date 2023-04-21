using System.Reactive.Subjects;
using DCM.Core.Builders;
using DCM.Core.Collectors;
using DCM.Core.Events;
using Discord;

namespace DCM.Core;

public abstract class DiscordContainer : ServiceContainer
{
    public IGuild Guild { get; internal set; }
    public Dictionary<string, IGuildUser> GuildUsers { get; } = new();
    public Dictionary<string, IRole> GuildRoles { get; } = new();
    public Dictionary<string, ITextChannel> GuildTextChannels { get; } = new();
    public Dictionary<string, IVoiceChannel> GuildVoiceChannels { get; } = new();

    protected MessageCollector CreateMessageCollector(IMessageChannel channel = null)
    {
        return DiscordService.CreateMessageCollector(channel);
    }

    protected ReactionCollector CreateReactionCollector(IMessage message = null)
    {
        return DiscordService.CreateReactionCollector(message);
    }

    protected Task BulkDelete(IEnumerable<IDeletable> deletables)
    {
        return DiscordService.BulkDelete(deletables);
    }

    protected Task<Subject<SlashCommandExecutedEvent>> CreateSlashCommand(IGuild guild,
        Action<DCMSlashCommandBuilder> configure)
    {
        return DiscordService.CreateSlashCommand(guild, configure);
    }

    protected IGuild GetRequiredGuild(ulong id)
    {
        return DiscordService.GetRequiredGuild(id);
    }
}
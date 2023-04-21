using System.Reactive.Subjects;
using DCM.Core.Builders;
using DCM.Core.Collectors;
using DCM.Core.Events;
using Discord;

namespace DCM.Core.Interfaces;

public interface IDiscordService
{
    Task BulkDelete(IEnumerable<IDeletable> deletable);
    MessageCollector CreateMessageCollector(IMessageChannel channel);
    ReactionCollector CreateReactionCollector(IMessage message);

    Task<Subject<SlashCommandExecutedEvent>> CreateSlashCommand(IGuild guild,
        Action<DCMSlashCommandBuilder> configure);

    IGuild GetRequiredGuild(ulong id);
}
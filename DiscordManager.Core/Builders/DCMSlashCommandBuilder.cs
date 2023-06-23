using DCM.Core.Events;
using DCM.Core.Models;
using Discord;

namespace DCM.Core.Builders;

public class DCMSlashCommandBuilder
{
    public SlashCommandProperties Properties { get; set; }
    public Func<SlashCommandExecutedEvent, Task> AsyncSubscriber { get; set; }
    public Action<SlashCommandExecutedEvent> SyncSubscriber { get; set; }

    public DCMSlashCommandProperties Build()
    {
        return new DCMSlashCommandProperties
        {
            AsyncSubscriber = AsyncSubscriber,
            SyncSubscriber = SyncSubscriber,
            Properties = Properties
        };
    }

    public DCMSlashCommandBuilder OnExecuted(Func<SlashCommandExecutedEvent, Task> subscriber)
    {
        AsyncSubscriber = subscriber;
        return this;
    }

    public DCMSlashCommandBuilder OnExecuted(Action<SlashCommandExecutedEvent> subscriber)
    {
        SyncSubscriber = subscriber;
        return this;
    }

    public DCMSlashCommandBuilder WithParameters(Action<SlashCommandBuilder> configure)
    {
        var builder = new SlashCommandBuilder();
        configure(builder);
        Properties = builder.Build();
        return this;
    }
}
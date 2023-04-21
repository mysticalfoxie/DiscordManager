using DCM.Core.Events;
using Discord;

namespace DCM.Core.Models;

public class DCMSlashCommandProperties
{
    internal DCMSlashCommandProperties()
    {
    }

    public SlashCommandProperties Properties { get; set; }

    public Func<SlashCommandExecutedEvent, Task> AsyncSubscriber { get; set; }
    public Action<SlashCommandExecutedEvent> SyncSubscriber { get; set; }
}
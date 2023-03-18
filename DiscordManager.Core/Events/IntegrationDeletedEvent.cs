using DCM.Core.Interfaces;
using Discord;

namespace DCM.Core.Events;

public class IntegrationDeletedEvent : IEvent
{
    public IGuild Guild { get; set; }
    public ulong Id { get; set; }
    public Optional<ulong> Huh { get; set; }
}
using DCM.Core.Interfaces;
using Discord;

namespace DCM.Core.Events;

public class IntegrationUpdatedEvent : IEvent
{
    public IIntegration Integration { get; set; }
}
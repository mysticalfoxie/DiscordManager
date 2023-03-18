using DCM.Core.Interfaces;
using Discord;

namespace DCM.Core.Events;

public class IntegrationCreatedEvent : IEvent
{
    public IIntegration Integration { get; set; }
}
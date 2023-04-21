using System.Reactive.Linq;
using DCM.Core.Events;
using DCM.Core.Interfaces;
using Discord;

namespace DCM.Core.Collectors;

public class ReactionCollector : CollectorBase<ReactionAddedEvent>
{
    public ReactionCollector(
        IMessage message,
        IEventService eventService)
    {
        Message = message;
        Source = eventService.ReactionAdded
            .Where(x => Message is null || x.Message.Id == Message.Id);
    }

    public IMessage Message { get; }
}
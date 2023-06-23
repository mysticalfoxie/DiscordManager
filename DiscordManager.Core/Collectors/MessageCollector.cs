using System.Reactive.Linq;
using DCM.Core.Events;
using DCM.Core.Interfaces;
using Discord;

namespace DCM.Core.Collectors;

public class MessageCollector : CollectorBase<MessageReceivedEvent>
{
    public MessageCollector(
        IMessageChannel channel,
        IEventService eventService)
    {
        Channel = channel;
        Source = eventService.MessageReceived
            .Where(x => Channel is null || x.Message.Channel.Id == Channel.Id);
    }

    public IMessageChannel Channel { get; }
}
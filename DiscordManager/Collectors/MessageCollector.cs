using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordManager.Core.Events;

namespace DCM.Collectors;

/// <summary>
///     An observer for the <see cref="IMessageChannel" />.
///     It hooks the event when a <see cref="SocketMessage" /> was added to the <see cref="IMessageChannel" />
///     Instantiate this class by invoking the Extension Method for the <see cref="IMessageChannel" />.
/// </summary>
public class MessageCollector : CollectorBase<IMessageChannel, SocketMessage>, IDisposable
{
    private readonly IMessageChannel _channel;
    private readonly IEventAggregator _eventAggregator;
    private readonly Subscription<MessageReceivedEvent> _subscription;

    public MessageCollector(IMessageChannel channel, IEventAggregator eventAggregator)
    {
        _channel = channel;
        _eventAggregator = eventAggregator;

        _subscription = _eventAggregator.Subscribe<MessageReceivedEvent>(listener: OnMessageReceived);
    }

    public new void Dispose()
    {
        _subscription.Unsubscribe();
        base.Dispose();
        GC.SuppressFinalize(this);
    }

    private void OnMessageReceived(MessageReceivedEvent eventArgs)
    {
        OnEventEmitted(eventArgs: eventArgs.Message);
    }

    protected override void OnEventEmitted(SocketMessage eventArgs)
    {
        if (eventArgs.Channel.Id != _channel.Id) return;

        base.OnEventEmitted(eventArgs: eventArgs);
    }

    // Overrides required for the return type of MessageCollector
    public override MessageCollector AddListener(Action<SocketMessage> listener)
    {
        base.AddListener(listener: listener);
        return this;
    }

    public override MessageCollector AddListener(Func<SocketMessage, Task> listener, bool awaitListener = false)
    {
        base.AddListener(listener: listener, awaitListener: awaitListener);
        return this;
    }

    public override MessageCollector WithFilter(Func<SocketMessage, bool> filterPredicate)
    {
        base.WithFilter(filterPredicate: filterPredicate);
        return this;
    }
}
using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordManager.Core.Events;

namespace DCM.Collectors;

/// <summary>
///     An observer for the <see cref="IMessage" />.
///     It hooks the event when a <see cref="SocketReaction" /> was added to the <see cref="IMessage" />
///     Instantiate this class by invoking the Extension Method for the <see cref="IMessage" />.
/// </summary>
public class ReactionCollector : CollectorBase<IMessage, SocketReaction>, IDisposable
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IMessage _message;
    private readonly Subscription<ReactionAddedEvent> _subscription;

    public ReactionCollector(
        IMessage message,
        IEventAggregator eventAggregator)
    {
        _message = message;
        _eventAggregator = eventAggregator;

        _subscription = _eventAggregator.Subscribe<ReactionAddedEvent>(listener: OnReactionAdded);
    }

    public new void Dispose()
    {
        _subscription.Unsubscribe();
        base.Dispose();
        GC.SuppressFinalize(this);
    }

    private void OnReactionAdded(ReactionAddedEvent eventArgs)
    {
        OnEventEmitted(eventArgs: eventArgs.Reaction);
    }

    protected override void OnEventEmitted(SocketReaction eventArgs)
    {
        if (eventArgs.MessageId != _message.Id) return;

        base.OnEventEmitted(eventArgs: eventArgs);
    }

    // Overrides required for the return type of ReactionCollector
    public override ReactionCollector AddListener(Action<SocketReaction> listener)
    {
        base.AddListener(listener: listener);
        return this;
    }

    public override ReactionCollector AddListener(Func<SocketReaction, Task> listener, bool awaitListener = false)
    {
        base.AddListener(listener: listener, awaitListener: awaitListener);
        return this;
    }

    public override ReactionCollector WithFilter(Func<SocketReaction, bool> filterPredicate)
    {
        base.WithFilter(filterPredicate: filterPredicate);
        return this;
    }
}
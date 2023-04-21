using DCM.Core.Interfaces;

namespace DCM.Core.Collectors;

public class CollectorBase<TEvent> : IDisposable where TEvent : class, IEvent
{
    protected CollectorBase()
    {
    }

    protected List<IDisposable> Subscriptions { get; } = new();
    protected IObservable<TEvent> Source { get; set; }
    protected IObservable<TEvent> Filtered { get; set; }

    public void Dispose()
    {
        Subscriptions
            .ForEach(x => x.Dispose());
        GC.SuppressFinalize(this);
    }

    public CollectorBase<TEvent> Filter(Func<IObservable<TEvent>, IObservable<TEvent>> filter)
    {
        Filtered = filter(Source);
        return this;
    }

    public CollectorBase<TEvent> OnCollect(Func<TEvent, Task> subscriber)
    {
        return Subscribe(x => x.Subscribe(y => subscriber(y).Wait()));
    }

    public CollectorBase<TEvent> OnCollect(Action<TEvent> subscriber)
    {
        return Subscribe(x => x.Subscribe(subscriber));
    }

    protected CollectorBase<TEvent> Subscribe(Func<IObservable<TEvent>, IDisposable> subscribe)
    {
        var observable = Filtered ?? Source;
        var subscription = subscribe(observable);
        Subscriptions.Add(subscription);
        return this;
    }
}
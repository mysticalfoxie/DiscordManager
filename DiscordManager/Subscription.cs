using System;
using System.Linq;
using System.Threading.Tasks;

namespace DCM
{
    public class Subscription<TEvent> where TEvent : Event
    {
        public Subscription(EventAggregator eventAggregator, Action<TEvent, Subscription<TEvent>> listener)
            : this(eventAggregator, (eventArgs, subscription) =>
            {
                listener(eventArgs, subscription);
                return Task.CompletedTask;
            }) { }
        public Subscription(EventAggregator eventAggregator, Func<TEvent, Subscription<TEvent>, Task> listener)
        {
            EventAggregator = eventAggregator;
            ListenerWithSubscriptionParameter = listener;
            NeesSubscriptionParameter = true;
            EventType = listener
                .Method
                .GetParameters()
                .First(x => x.ParameterType.IsSubclassOf(typeof(Event)))
                .ParameterType;
        }
        public Subscription(EventAggregator eventAggregator, Action<TEvent> listener)
            : this(eventAggregator, (eventArgs) =>
            {
                listener(eventArgs);
                return Task.CompletedTask;
            })
        { }
        public Subscription(EventAggregator eventAggregator, Func<TEvent, Task> listener)
        {
            EventAggregator = eventAggregator;
            Listener = listener;
            NeesSubscriptionParameter = false;
            EventType = listener
                .Method
                .GetParameters()
                .First()
                .ParameterType;
        }

        public EventAggregator EventAggregator;
        public bool NeesSubscriptionParameter { get; }
        public Func<TEvent, Subscription<TEvent>, Task> ListenerWithSubscriptionParameter { get; }
        public Func<TEvent, Task> Listener { get; }
        public Type EventType { get; }

        public void Unsubscribe()
            => EventAggregator.Unsubscribe(this);
    }
}

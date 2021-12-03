using System;
using System.Linq;
using System.Threading.Tasks;

namespace DCM
{
    public class Subscription
    {
        public Subscription(EventAggregator eventAggregator, Action<Event, Subscription> listener)
            : this(eventAggregator, (eventArgs, eventAggregator) =>
                new Task(() => listener(eventArgs, eventAggregator))) { }
        public Subscription(EventAggregator eventAggregator, Func<Event, Subscription, Task> listener)
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
        public Subscription(EventAggregator eventAggregator, Action<Event> listener)
            : this(eventAggregator, eventArgs => 
                new Task(() => 
                    listener(eventArgs))) {}
        public Subscription(EventAggregator eventAggregator, Func<Event, Task> listener)
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
        public Func<Event, Subscription, Task> ListenerWithSubscriptionParameter { get; }
        public Func<Event, Task> Listener { get; }
        public Type EventType { get; }

        public void Unsubscribe()
            => EventAggregator.Unsubscribe(this);
    }
}

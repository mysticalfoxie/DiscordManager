using System;
using System.Threading.Tasks;

namespace DCM.Interfaces
{
    public interface IEventAggregator
    {
        Subscription Subscribe<TEvent>(Action<TEvent> listener) where TEvent : Event;
        Subscription Subscribe<TEvent>(Func<TEvent, Task> listener) where TEvent : Event;
        Subscription Subscribe<TEvent>(Action<TEvent, Subscription> listener) where TEvent : Event;
        Subscription Subscribe<TEvent>(Func<TEvent, Subscription, Task> listener) where TEvent : Event;
        void Publish<TEvent>(TEvent eventArgs) where TEvent : Event;
        void Unsubscribe(Subscription subscription);
    }
}
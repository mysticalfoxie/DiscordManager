using DCM.Events.Logging;
using System;
using System.Threading.Tasks;

namespace DCM.Interfaces
{
    public interface IEventAggregator
    {
        Subscription<TEvent> Subscribe<TEvent>(Action<TEvent> listener) where TEvent : Event;
        Subscription<TEvent> Subscribe<TEvent>(Action<TEvent, Subscription<TEvent>> listener) where TEvent : Event;
        Subscription<TEvent> Subscribe<TEvent>(Func<TEvent, Subscription<TEvent>, Task> listener) where TEvent : Event;
        Subscription<TEvent> Subscribe<TEvent>(Func<TEvent, Task> listener) where TEvent : Event;
        void Unsubscribe<TEvent>(Subscription<TEvent> subscription) where TEvent : Event;
        void Publish<TEvent>(TEvent eventArgs) where TEvent : Event;
        Task PublishAsync<TEvent>(TEvent eventArgs) where TEvent : Event;
        IEventAggregator OnError(Action<ErrorEvent> listener);
        IEventAggregator OnError(Func<ErrorEvent, Task> listener);
        IEventAggregator OnEventPublished(Action<Type, object> listener);
        IEventAggregator OnEventPublished(Func<Type, object, Task> listener);
        IEventAggregator OnEventSubscribed(Action<Type, object> listener);
        IEventAggregator OnEventSubscribed(Func<Type, object, Task> listener);
        IEventAggregator OnEventUnsubscribed(Action<Type, object> listener);
        IEventAggregator OnEventUnsubscribed(Func<Type, object, Task> listener);
    }
}
using DCM.Events.Logging;
using DCM.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DCM
{
    public sealed class EventAggregator : IEventAggregator
    {
        private readonly List<Action<ErrorEvent>> _syncErrorListeners = new();
        private readonly List<Func<ErrorEvent, Task>> _asyncErrorListeners = new();
        private readonly List<Action<Type, object>> _syncEventPublishedListeners = new();
        private readonly List<Func<Type, object, Task>> _asyncEventPublishedListeners = new();
        private readonly List<Action<Type, object>> _syncEventSuscribedListeners = new();
        private readonly List<Func<Type, object, Task>> _asyncEventSuscribedListeners = new();
        private readonly List<Action<Type, object>> _syncEventUnsubscribedListeners = new();
        private readonly List<Func<Type, object, Task>> _asyncEventUnsubscribedListeners = new();

        public EventAggregator()
            => Subscribe<ErrorEvent>(eventArgs =>
            {
                foreach (var syncErrorListeners in _syncErrorListeners)
                    try { syncErrorListeners(eventArgs); } catch { }
                foreach (var asyncErrorListener in _asyncErrorListeners)
                    Task.Factory.StartNew(() => { try { asyncErrorListener(eventArgs); } catch { } });
            });

        private List<object> Subscriptions { get; } = new();
        private bool Locked { get; set; }

        public Subscription<TEvent> Subscribe<TEvent>(Action<TEvent> listener) where TEvent : Event
            => Subscribe(new Subscription<TEvent>(this, listener));
        public Subscription<TEvent> Subscribe<TEvent>(Action<TEvent, Subscription<TEvent>> listener) where TEvent : Event
            => Subscribe(new Subscription<TEvent>(this, listener));
        public Subscription<TEvent> Subscribe<TEvent>(Func<TEvent, Subscription<TEvent>, Task> listener) where TEvent : Event
            => Subscribe(new Subscription<TEvent>(this, listener));
        public Subscription<TEvent> Subscribe<TEvent>(Func<TEvent, Task> listener) where TEvent : Event
            => Subscribe(new Subscription<TEvent>(this, listener));

        public void Publish<TEvent>(TEvent eventArgs) where TEvent : Event
            => StartListeners(Execute(() =>
            {
                var subscriptions = CopySubscriptions<TEvent>();
                Invoke(_syncEventPublishedListeners, _asyncEventPublishedListeners, typeof(TEvent), eventArgs);
                return subscriptions;
            }),
            eventArgs);

        public async Task PublishAsync<TEvent>(TEvent eventArgs) where TEvent : Event
            => await Task.WhenAll(
                StartListeners(Execute(() =>
                {
                    var subscriptions = CopySubscriptions<TEvent>();
                    Invoke(_syncEventPublishedListeners, _asyncEventPublishedListeners, typeof(TEvent), eventArgs);
                    return subscriptions;
                }),
                eventArgs));

        public void Unsubscribe<TEvent>(Subscription<TEvent> subscription) where TEvent : Event
            => Execute(() => {
                Subscriptions.Remove(subscription);
                Invoke(_syncEventUnsubscribedListeners, _asyncEventUnsubscribedListeners, typeof(TEvent), subscription);
            });

        public IEventAggregator OnError(Action<ErrorEvent> listener) { _syncErrorListeners.Add(listener); return this; }
        public IEventAggregator OnError(Func<ErrorEvent, Task> listener) { _asyncErrorListeners.Add(listener); return this; }

        public IEventAggregator OnEventPublished(Action<Type, object> listener) { _syncEventPublishedListeners.Add(listener); return this; }
        public IEventAggregator OnEventPublished(Func<Type, object, Task> listener) { _asyncEventPublishedListeners.Add(listener); return this; }

        public IEventAggregator OnEventSubscribed(Action<Type, object> listener) { _syncEventSuscribedListeners.Add(listener); return this; }
        public IEventAggregator OnEventSubscribed(Func<Type, object, Task> listener) { _asyncEventSuscribedListeners.Add(listener); return this; }

        public IEventAggregator OnEventUnsubscribed(Action<Type, object> listener) { _syncEventUnsubscribedListeners.Add(listener); return this; }
        public IEventAggregator OnEventUnsubscribed(Func<Type, object, Task> listener) { _asyncEventUnsubscribedListeners.Add(listener); return this; }

        private Task[] StartListeners<TEvent>(object[] subscriptions, TEvent eventArgs) where TEvent : Event
        {
            var listeners = new List<Task>();

            foreach (Subscription<TEvent> subscription in subscriptions)
                listeners.Add(
                    Invoke(subscription, () =>
                        subscription.NeesSubscriptionParameter
                            ? subscription.ListenerWithSubscriptionParameter.Invoke(eventArgs, subscription)
                            : subscription.Listener.Invoke(eventArgs)
                    )
                );

            return listeners.ToArray();
        }

        private Subscription<TEvent> Subscribe<TEvent>(Subscription<TEvent> subscription) where TEvent : Event
            => Execute(() =>
            {
                Subscriptions.Add(subscription); 
                Invoke(_syncEventSuscribedListeners, _asyncEventSuscribedListeners, typeof(TEvent), subscription);                    
                return subscription;
            });

        private object[] CopySubscriptions<TEvent>()
        {
            static bool predicate(object x) => x.GetType().GetGenericArguments()[0] == typeof(TEvent);
            var subscriptions = new object[Subscriptions.Count(predicate)];
            var entries = Subscriptions.Where(predicate).ToArray();
            entries.CopyTo(subscriptions, 0);

            return subscriptions;
        }

        private Task Invoke<TEvent>(Subscription<TEvent> subscription, Func<Task> func) where TEvent : Event
            => Task.Factory.StartNew(async () =>
            {
                try
                {
                    await func();
                }
                catch
                {
                    var message = $"An error occurred when invoking the event listener for event '{subscription.EventType.Name}'.";
                    try { Publish<ErrorEvent>(new(new(message))); } catch { }
                    Trace.WriteLine(message);
                }
            });

        private static void Invoke(IEnumerable<Action<Type, object>> syncListeners, IEnumerable<Func<Type, object, Task>> asyncListeners, Type type, object obj)
        {
            foreach (var syncListener in syncListeners)
                try { syncListener.Invoke(type, obj); } catch { }
            foreach (var asyncListener in asyncListeners)
                Task.Factory.StartNew(() => { try { asyncListener(type, obj); } catch { } });
        }

        private void Execute(Action action)
        {
            while (Locked) continue;
            Locked = true;
            action();
            Locked = false;
        }
        private T Execute<T>(Func<T> func)
        {
            T result = default;
            Execute(() => { result = func(); });
            return result;
        }
    }
}

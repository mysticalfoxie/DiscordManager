using DCM.Events.Logging;
using DCM.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DCM
{
    public sealed class EventAggregator : IEventAggregator
    {
        private readonly object _locker = new();
        private bool Locked => !Monitor.TryEnter(_locker);
        private List<Subscription> Subscriptions { get; } = new();

        public Subscription Subscribe<TEvent>(Action<TEvent> listener) where TEvent : Event
            => Subscribe((Action<Event>)listener);
        public Subscription Subscribe<TEvent>(Func<TEvent, Task> listener) where TEvent : Event
            => Subscribe((Func<Event, Task>)listener);
        public Subscription Subscribe<TEvent>(Action<TEvent, Subscription> listener) where TEvent : Event
            => Subscribe((Action<Event, Subscription>)listener);
        public Subscription Subscribe<TEvent>(Func<TEvent, Subscription, Task> listener) where TEvent : Event
            => Subscribe((Func<Event, Subscription, Task>)listener);
        private Subscription Subscribe(Action<Event, Subscription> listener)
            => Subscribe(new Subscription(this, listener));
        private Subscription Subscribe(Func<Event, Subscription, Task> listener)
            => Subscribe(new Subscription(this, listener));
        private Subscription Subscribe(Action<Event> listener)
            => Subscribe(new Subscription(this, listener));
        private Subscription Subscribe(Func<Event, Task> listener)
            => Subscribe(new Subscription(this, listener));
        private Subscription Subscribe(Subscription subscription)
        {
            while (Locked) continue;
            lock (_locker)
                Subscriptions.Add(subscription);

            return subscription;
        }

        public void Publish<TEvent>(TEvent eventArgs) where TEvent : Event
            => Publish((Event)eventArgs);
        private void Publish(Event eventArgs)
        {
            while (Locked) continue;
            lock (_locker)
            {
                var subscriptions = Subscriptions.Where(x => x.EventType == eventArgs.GetType());
                foreach (var subscription in subscriptions)
                {
                    Task.Factory.StartNew(async () =>
                    {
                        try
                        {
                            await subscription.Listener(eventArgs);
                        }
                        catch
                        {
                            var message = $"An occurred an event listener for event of type '{subscription.EventType.Name}'.";
                            try { Publish<ErrorEvent>(new(new(message))); } catch { }
                            Trace.WriteLine(message);
                        }
                    });
                }
            }
        }

        public void Unsubscribe(Subscription subscription)
        {
            while (Locked) continue;
            lock (_locker)
                Subscriptions.Remove(subscription);
        }
    }
}

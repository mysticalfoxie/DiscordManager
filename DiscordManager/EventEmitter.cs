using DCM.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DCM
{
    public class EventEmitter : IEventEmitter
    {
        private static readonly int _maxTries = 3;
        private static readonly object _lockObj = new();
        private ConcurrentDictionary<Type, List<object>> Events { get; } = new ConcurrentDictionary<Type, List<object>>();

        public void AddListener<TEvent>(Action<TEvent> listener) where TEvent : IEvent
        {
            if (Events.TryGetValue(typeof(TEvent), out var listeners))
                listeners.Add(listener);
            else
                Events.TryAdd(typeof(TEvent), new List<object>() { listener });
        }

        public void AddListeners<TEvent>(IEnumerable<Action<TEvent>> listeners) where TEvent : IEvent
        {
            TryRun(() =>
            {
                lock (_lockObj)
                {
                    foreach (var listener in listeners)
                        AddListener(listener);
                }
            });
        }

        public void Emit<TEvent>(TEvent args) where TEvent : IEvent
        {
            TryRun(() =>
            {
                if (!Events.TryGetValue(typeof(TEvent), out var listeners))
                    return;

                lock (_lockObj)
                {
                    foreach (Action<TEvent> listener in listeners)
                        listener.Invoke(args);
                }
            });
        }

        public void RemoveListener<TEvent>(Action<TEvent> listener) where TEvent : IEvent
        {
            TryRun(() =>
            {
                if (!Events.TryGetValue(typeof(TEvent), out List<object> listeners))
                    return;

                listeners.Remove(listener);
            });
        }

        public void RemoveListeners<TEvent>(IEnumerable<Action<TEvent>> listeners) where TEvent : IEvent
        {
            TryRun(() =>
            {
                if (!Events.TryGetValue(typeof(TEvent), out var eventListeners))
                    return;

                foreach (var listener in listeners)
                    eventListeners.Remove(listener);
            });
        }

        private static bool TryRun(Action method)
        {
            for (var i = 0; i < _maxTries; i++)
                try
                {
                    method();
                    return true;
                }
                catch (InvalidOperationException) { }

            return false;
        }
    }
}

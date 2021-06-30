using System;
using System.Collections.Generic;

namespace DCM.Interfaces
{
    public interface IEventEmitter
    {
        void AddListener<TEvent>(Action<TEvent> listener) where TEvent : IEvent;
        void AddListeners<TEvent>(IEnumerable<Action<TEvent>> listeners) where TEvent : IEvent;
        void Emit<TEvent>(TEvent args) where TEvent : IEvent;
        void RemoveListener<TEvent>(Action<TEvent> listener) where TEvent : IEvent;
        void RemoveListeners<TEvent>(IEnumerable<Action<TEvent>> listeners) where TEvent : IEvent;
    }
}
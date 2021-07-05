using DCM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DCM.Collectors
{
    public abstract class CollectorBase<TSource, TEventArgs> : IDisposable
    {
        private readonly List<Func<TEventArgs, bool>> _filters = new();
        private readonly List<Action<TEventArgs>> _listeners = new();
        private readonly List<(Func<TEventArgs, Task> listener, bool awaitListener)> _asyncListeners = new();
        private readonly CancellationTokenSource _listenerCts = new();

        public event Action<TEventArgs> Collect;

        public virtual CollectorBase<TSource, TEventArgs> AddListener(Action<TEventArgs> listener)
        {
            if (listener is null) throw new ArgumentNullException(nameof(listener));
            _listeners.Add(listener);
            return this;
        }

        public virtual CollectorBase<TSource, TEventArgs> AddListener(Func<TEventArgs, Task> listener, bool awaitListener = false)
        {
            if (listener is null) throw new ArgumentNullException(nameof(listener));
            _asyncListeners.Add((listener, awaitListener));
            return this;
        }

        public virtual CollectorBase<TSource, TEventArgs> WithFilter(Func<TEventArgs, bool> filterPredicate)
        {
            _filters.Add(filterPredicate ?? throw new ArgumentNullException(nameof(filterPredicate)));
            return this;
        }

        protected virtual void OnEventEmitted(TEventArgs eventArgs)
        {
            if (_filters.Any(filter => filter.Invoke(eventArgs) == false)) return;

            foreach (var listener in _listeners)
                listener.Invoke(eventArgs);

            foreach (var (listener, awaitRequested) in _asyncListeners)
            {
                try
                {
                    var task = listener.Invoke(eventArgs);
                    if (awaitRequested)
                        task.Wait(_listenerCts.Token);
                    else
                        Task.Run(async () => await task, _listenerCts.Token);
                }
                catch (TaskCanceledException) { }
                catch (OperationCanceledException) { }
            }

            Collect?.Invoke(eventArgs);
        }

        public void Dispose()
        {
            _listenerCts.Cancel();
            _listenerCts.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

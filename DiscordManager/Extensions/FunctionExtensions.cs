using System;
using System.Threading;
using System.Threading.Tasks;

namespace DCM.Extensions
{
    internal static class FunctionExtensions
    {
        public static Task StartHandled(this Func<Task> asyncMethod, CancellationToken token = default)
        {
            var task = Task.Factory.StartNew(asyncMethod, token)
                                   .GetAwaiter()
                                   .GetResult();

            if (task.IsFaulted)
                throw task.Exception.GetBaseException();
            else
                return task;
        }

        public static Task StartHandled<T>(this Func<T, Task> asyncMethod, T parameter, CancellationToken token = default)
        {
            var task = Task.Factory.StartNew(() => asyncMethod(parameter), token)
                                   .GetAwaiter()
                                   .GetResult();

            if (task.IsFaulted)
                throw task.Exception.GetBaseException();
            else
                return task;
        }
    }
}

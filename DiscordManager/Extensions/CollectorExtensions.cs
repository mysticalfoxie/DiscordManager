using DCM.Collectors;
using DCM.Interfaces;
using Discord;
using Discord.WebSocket;
using System.Threading;
using System.Threading.Tasks;

namespace DCM.Extensions
{
    public static class CollectorExtensions
    {
        public static ReactionCollector CreateReactionCollector(this IMessage message, IEventAggregator eventEmitter)
            => new(message, eventEmitter);

        public static MessageCollector CreateMessageCollector(this IMessageChannel channel, IEventAggregator eventEmitter)
            => new(channel, eventEmitter);

        /// <exception cref="TaskCanceledException"></exception>
        public static Task<SocketReaction> WaitForReaction(this ReactionCollector collector, CancellationToken token = default)
        {
            var tcs = new TaskCompletionSource<SocketReaction>();

            collector.Collect += Collector_ReactionAdded;
            void Collector_ReactionAdded(SocketReaction reaction)
            {
                tcs.SetResult(reaction);
                collector.Collect -= Collector_ReactionAdded;
                collector.Dispose();
            }

            if (token != default)
                Task.Factory.StartNew(() =>
                {
                    while (!token.IsCancellationRequested)
                        continue;

                    tcs.TrySetCanceled();
                    collector.Collect -= Collector_ReactionAdded;

                }, CancellationToken.None);

            return tcs.Task;
        }

        /// <exception cref="TaskCanceledException"></exception>
        public static Task<SocketMessage> WaitForMessage(this MessageCollector collector, CancellationToken token = default)
        {
            var tcs = new TaskCompletionSource<SocketMessage>();

            collector.Collect += Collector_MessageReceived;
            void Collector_MessageReceived(SocketMessage reaction)
            {
                tcs.SetResult(reaction);
                collector.Collect -= Collector_MessageReceived;
                collector.Dispose();
            }

            if (token != default)
                Task.Factory.StartNew(() =>
                {
                    while (!token.IsCancellationRequested)
                        continue;

                    tcs.TrySetCanceled();
                    collector.Collect -= Collector_MessageReceived;

                }, CancellationToken.None);

            return tcs.Task;
        }
    }
}

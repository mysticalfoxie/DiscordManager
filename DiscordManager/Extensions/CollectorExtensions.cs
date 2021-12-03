using DCM.Collectors;
using DCM.Interfaces;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DCM.Extensions
{
    public static class CollectorExtensions
    {
        public static ReactionCollector CreateReactionCollector(this IMessage message, IEventAggregator eventEmitter)
            => new(message, eventEmitter);

        public static MessageCollector CreateMessageCollector(this IMessageChannel channel, IEventAggregator eventEmitter)
            => new(channel, eventEmitter);

        public static Task<SocketReaction> WaitForReaction(this ReactionCollector collector)
        {
            var tcs = new TaskCompletionSource<SocketReaction>();

            collector.Collect += Collector_ReactionAdded;
            void Collector_ReactionAdded(SocketReaction reaction)
            {
                tcs.SetResult(reaction);
                collector.Collect -= Collector_ReactionAdded;
                collector.Dispose();
            }

            return tcs.Task;
        }

        public static Task<SocketMessage> WaitForMessage(this MessageCollector collector)
        {
            var tcs = new TaskCompletionSource<SocketMessage>();

            collector.Collect += Collector_MessageReceived;
            void Collector_MessageReceived(SocketMessage reaction)
            {
                tcs.SetResult(reaction);
                collector.Collect -= Collector_MessageReceived;
                collector.Dispose();
            }

            return tcs.Task;
        }
    }
}

using DCM.Collectors;
using DCM.Interfaces;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DCM.Extensions
{
    public static class CollectorExtensions
    {
        public static ReactionCollector CreateReactionCollector(this IMessage message, IEventEmitter eventEmitter)
            => new(eventEmitter, message);

        public static MessageCollector CreateMessageCollector(this ISocketMessageChannel channel, IEventEmitter eventEmitter)
            => new(eventEmitter, channel);

        public static Task<SocketReaction> WaitForReaction(this ReactionCollector collector)
        {
            var tcs = new TaskCompletionSource<SocketReaction>();

            collector.ReactionAdded += Collector_ReactionAdded;
            void Collector_ReactionAdded(SocketReaction reaction)
            {
                tcs.SetResult(reaction);
                collector.ReactionAdded -= Collector_ReactionAdded;
                collector.Dispose();
            }

            return tcs.Task;
        }

        public static Task<SocketMessage> WaitForMessage(this MessageCollector collector)
        {
            var tcs = new TaskCompletionSource<SocketMessage>();

            collector.MessageReceived += Collector_MessageReceived;
            void Collector_MessageReceived(SocketMessage reaction)
            {
                tcs.SetResult(reaction);
                collector.MessageReceived -= Collector_MessageReceived;
                collector.Dispose();
            }

            return tcs.Task;
        }
    }
}

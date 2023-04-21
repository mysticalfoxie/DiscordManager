using DCM.Core.Services;
using Discord;

namespace DCM.Core.Extensions;

public static class MessageChannelExtensions
{
    public static IAsyncEnumerable<IMessage[]> EnumerateMessages(
        this IMessageChannel channel,
        int batchSize = 100,
        ulong? startMessageId = null,
        int? delay = null)
    {
        return DiscordService.EnumerateMessages(channel, batchSize, startMessageId, delay);
    }
}
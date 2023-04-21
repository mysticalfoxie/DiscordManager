using System.Reactive.Linq;
using System.Reactive.Subjects;
using DCM.Core.Builders;
using DCM.Core.Collectors;
using DCM.Core.Events;
using DCM.Core.Extensions;
using DCM.Core.Interfaces;
using Discord;

namespace DCM.Core.Services;

public class DiscordService : IDiscordService
{
    private readonly IDiscordClientService _clientService;
    private readonly IEventService _eventService;

    public DiscordService(
        IEventService eventService,
        IDiscordClientService clientService)
    {
        _eventService = eventService;
        _clientService = clientService;
    }

    public async Task BulkDelete(IEnumerable<IDeletable> deletable)
    {
        var tasks = deletable
            .AsParallel()
            .Select(x => x.DeleteSafe())
            .ToArray();

        await Task.WhenAll(tasks);
    }

    public MessageCollector CreateMessageCollector(IMessageChannel channel)
    {
        return new MessageCollector(channel, _eventService);
    }

    public ReactionCollector CreateReactionCollector(IMessage message)
    {
        return new ReactionCollector(message, _eventService);
    }

    public async Task<Subject<SlashCommandExecutedEvent>> CreateSlashCommand(
        IGuild guild,
        Action<DCMSlashCommandBuilder> configure)
    {
        var builder = new DCMSlashCommandBuilder();
        configure(builder);
        var dcmCommandInfo = builder.Build();
        var dcCommandInfo = dcmCommandInfo.Properties;
        var command = await guild.CreateApplicationCommandAsync(dcCommandInfo);
        var subject = new Subject<SlashCommandExecutedEvent>();
        var subscription = _eventService.SlashCommandExecuted
            .Where(x => x.Command.Id == command.Id)
            .Subscribe(x => subject.OnNext(x));

        subject.Subscribe(_ => { }, () =>
        {
            subscription.Dispose();
            command.DeleteSafe().Wait();
        });

        if (dcmCommandInfo.SyncSubscriber is not null)
            subject.Subscribe(dcmCommandInfo.SyncSubscriber);
        if (dcmCommandInfo.AsyncSubscriber is not null)
            subject.Subscribe(x => dcmCommandInfo.AsyncSubscriber(x).Wait());

        return subject;
    }

    public IGuild GetRequiredGuild(ulong id)
    {
        if (id == default)
            throw new ArgumentNullException(nameof(id));

        return _clientService.Client.GetGuild(id)
               ?? throw new NullReferenceException();
    }

    public static async IAsyncEnumerable<IMessage[]> EnumerateMessages(
        IMessageChannel channel,
        int batchSize = 100,
        ulong? startMessageId = null,
        int? delay = null)
    {
        IMessage[] batch;
        IMessage last = null;

        do
        {
            batch = await GetMessagesBatch(
                    channel,
                    batchSize,
                    startMessageId,
                    last?.Id ?? 0)
                .ToArrayAsync();

            if (batch.Length == 0)
                break;

            last = batch.Last();

            yield return batch;

            if (delay.HasValue)
                await Task.Delay(delay.Value);
        } while (batch.Length == batchSize);
    }

    private static IAsyncEnumerable<IMessage> GetMessagesBatch(
        IMessageChannel channel,
        int batchSize,
        ulong? startMessageId,
        ulong lastMessage)
    {
        if (lastMessage != default)
            return channel
                .GetMessagesAsync(
                    limit: batchSize,
                    fromMessageId: lastMessage,
                    dir: Direction.Before)
                .Flatten();

        if (startMessageId.HasValue)
            return channel
                .GetMessagesAsync(
                    limit: batchSize,
                    fromMessageId: startMessageId.Value,
                    dir: Direction.Before)
                .Flatten();

        return channel
            .GetMessagesAsync(batchSize)
            .Flatten();
    }
}
using System.Reactive;
using System.Reactive.Subjects;
using DCM.Core.Attributes;
using DCM.Core.Extensions;
using DCM.Core.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Core.Services;

[Injectable(typeof(IDiscordService))]
public class DiscordService : IDisposable, IDiscordService
{
    private readonly IConfigService _configService;
    private readonly ICredentialsService _credentials;

    public DiscordService(
        IConfigService configService,
        ICredentialsService credentials)
    {
        _configService = configService;
        _credentials = credentials;

        Connect.Subscribe(_ => Running = true);
        Disconnect.Subscribe(_ => Running = false);
    }

    public DiscordSocketClient Client { get; private set; }

    public bool Running { get; private set; }

    public ISubject<Unit> Connect { get; } = new Subject<Unit>();
    public ISubject<Unit> Disconnect { get; } = new Subject<Unit>();

    public void Build()
    {
        if (string.IsNullOrWhiteSpace(value: _credentials.LoginToken))
            throw new InvalidOperationException("The bot is missing a login token.");

        var config = _configService.ReadSocketConfig();

        Client = new DiscordSocketClient(config: config);
        Client.Connected += () => Task.Run(() => Connect.OnNext(value: Unit.Default));
        Client.Disconnected += _ => Task.Run(() => Disconnect.OnNext(value: Unit.Default));
    }

    public async Task StartAsync()
    {
        if (Client is null)
            throw new InvalidOperationException("The client is not built yet!");

        await Client.LoginAsync(tokenType: TokenType.Bot, token: _credentials.LoginToken);
        await Client.StartAsync();
        await Client.WaitForReadyEvent();
    }

    public async Task StopAsync()
    {
        if (Client is null)
            throw new InvalidOperationException("The client is not running.");

        await Client.StopAsync();
        await Client.DisposeAsync();
    }

    public void Dispose()
    {
        Client?.StopAsync();
        Client?.Dispose();
        GC.SuppressFinalize(this);
    }
}
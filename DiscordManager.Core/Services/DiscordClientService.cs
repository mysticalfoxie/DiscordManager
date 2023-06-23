using System.Reactive;
using System.Reactive.Subjects;
using DCM.Core.Attributes;
using DCM.Core.Extensions;
using DCM.Core.Interfaces;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace DCM.Core.Services;

[Injectable(typeof(IDiscordClientService))]
public class DiscordClientService : IDisposable, IDiscordClientService
{
    private readonly IConfigService _configService;
    private readonly ICredentialsService _credentials;
    private readonly ILogger<DiscordClientService> _logger;

    public DiscordClientService(
        IConfigService configService,
        ILogger<DiscordClientService> logger,
        ICredentialsService credentials)
    {
        _configService = configService;
        _logger = logger;
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
        if (string.IsNullOrWhiteSpace(_credentials.LoginToken))
            throw new InvalidOperationException("The bot is missing a login token.");

        var config = _configService.ReadSocketConfig();

        Client = new DiscordSocketClient(config);
        Client.Connected += () => Task.Run(() => Connect.OnNext(Unit.Default));
        Client.Disconnected += _ => Task.Run(() => Disconnect.OnNext(Unit.Default));
    }

    public async Task StartAsync()
    {
        if (Client is null)
            throw new InvalidOperationException("The client is not built yet!");

        await Client.LoginAsync(TokenType.Bot, _credentials.LoginToken);
        _logger.LogTrace("Discord client logged in");
        await Client.StartAsync();
        _logger.LogTrace("Discord client started");
        await Client.WaitForReadyEvent();
        _logger.LogTrace("Discord received 'READY' feedback");

        _logger.LogInformation("Discord client has been started");
    }

    public async Task StopAsync()
    {
        if (Client is null)
            throw new InvalidOperationException("The client is not running.");

        await Client.StopAsync();
        await Client.DisposeAsync();

        _logger.LogInformation("Discord client has been stopped");
    }

    public void Dispose()
    {
        Client?.StopAsync();
        Client?.Dispose();
        GC.SuppressFinalize(this);
    }
}
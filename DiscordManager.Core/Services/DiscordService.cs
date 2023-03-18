using DCM.Core.Extensions;
using DCM.Core.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DCM.Core.Services;

public class DiscordService : IDisposable, IDiscordService
{
    private readonly IDiscordConfigService _configService;
    private readonly ICredentialsService _credentials;

    public DiscordService(
        IDiscordConfigService configService,
        ICredentialsService credentials)
    {
        _configService = configService;
        _credentials = credentials;
    }

    public DiscordSocketClient Client { get; set; }

    public void Dispose()
    {
        Client?.StopAsync();
        Client?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task StartAsync()
    {
        if (Client is not null)
            throw new InvalidOperationException("The client cannot be started twice!");

        Client = new DiscordSocketClient(config: _configService.Config);
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
}
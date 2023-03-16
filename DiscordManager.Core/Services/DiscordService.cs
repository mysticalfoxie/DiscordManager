using Discord;
using Discord.WebSocket;
using DiscordManager.Core.Extensions;
using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Services;

internal class DiscordService : IDisposable
{
    private readonly IDiscordConfigService _configService;
    private readonly ICredentialsService _credentials;

    private DiscordSocketClient _client;

    public DiscordService(
        IDiscordConfigService configService,
        ICredentialsService credentials)
    {
        _configService = configService;
        _credentials = credentials;
    }

    public void Dispose()
    {
        _client?.StopAsync();
        _client?.Dispose();
    }

    public async Task StartAsync(string loginToken)
    {
        if (_client is not null)
            throw new InvalidOperationException("The client cannot be started twice!");

        _client = new DiscordSocketClient(config: _configService.Config);
        await _client.LoginAsync(tokenType: TokenType.Bot, token: _credentials.LoginToken);
        await _client.StartAsync();
        await _client.WaitForReadyEvent();
    }

    public async Task StopAsync()
    {
        if (_client is null)
            throw new InvalidOperationException("The client is not running.");

        await _client.StopAsync();
        await _client.DisposeAsync();
    }
}
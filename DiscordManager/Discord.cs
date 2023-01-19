using Discord;
using Discord.WebSocket;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DCM
{
    internal class Discord : IDisposable
    {
        private DiscordSocketClient _client;
        public DiscordSocketClient Client => _client ??= new(Config);
        public DiscordSocketConfig Config { get; } = new();

        public async Task StartClient(string loginToken)
        {
            try
            {
                using var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(30 * 1000);
                await Start(loginToken, cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException("The discord client has still not received the READY event. Operation timed out.");
            }
        }

        private async Task Start(string loginToken, CancellationToken token)
        {
            var ready = false;

            Client.Ready += OnReady;

            await Client.LoginAsync(TokenType.Bot, loginToken);
            await Client.StartAsync();

            while (!ready && !token.IsCancellationRequested) 
                continue;

            if (token.IsCancellationRequested)
                throw new OperationCanceledException();

            Client.Ready -= OnReady;

            Task OnReady()
            {
                ready = true;
                return Task.CompletedTask;
            }
        }

        public void Dispose()
        {
            try { _client?.StopAsync(); } catch { }
            _client?.Dispose();
        }
    }
}

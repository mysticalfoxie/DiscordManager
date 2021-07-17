using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace DCM
{
    internal class Discord : IDisposable
    {
        private bool _isReady;
        public DiscordSocketClient Client { get; } = new();

        public async Task StartClient(string loginToken)
        {
            Client.Ready += OnReady;

            await Client.LoginAsync(TokenType.Bot, loginToken);
            await Client.StartAsync();

            while (!_isReady) continue;

            Client.Ready -= OnReady;
        }

        private Task OnReady()
        {
            _isReady = true;
            return Task.CompletedTask;
        }

        public void Dispose()
            => Client.Dispose();
    }
}

using DCM;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    { 
        static async Task Main()
            => await new DiscordManager()
                .WithCredentials(new("NTIwMzAyMzYyMzk3NjM4Njgy.XAlnVQ.pSjx6Dq_pUJC6n6r88BRxcYSvVY"))
                .ConfigureCommands(x => x
                    .UsePrefix('!')
                    .IgnoreCasing() 
                    .Bind<MyCommandHandler>("PinG", options => options
                        .AddAliases(new string[] { "connect", "healthcheck" })))
                .StartAsync();
    }

    public class MyCommandHandler : CommandHandler
    {
        public override async Task HandleAsync(SocketMessage message)
            => await message.AddReactionAsync(new Emoji("🏓"));
    }
}

using System.Reactive.Linq;
using Discord.WebSocket;

namespace DiscordManager.Core.Extensions;

public static class DiscordClientExtensions
{
    public static Task WaitForReadyEvent(this DiscordSocketClient client)
    {
        var tcs = new TaskCompletionSource();

        Observable
            .Timer(TimeSpan.FromSeconds(10))
            .Where(x => tcs.Task.IsCompleted)
            .Subscribe(x =>
            {
                var ex = new TimeoutException("The discord client didnt receive within 10 seconds the ready event.");
                tcs.SetException(exception: ex);
            });

        client.Ready += () =>
        {
            tcs.SetResult();
            return Task.CompletedTask;
        };

        return tcs.Task;
    }
}
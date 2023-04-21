using System.Reactive.Linq;
using Discord.WebSocket;

namespace DCM.Core.Extensions;

internal static class DiscordClientExtensions
{
    public static Task WaitForReadyEvent(this DiscordSocketClient client)
    {
        var tcs = new TaskCompletionSource();

        Observable
            .Timer(TimeSpan.FromSeconds(10))
            .Where(_ => !tcs.Task.IsCompleted)
            .Subscribe(_ =>
            {
                var ex = new TimeoutException("The discord client didnt receive within 10 seconds the ready event.");
                tcs.SetException(ex);
            });

        client.Ready += () =>
        {
            tcs.SetResult();
            return Task.CompletedTask;
        };

        return tcs.Task;
    }
}
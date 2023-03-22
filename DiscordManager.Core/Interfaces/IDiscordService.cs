using System.Reactive;
using System.Reactive.Subjects;
using Discord.WebSocket;

namespace DCM.Core.Interfaces;

public interface IDiscordService
{
    Task StartAsync();
    Task StopAsync();
    DiscordSocketClient Client { get; }
    bool Running { get; }
    ISubject<Unit> Connect { get; }
    ISubject<Unit> Disconnect { get; }
    void Build();
}
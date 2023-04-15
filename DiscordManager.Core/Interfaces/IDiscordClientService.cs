using System.Reactive;
using System.Reactive.Subjects;
using Discord.WebSocket;

namespace DCM.Core.Interfaces;

public interface IDiscordClientService
{
    DiscordSocketClient Client { get; }
    bool Running { get; }
    ISubject<Unit> Connect { get; }
    ISubject<Unit> Disconnect { get; }
    void Build();
    Task StartAsync();
    Task StopAsync();
}
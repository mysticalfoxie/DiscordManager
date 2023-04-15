using DCM.Core.Interfaces;
using Discord;

namespace DCM.Core.Services;

public class DiscordService : IDiscordService
{
    private readonly IDiscordClientService _clientService;

    public DiscordService(IDiscordClientService clientService)
    {
        _clientService = clientService;
    }

    public IGuild GetRequiredGuild(ulong id)
    {
        if (id == default)
            throw new ArgumentNullException(nameof(id));

        return _clientService.Client.GetGuild(id)
               ?? throw new NullReferenceException();
    }
}
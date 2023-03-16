using DiscordManager.Core.Interfaces;

namespace DiscordManager.Core.Services;

public class CredentialsService : ICredentialsService
{
    public string LoginToken { get; set; }
}
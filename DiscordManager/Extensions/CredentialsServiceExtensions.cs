using System;

namespace DCM.Extensions;

public static class CredentialsServiceExtensions
{
    public static DiscordManager SetToken(this DiscordManager dcm, string token)
    {
        if (!string.IsNullOrWhiteSpace(token))
            throw new ArgumentNullException(nameof(token));

        dcm.Services.CredentialsService.LoginToken = token;

        return dcm;
    }
}
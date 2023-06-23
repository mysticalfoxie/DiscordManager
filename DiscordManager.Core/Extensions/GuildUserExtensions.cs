using Discord;

namespace DCM.Core.Extensions;

public static class GuildUserExtensions
{
    public static bool IsAdministrator(this IGuildUser user)
    {
        return user.Guild.Roles.Where(r => user.RoleIds.Contains(r.Id)).Any(r => r.Permissions.Administrator);
    }
}
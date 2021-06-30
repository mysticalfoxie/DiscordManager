using Discord;
using System.Linq;

namespace DCM.Extensions
{
    public static class UserExtensions
    {
        /// <summary>
        /// Checks if the user has in any role the permission 'administrator'.
        /// </summary>
        public static bool IsAdmin(this IGuildUser user)
            => user.Guild.Roles.Where(r => user.RoleIds.Contains(r.Id)).Any(r => r.Permissions.Administrator);
    }
}

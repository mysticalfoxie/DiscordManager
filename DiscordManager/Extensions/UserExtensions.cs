using DCM.Exceptions;
using Discord;
using System.Linq;
using System.Threading.Tasks;

namespace DCM.Extensions
{
    public static class UserExtensions
    {
        /// <summary>
        /// Checks if the user has in any role the permission 'administrator'.
        /// </summary>
        public static bool IsAdmin(this IGuildUser user)
            => user.Guild.Roles.Where(r => user.RoleIds.Contains(r.Id)).Any(r => r.Permissions.Administrator);

        /// <summary>
        /// Tries receiving the avatar of the user. if it cannot find a specified avatar it returns the default avatar.
        /// </summary>
        public static string GetAnyAvatarUrl(this IUser user, ImageFormat format = ImageFormat.Auto, ushort size = 128)
            => user.GetAvatarUrl(format, size) ?? user.GetDefaultAvatarUrl();

        /// <summary>
        /// Gets a private channel with the provided ID.
        /// Throws an exception if there is no private channel with this ID.
        /// </summary>
        /// <exception cref="ChannelNotFoundException">The exception if the private channel was not found.</exception>
        public static async Task<IPrivateChannel> GetRequiredPrivateChannelAsync(this IDiscordClient client, ulong id)
            => await client.GetPrivateChannelAsync(id)
                ?? throw new ChannelNotFoundException(id);

        /// <summary>
        /// Gets a guild with the provided ID.
        /// Throws an exception if there is no guild with this ID.
        /// </summary>
        /// <exception cref="GuildNotFoundException">The exception if the guild was not found.</exception>
        public static async Task<IGuild> GetRequiredGuildAsync(
            this IDiscordClient client, 
            ulong id, 
            CacheMode mode = CacheMode.AllowDownload, 
            RequestOptions options = null)
            => await client.GetGuildAsync(id, mode, options)
                ?? throw new GuildNotFoundException(id);

        /// <summary>
        /// Gets a user with the provided ID.
        /// Throws an exception if there is no user with this ID.
        /// </summary>
        /// <exception cref="UserNotFoundException">The exception if the user was not found.</exception>
        public static async Task<IUser> GetRequiredUserAsync(this IDiscordClient client, ulong id, CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
            => await client.GetUserAsync(id, mode, options)
                ?? throw new UserNotFoundException(id);
    }
}

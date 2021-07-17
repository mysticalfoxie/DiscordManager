using DCM.Exceptions;
using Discord;
using System.Threading.Tasks;

namespace DCM.Extensions
{
    public static class GuildExtensions
    {
        /// <summary>
        /// Gets a text channel with the provided ID.
        /// Throws an exception if there is no text channel with this ID.
        /// </summary>
        /// <exception cref="ChannelNotFoundException">The exception if the text channel was not found.</exception>
        public static async Task<ITextChannel> GetRequiredTextChannelAsync(
            this IGuild guild, 
            ulong id, 
            CacheMode mode = CacheMode.AllowDownload,
            RequestOptions options = null)
            => await guild.GetTextChannelAsync(id, mode, options)
                ?? throw new ChannelNotFoundException(id);

        /// <summary>
        /// Gets a voice channel with the provided ID.
        /// Throws an exception if there is no voice channel with this ID.
        /// </summary>
        /// <exception cref="ChannelNotFoundException">The exception if the voice channel was not found.</exception>
        public static async Task<IVoiceChannel> GetRequiredVoiceChannelAsync(
            this IGuild guild,
            ulong id,
            CacheMode mode = CacheMode.AllowDownload,
            RequestOptions options = null)
            => await guild.GetVoiceChannelAsync(id, mode, options)
                ?? throw new ChannelNotFoundException(id);

        /// <summary>
        /// Gets a channel with the provided ID.
        /// Throws an exception if there is no channel with this ID.
        /// </summary>
        /// <exception cref="ChannelNotFoundException">The exception if the channel was not found.</exception>
        public static async Task<IGuildChannel> GetRequiredChannelAsync(
            this IGuild guild,
            ulong id,
            CacheMode mode = CacheMode.AllowDownload,
            RequestOptions options = null)
            => await guild.GetChannelAsync(id, mode, options)
                ?? throw new ChannelNotFoundException(id);

        /// <summary>
        /// Gets an emote with the provided ID.
        /// Throws an exception if there is no emote with this ID.
        /// </summary>
        /// <exception cref="EmoteNotFoundException">The exception if the emote was not found.</exception>
        public static async Task<GuildEmote> GetRequiredEmoteAsync(
            this IGuild guild,
            ulong id,
            RequestOptions options = null)
            => await guild.GetEmoteAsync(id, options)
                ?? throw new EmoteNotFoundException(id);

        /// <summary>
        /// Gets a role with the provided ID.
        /// Throws an exception if there is no role with this ID.
        /// </summary>
        /// <exception cref="RoleNotFoundException">The exception if the role was not found.</exception>
        public static IRole GetRequiredRole(
            this IGuild guild,
            ulong id)
            => guild.GetRole(id)
                ?? throw new RoleNotFoundException(id);

        /// <summary>
        /// Gets a guild user with the provided ID.
        /// Throws an exception if there is no  guild user with this ID.
        /// </summary>
        /// <exception cref="UserNotFoundException">The exception if the guild user was not found.</exception>
        public static async Task<IGuildUser> GetRequiredUserAsync(
            this IGuild guild,
            ulong id,
            CacheMode mode = CacheMode.AllowDownload,
            RequestOptions options = null)
            => await guild.GetUserAsync(id, mode, options)
                ?? throw new UserNotFoundException(id);
    }
}

using Discord;

namespace DCM.Core.Extensions;

public static class GuildExtensions
{
    public static async Task<ITextChannel> GetRequiredTextChannelAsync(
        this IGuild guild,
        ulong id,
        CacheMode mode = CacheMode.AllowDownload,
        RequestOptions options = null)
    {
        return await guild.GetTextChannelAsync(id: id, mode: mode, options: options)
               ?? throw new NullReferenceException(nameof(id));
    }

    public static async Task<IVoiceChannel> GetRequiredVoiceChannelAsync(
        this IGuild guild,
        ulong id,
        CacheMode mode = CacheMode.AllowDownload,
        RequestOptions options = null)
    {
        return await guild.GetVoiceChannelAsync(id: id, mode: mode, options: options)
               ?? throw new NullReferenceException(nameof(id));
    }

    public static async Task<IGuildChannel> GetRequiredChannelAsync(
        this IGuild guild,
        ulong id,
        CacheMode mode = CacheMode.AllowDownload,
        RequestOptions options = null)
    {
        return await guild.GetChannelAsync(id: id, mode: mode, options: options)
               ?? throw new NullReferenceException(nameof(id));
    }

    public static async Task<GuildEmote> GetRequiredEmoteAsync(
        this IGuild guild,
        ulong id,
        RequestOptions options = null)
    {
        return await guild.GetEmoteAsync(id: id, options: options)
               ?? throw new NullReferenceException(nameof(id));
    }

    public static IRole GetRequiredRole(
        this IGuild guild,
        ulong id)
    {
        return guild.GetRole(id: id)
               ?? throw new NullReferenceException(nameof(id));
    }

    public static async Task<IGuildUser> GetRequiredUserAsync(
        this IGuild guild,
        ulong id,
        CacheMode mode = CacheMode.AllowDownload,
        RequestOptions options = null)
    {
        return await guild.GetUserAsync(id: id, mode: mode, options: options)
               ?? throw new NullReferenceException(nameof(id));
    }

    public static async Task<bool> DeleteSafe(this IDeletable deleteable)
    {
        try
        {
            await deleteable.DeleteAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }


    public static void DeleteAfter(this IDeletable deleteable, int delay)
    {
        Task.Factory.StartNew(async () =>
        {
            await Task.Delay(millisecondsDelay: delay);
            await deleteable.DeleteAsync();
        });
    }

    public static bool IsAdmin(this IGuildUser user)
    {
        return user.Guild.Roles.Where(r => user.RoleIds.Contains(value: r.Id)).Any(r => r.Permissions.Administrator);
    }

    public static string GetAvatar(this IUser user, ImageFormat format = ImageFormat.Auto, ushort size = 128)
    {
        return user.GetAvatarUrl(format: format, size: size) ?? user.GetDefaultAvatarUrl();
    }

    public static async Task<IPrivateChannel> GetRequiredPrivateChannelAsync(this IDiscordClient client, ulong id)
    {
        return await client.GetPrivateChannelAsync(id: id)
               ?? throw new NullReferenceException();
    }

    public static async Task<IGuild> GetRequiredGuildAsync(
        this IDiscordClient client,
        ulong id,
        CacheMode mode = CacheMode.AllowDownload,
        RequestOptions options = null)
    {
        return await client.GetGuildAsync(id: id, mode: mode, options: options)
               ?? throw new NullReferenceException();
    }

    public static async Task<IUser> GetRequiredUserAsync(this IDiscordClient client, ulong id,
        CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
    {
        return await client.GetUserAsync(id: id, mode: mode, options: options)
               ?? throw new NullReferenceException();
    }

    public static string GetDisplayFormat(this IUser user)
    {
        return user is null
            ? string.Empty
            : $"{user.Username}#{user.DiscriminatorValue}";
    }
}
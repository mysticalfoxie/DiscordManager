using Discord;

namespace DCM.Core.Extensions;

public static class UserExtensions
{
    public static string GetAvatar(this IUser user, ImageFormat format = ImageFormat.Auto, ushort size = 128)
    {
        return user.GetAvatarUrl(format, size) ?? user.GetDefaultAvatarUrl();
    }
}
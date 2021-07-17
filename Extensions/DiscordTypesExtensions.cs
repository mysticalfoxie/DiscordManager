namespace DCM.Extensions
{
    public static class DiscordTypesExtensions
    {
        public static ulong ToSnowflake(this string str)
            => ulong.Parse(str);
    }
}

using Discord;
using Discord.Rest;
using Discord.WebSocket;

namespace DCM.Core.Extensions;

public static class InteractionExtensions
{
    public static async Task<RestFollowupMessage> FollowupWithEmbed(
        this SocketInteraction interaction,
        Action<EmbedBuilder> configure,
        bool ephemeral = false)
    {
        var embed = GetConfiguredEmbed(configure);
        return await interaction.FollowupAsync(embed: embed, ephemeral: ephemeral);
    }


    public static async Task RespondWithEmbed(
        this SocketInteraction interaction,
        Action<EmbedBuilder> configure,
        bool ephemeral = false)
    {
        var embed = GetConfiguredEmbed(configure);
        await interaction.RespondAsync(embed: embed, ephemeral: ephemeral);
    }

    private static Embed GetConfiguredEmbed(Action<EmbedBuilder> configure)
    {
        var builder = new EmbedBuilder();
        configure(builder);
        var embed = builder.Build();
        return embed;
    }
}
using DCM.Core.Interfaces;
using DCM.Core.Models;
using Discord;
using Microsoft.Extensions.Logging;

namespace DCM.Core.Services;

public class GuildService : IGuildService
{
    private readonly IConfigService _configService;
    private readonly IDiscordService _discordService;
    private readonly ILogger<GuildService> _logger;

    public GuildService(
        IConfigService configService,
        ILogger<GuildService> logger,
        IDiscordService discordService)
    {
        _configService = configService;
        _logger = logger;
        _discordService = discordService;
    }

    public IGuild Guild { get; private set; }
    public IReadOnlyCollection<IGuildUser> Users { get; private set; }
    public IReadOnlyCollection<IRole> Roles { get; private set; }
    public IReadOnlyCollection<ITextChannel> TextChannels { get; private set; }
    public IReadOnlyCollection<IVoiceChannel> VoiceChannels { get; private set; }

    public async Task Load()
    {
        Guild = _discordService.GetRequiredGuild(_configService.GuildConfig.Id);

        var usersTask = Guild.GetUsersAsync();
        var textChannelsTask = Guild.GetTextChannelsAsync();
        var voiceChannelsTask = Guild.GetVoiceChannelsAsync();
        await Task.WhenAll(usersTask, textChannelsTask, voiceChannelsTask);

        Users = usersTask.Result;
        TextChannels = textChannelsTask.Result;
        VoiceChannels = voiceChannelsTask.Result;
        Roles = Guild.Roles;
    }

    public DiscordContainer PropagateContainerFromCache(DiscordContainer container)
    {
        container.Guild = Guild;
        SetDCMGuildConfigValues(
            Users,
            _configService.GuildConfig.GuildUsers,
            (x, y) => x.Id == y.Id,
            container.GuildUsers);

        SetDCMGuildConfigValues(
            Roles,
            _configService.GuildConfig.Roles,
            (x, y) => x.Id == y.Id,
            container.GuildRoles);

        SetDCMGuildConfigValues(
            TextChannels,
            _configService.GuildConfig.TextChannels,
            (x, y) => x.Id == y.Id,
            container.GuildTextChannels);

        SetDCMGuildConfigValues(
            VoiceChannels,
            _configService.GuildConfig.VoiceChannels,
            (x, y) => x.Id == y.Id,
            container.GuildVoiceChannels);

        return container;
    }

    private void SetDCMGuildConfigValues<T>(
        IReadOnlyCollection<T> source,
        IEnumerable<DCMGuildConfigIdMapping> entries,
        Func<DCMGuildConfigIdMapping, T, bool> predicate,
        Dictionary<string, T> dictionary)
    {
        foreach (var entry in entries ?? Array.Empty<DCMGuildConfigIdMapping>())
        {
            var match = source.FirstOrDefault(x => predicate(entry, x));
            if (match is not null)
                dictionary.Add(entry.Name, match);
            else
                _logger.LogError($"An error occured loading config entry '{entry.Name}' with id '{entry.Id}'");
        }
    }
}
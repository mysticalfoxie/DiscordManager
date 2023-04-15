using Discord;

namespace DCM.Core.Interfaces;

public interface IDiscordService
{
    IGuild GetRequiredGuild(ulong id);
}
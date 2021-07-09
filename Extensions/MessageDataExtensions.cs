using DCM.Models;
using DCM.Models.Embed;
using Discord;
using System.Linq;

namespace DCM.Extensions
{
    public static class MessageDataExtensions
    {
        public static Embed Build(this EmbedData data)
            => data is null
            ? null
            : new EmbedBuilder().Build(new EmbedData()
            {

            });
    }
}

using DCM.Models;
using Discord;
using System.Linq;

namespace DCM.Extensions
{
    public static class MessageDataExtensions
    {
        public static Embed GetEmbed(this MessageData data)
        {
            if (data.Embed is null) return null;
            return new EmbedBuilder().Build(new EmbedData()
            {

            })
        }

        public static EmbedData ToEmbedData(this EmbedJsonObject data)
            => new()
            {
                BorderColor = data.Color,
                Fields = data.Fields.Select(x => x.ToFieldData()).ToArray(),
                
            }

        public static FieldData ToFieldData(this Models.EmbedField data)
            => new()
            {
                Heading = data.Name,
                Text = data.Value,
                IsInline = data.Inline
            };
    }
}

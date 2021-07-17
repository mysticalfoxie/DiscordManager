using DCM.Models.Embed;
using DCM.Models.JsonObjects;
using Discord;
using System;
using System.Linq;

namespace DCM.Extensions
{
    public static class JsonObjectExtensions
    {
        public static EmbedData ToEmbedData(this JsonEmbed embed)
            => new()
            {
                BorderColor = embed?.Color.ToColor() ?? Color.DarkGrey,
                Fields = embed?.Fields?.Select(f => f.ToFieldData()).ToArray() ?? Array.Empty<FieldData>(),
                Footer = CreateFooterData(embed?.Footer, embed),
                Text = embed?.Description,
                Header = embed?.Author?.ToHeaderData(),
                ImageUrl = embed?.Image?.Url,
                SubHeading = embed?.Title,
                ThumbnailUrl = embed?.Thumbnail?.Url
            };

        public static FieldData ToFieldData(this JsonField field)
            => new()
            {
                Heading = field.Name,
                IsInline = field.Inline,
                Text = field.Value
            };

        public static FooterData CreateFooterData(JsonFooter footer, JsonEmbed embed)
            => footer == null && embed == null 
            ? null 
            : new()
            {
                IconUrl = footer?.Icon_Url,
                Text = footer?.Text,
                Timestamp = embed?.Timestamp
            };

        public static HeaderData ToHeaderData(this JsonAuthor author)
            => new()
            {
                Heading = author.Name,
                IconUrl = author.Icon_Url,
                Url = author.Url
            };

        private static Color ToColor(this int decColor)
        {
            var hexValue = decColor.ToString("X");
            var hexColor = $"#{hexValue.PadLeft(6, '0')}";
            var color = System.Drawing.ColorTranslator.FromHtml(hexColor);
            return new Color(color.R, color.G, color.B);        
        }
    }
}

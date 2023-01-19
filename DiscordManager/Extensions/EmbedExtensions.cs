using DCM.Exceptions;
using DCM.Models.Embed;
using Discord;
using System;
using System.Collections.Generic;

namespace DCM.Extensions
{
    public static class EmbedExtensions
    {
        public static Embed Build(this EmbedBuilder embedBuilder, EmbedData data)
        {
            if (data.CharactersInSum() > EmbedData.GLOBAL_MAX_CHARACTERS)
                throw new EmbedLimitException("embed", EmbedData.GLOBAL_MAX_CHARACTERS);

            if (!data.Header.IsEmpty())
            {
                var authorBuilder = new EmbedAuthorBuilder();

                if (data.Header.Heading is not null)
                    authorBuilder.WithName(data.Header.Heading);
                if (data.Header.IconUrl is not null)
                    authorBuilder.WithIconUrl(data.Header.IconUrl);
                if (data.Header.Url is not null)
                    authorBuilder.WithUrl(data.Header.Url);

                embedBuilder.WithAuthor(authorBuilder);
            }

            if (!data.Footer.IsEmpty())
            {
                var footerBuilder = new EmbedFooterBuilder();

                if (data.Footer.Text is not null)
                    footerBuilder.WithText(data.Footer.Text);
                if (data.Footer.IconUrl is not null)
                    footerBuilder.WithIconUrl(data.Footer.IconUrl);
                if (data.Footer.Timestamp is not null)
                    embedBuilder.WithTimestamp(new(DateTime.Parse(data.Footer.Timestamp)));

                embedBuilder.WithFooter(footerBuilder);
            }

            if (data.ImageUrl is not null)
                embedBuilder.WithImageUrl(data.ImageUrl);
            if (data.SubHeading is not null)
                embedBuilder.WithTitle(data.SubHeading);
            if (data.Text is not null)
                embedBuilder.WithDescription(data.Text);
            if (data.ThumbnailUrl is not null)
                embedBuilder.WithThumbnailUrl(data.ThumbnailUrl);
            if (data.BorderColor is not null)
                embedBuilder.WithColor((Color)data.BorderColor);

            if ((data.Fields?.Length ?? 0) > 0)
                foreach (var fieldData in data.Fields)
                    embedBuilder
                        .AddField(field => field
                            .WithName(fieldData.Heading)
                            .WithValue(fieldData.Text)
                            .WithIsInline(fieldData.IsInline));

            return embedBuilder.Build();
        }

        public static Embed Build(this EmbedData data)
            => Build(new EmbedBuilder(), data);

        public static EmbedData GetData(this Embed embed)
        {
            var data = new EmbedData()
            {
                SubHeading = embed.Title?.NullWhenEmptyOrWhitespace(),
                Text = embed.Description?.NullWhenEmptyOrWhitespace(),
                ThumbnailUrl = embed.Thumbnail?.Url,
                ImageUrl = embed.Image?.Url
            };

            data.Footer = new FooterData()
            {
                IconUrl = embed.Footer?.IconUrl,
                Text = embed.Footer?.Text,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss.fff") + "Z"
            }.NullWhenEmpty();

            data.Header = new HeaderData()
            {
                Heading = embed.Author?.Name,
                IconUrl = embed.Author?.IconUrl,
                Url = embed.Author?.Url
            }.NullWhenEmpty();

            var fieldDatas = new List<FieldData>();
            if (embed.Fields.Length > 0)
                foreach (var field in embed.Fields)
                    fieldDatas.Add(new()
                    {
                        Heading = field.Name,
                        Text = field.Value,
                        IsInline = field.Inline
                    });

            data.Fields = fieldDatas.ToArray();

            return data;
        }

        internal static string Truncate(this string str, int limit)
            => !string.IsNullOrEmpty(str) && (str?.Length ?? 0) > limit 
                   ? str[..limit] 
                   : str;

        internal static T[] Truncate<T>(this T[] array, int limit)
            => (array?.Length ?? 0) > limit
                   ? array[..limit]
                   : array;

        internal static DateTime? NullWhenDefault(this DateTime dateTime)
            => dateTime == default ? null : dateTime;

        internal static bool IsNullOrEmpty<T>(this T[] array)
            => (array?.Length ?? 0) == 0;

        internal static string NullWhenEmptyOrWhitespace(this string str)
            => string.IsNullOrWhiteSpace(str) ? null : str;

        internal static string ExceptionWhenNull(this string str, string argumentName)
            => str ?? throw new ArgumentNullException(argumentName);

        internal static FooterData NullWhenEmpty(this FooterData footer)
            => footer.IsEmpty() ? null : footer;

        internal static HeaderData NullWhenEmpty(this HeaderData header)
            => header.IsEmpty() ? null : header;

        internal static int CharactersInSum(this EmbedData embed)
        {
            int sum = 0;

            sum += embed.Header?.Heading?.Length ?? 0;
            sum += embed.SubHeading?.Length ?? 0;
            sum += embed.Text?.Length ?? 0;
            sum += embed.Footer?.Text?.Length ?? 0;

            if ((embed.Fields?.Length ?? 0) > 0)
                foreach (var field in embed.Fields)
                    sum += field.Heading.Length + field.Text.Length;

            return sum;
        }

        internal static bool IsEmpty(this HeaderData header)
            => header == null || header?.Heading is null;

        internal static bool IsEmpty(this FooterData footer)
            => footer == null 
            || !(footer?.Text is not null
            || string.IsNullOrWhiteSpace(footer.Timestamp));
    }
}

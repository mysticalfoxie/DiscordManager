using DCM.Models.Embed;
using DCM.Models.JsonObjects;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public static EmbedData ToEmbedData(this JsonEmbed embed, Dictionary<string, object> variableDeclarations)
        {
            if (!string.IsNullOrWhiteSpace(embed.Description))
                embed.Description = InjectVariables(embed.Description, variableDeclarations);

            if (!string.IsNullOrWhiteSpace(embed.Title))
                embed.Title = InjectVariables(embed.Title, variableDeclarations);

            if (!string.IsNullOrWhiteSpace(embed.Url))
                embed.Url = InjectVariables(embed.Url, variableDeclarations);

            if (!string.IsNullOrWhiteSpace(embed.Timestamp))
                embed.Timestamp = InjectVariables(embed.Timestamp, variableDeclarations);

            if (embed.Author is not null)
                embed.Author
                    .GetType()
                    .GetStringProperties()
                    .InjectVariables(embed.Author, variableDeclarations);

            if (embed.Footer is not null)
                embed.Footer
                    .GetType()
                    .GetStringProperties()
                    .InjectVariables(embed.Footer, variableDeclarations);

            if (embed.Image is not null)
                embed.Image
                    .GetType()
                    .GetStringProperties()
                    .InjectVariables(embed.Image, variableDeclarations);

            if (embed.Thumbnail is not null)
                embed.Thumbnail
                    .GetType()
                    .GetStringProperties()
                    .InjectVariables(embed.Thumbnail, variableDeclarations);

            foreach (var field in embed.Fields)
                field
                    .GetType()
                    .GetStringProperties()
                    .InjectVariables(field, variableDeclarations);

            return embed.ToEmbedData();
        }

        public static MessageReference ToMessageReference(this JsonMessageReference reference)
        {
            ulong? messageId = null;
            ulong? channelId = null;
            ulong? guildId = null;

            if (reference.MessageId is not null)
                if (!ulong.TryParse(reference.MessageId, out var parsedMessageId))
                    throw new FormatException($"Cannot assign a ulong from the message id of '{reference.MessageId}'");
                else
                    messageId = parsedMessageId;

            if (reference.ChannelId is not null)
                if (!ulong.TryParse(reference.ChannelId, out var parsedChannelId))
                    throw new FormatException($"Cannot assign a ulong from the channel id of '{reference.ChannelId}'");
                else
                    channelId = parsedChannelId;

            if (reference.GuildId is not null)
                if (!ulong.TryParse(reference.GuildId, out var parsedGuildId))
                    throw new FormatException($"Cannot assign a ulong from the guild id of '{reference.GuildId}'");
                else
                    guildId = parsedGuildId;

            return new(messageId, channelId, guildId);
        }

        public static MessageReference ToMessageReference(this JsonMessageReference reference, Dictionary<string, object> variableDeclarations)
        {
            var message = !string.IsNullOrWhiteSpace(reference.MessageId) ? reference.MessageId : null;
            var channel = !string.IsNullOrWhiteSpace(reference.ChannelId) ? reference.ChannelId : null;
            var guild = !string.IsNullOrWhiteSpace(reference.GuildId) ? reference.GuildId : null;

            return new JsonMessageReference() {
                MessageId = message?.InjectVariables(variableDeclarations),
                GuildId = guild?.InjectVariables(variableDeclarations),
                ChannelId = channel?.InjectVariables(variableDeclarations)
            }.ToMessageReference();
        }

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

        private static IEnumerable<PropertyInfo> GetStringProperties(this Type type)
            => type.GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.CanWrite)
                .Where(x => x.PropertyType == typeof(string));

        private static string InjectVariables(this string str, Dictionary<string, object> declarations)
        {
            var result = str;

            while (result.IndexOf("{{") != -1)
            {
                var span = GetVariableSpan(result); // example: User.CurrentUser.Username                    
                var variableValue = GetVariableValue(span, declarations);
                result = result.Replace($"{{{{{span}}}}}", variableValue);
            }

            return result;
        }

        private static T InjectVariables<T>(this IEnumerable<PropertyInfo> properties, T instance, Dictionary<string, object> declarations)
        {
            foreach (var property in properties)
            {
                var value = (string)property.GetValue(instance);
                if (value is null) continue;

                while (value.IndexOf("{{") != -1)
                {
                    var span = GetVariableSpan(value); // example: User.CurrentUser.Username                    
                    var variableValue = GetVariableValue(span, declarations);
                    value = value.Replace($"{{{{{span}}}}}", variableValue);
                }

                if (value != (string)property.GetValue(instance)) // Something changed
                    property.SetValue(instance, value);
            }

            return instance;
        }

        private static string GetVariableValue(string span, Dictionary<string, object> declarations)
        {
            if (span.Contains('.'))
            {
                var declaration = declarations.First(x => x.Key.ToLower() == span.Split('.')[0].ToLower()).Value;
                Type currentType = declaration.GetType();
                object currentInstance = declaration;

                string[] part = span.Split('.');
                for (int i = 1; i < part.Length; i++)
                {
                    var childProperty = GetPropertyCaseInvarient(currentType, part[i]);
                    currentType = childProperty.PropertyType;
                    currentInstance = childProperty.GetValue(currentInstance);
                }

                return currentInstance.ToString();
            }
            else
            {
                var declaration = declarations.First(x => x.Key.ToLower() == span.ToLower()).Value;
                Type type = declaration.GetType();
                object instance = declaration;

                return instance.ToString();
            }
        }

        private static string GetVariableSpan(string str)
        {
            var startIndex = str.IndexOf("{{");
            var endIndex = str.IndexOf("}}");

            if (endIndex == -1) throw new FormatException("Missing the end of the variable declaration. Please add '}}' to the end of the variable.");
            if (endIndex < startIndex) throw new FormatException("Missing the end of the variable declaration. Please add '}}' to the end of the variable.");

            var span = str[(startIndex + "{{".Length)..endIndex];

            if (string.IsNullOrWhiteSpace(span)) throw new FormatException("The variable cannot be empty or whitespace!");

            return span.Trim();
        }

        private static PropertyInfo GetPropertyCaseInvarient(Type type, string propertyName)
        {
            var exactMatch = type
                .GetProperties()
                .FirstOrDefault(x => x.Name == propertyName);

            if (exactMatch is not null) // Exact Match!!
                return exactMatch;
            else
            {
                var propertyNames = type.GetProperties().Select(x => x.Name);
                var matches = propertyNames.Where(x => x.ToLower() == propertyName).ToArray();
                if (matches.Length > 1) throw new AmbiguousMatchException("Cannot find a unique property because there are multiple properties with the same name. (case-invarient)");
                if (matches.Length == 0) throw new NullReferenceException($"Could not find property that matches the name '{propertyName}' on the type '{type.FullName}'.");

                var ciPropertyName = matches[0]; // Case-Invarient Property Name
                return type.GetProperty(ciPropertyName);
            }
        }
    }
}

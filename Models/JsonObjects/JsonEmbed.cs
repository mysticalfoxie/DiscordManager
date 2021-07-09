using Newtonsoft.Json;
using System;

namespace DCM.Models.JsonObjects
{
    public class JsonEmbed
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("color")]
        public int Color { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("footer")]
        public JsonFooter Footer { get; set; }

        [JsonProperty("thumbnail")]
        public JsonThumbnail Thumbnail { get; set; }

        [JsonProperty("image")]
        public JsonImage Image { get; set; }

        [JsonProperty("author")]
        public JsonAuthor Author { get; set; }

        [JsonProperty("fields")]
        public JsonField[] Fields { get; set; }

        public static JsonEmbed GetExample()
            => new()
            {
                Author = new()
                {
                    IconUrl = "https://cdn.discordapp.com/embed/avatars/0.png",
                    Name = "Author Name",
                    Url = "https://discord.com/"
                },
                Color = 14359500,
                Timestamp = DateTime.Now,
                Description = "this supports [named links](https://discordapp.com) on top of the previously shown subset of markdown. ```\nyes, even code blocks```",
                Fields = new JsonField[]
                {
                    new()
                    {
                        Name = "🤔",
                        Value = "Some of these properties have certain limits..."
                    },
                    new()
                    {
                        Name = "😱",
                        Value = "Try Esceeding some of them!"
                    },
                    new()
                    {
                        Name = "🙄",
                        Value = "An informative error should show up, and this view will remain as-is until all issues are fixed"
                    },
                    new()
                    {
                        Name = "😍",
                        Value = "These last two",
                        Inline = true
                    },
                    new()
                    {
                        Name = "😍",
                        Value = "are inline fields!",
                        Inline = true
                    }
                },
                Footer = new()
                {
                    IconUrl = "https://cdn.discordapp.com/embed/avatars/0.png",
                    Text = "Footer text!"
                },
                Image = new()
                {
                    Url = "https://cdn.discordapp.com/embed/avatars/0.png"
                },
                Thumbnail = new()
                {
                    Url = "https://cdn.discordapp.com/embed/avatars/0.png"
                },
                Title = "Title ~~(did you know you can have markdown here too?)~~",
                Url = "https://discordapp.com"
            };
    }
}

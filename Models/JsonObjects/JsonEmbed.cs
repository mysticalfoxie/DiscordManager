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
    }
}

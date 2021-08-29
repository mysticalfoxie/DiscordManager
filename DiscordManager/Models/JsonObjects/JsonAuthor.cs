using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace DCM.Models.JsonObjects
{
    public class JsonAuthor : ICloneable
    {
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonProperty("icon_url")]
        [JsonPropertyName("icon_url")]
        public string Icon_Url { get; set; }

        public object Clone()
            => new JsonAuthor()
            {
                Name = Name,
                Url = Url,
                Icon_Url = Icon_Url
            };
    }
}

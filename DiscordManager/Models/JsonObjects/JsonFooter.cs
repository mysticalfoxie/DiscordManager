using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace DCM.Models.JsonObjects
{
    public class JsonFooter : ICloneable
    {
        [JsonProperty("icon_url")]
        [JsonPropertyName("icon_url")]
        public string Icon_Url { get; set; }

        [JsonProperty("text")]
        [JsonPropertyName("text")]
        public string Text { get; set; }

        public object Clone()
            => new JsonFooter()
            {
                Icon_Url = Icon_Url,
                Text = Text
            };
    }
}

using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace DCM.Models.JsonObjects
{
    public class JsonField : ICloneable
    {
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonProperty("inline")]
        [JsonPropertyName("inline")]
        public bool Inline { get; set; }

        public object Clone()
            => new JsonField()
            {
                Inline = Inline,
                Name = Name,
                Value = Value
            };
    }
}

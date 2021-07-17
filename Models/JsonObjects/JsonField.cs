using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DCM.Models.JsonObjects
{
    public class JsonField
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
    }
}

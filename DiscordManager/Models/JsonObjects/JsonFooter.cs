using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DCM.Models.JsonObjects
{
    public class JsonFooter
    {
        [JsonProperty("icon_url")]
        [JsonPropertyName("icon_url")]
        public string Icon_Url { get; set; }

        [JsonProperty("text")]
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}

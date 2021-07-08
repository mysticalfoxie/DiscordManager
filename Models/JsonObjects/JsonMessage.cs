using Newtonsoft.Json;

namespace DCM.Models.JsonObjects
{
    public class JsonMessage
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("embed")]
        public JsonEmbed Embed { get; set; }
    }
}

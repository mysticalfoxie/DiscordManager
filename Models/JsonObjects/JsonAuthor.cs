using Newtonsoft.Json;

namespace DCM.Models.JsonObjects
{
    public class JsonAuthor
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }
    }
}

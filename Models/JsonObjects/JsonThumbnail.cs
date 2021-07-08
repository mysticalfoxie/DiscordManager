using Newtonsoft.Json;

namespace DCM.Models.JsonObjects
{
    public class JsonThumbnail
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}

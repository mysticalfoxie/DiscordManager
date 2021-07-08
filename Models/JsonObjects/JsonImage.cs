using Newtonsoft.Json;

namespace DCM.Models.JsonObjects
{
    public class JsonImage
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}

using Newtonsoft.Json;

namespace DCM.Models.JsonObjects
{
    public class JsonFooter
    {
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}

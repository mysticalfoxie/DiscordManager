using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace DCM.Models.JsonObjects
{
    public class JsonImage : ICloneable
    {
        [JsonProperty("url")]
        [JsonPropertyName("url")]
        public string Url { get; set; }

        public object Clone()
            => new JsonImage()
            {
                Url = Url
            };
    }
}

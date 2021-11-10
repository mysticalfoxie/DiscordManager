using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace DCM.Models.JsonObjects
{
    public class JsonMessage : ICloneable
    {
        [JsonProperty("content")]
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonProperty("embed")]
        [JsonPropertyName("embed")]
        public JsonEmbed Embed { get; set; }

        [JsonProperty("tts")]
        [JsonPropertyName("tts")]
        public bool TTS { get; set; }

        [JsonProperty("message_reference")]
        [JsonPropertyName("message_reference")]
        public JsonMessageReference MessageReference { get; set; }

        public static JsonMessage GetExample()
            => new()
            {
                Content = "This `supports` __a__ **subset** *of* ~~markdown~~ 😃 ```cs\npublic class Program {\n    public static void Main(string[] args) {\n        // Coding is fun! :D\n    }\n}```",
                Embed = JsonEmbed.GetExample()
            };

        public object Clone()
            => new JsonMessage()
            {
                Content = (string)Content?.Clone(),
                Embed = (JsonEmbed)Embed?.Clone()
            };
    }
}

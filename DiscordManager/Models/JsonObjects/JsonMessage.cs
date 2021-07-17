using Newtonsoft.Json;

namespace DCM.Models.JsonObjects
{
    public class JsonMessage
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("embed")]
        public JsonEmbed Embed { get; set; }

        public static JsonMessage GetExample()
            => new()
            {
                Content = "This `supports` __a__ **subset** *of* ~~markdown~~ 😃 ```cs\npublic class Program {\n    public static void Main(string[] args) {\n        // Coding is fun! :D\n    }\n}```",
                Embed = JsonEmbed.GetExample()
            };
    }
}

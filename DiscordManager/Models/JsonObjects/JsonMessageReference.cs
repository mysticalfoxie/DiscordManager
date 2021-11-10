using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace DCM.Models.JsonObjects
{
    public class JsonMessageReference : ICloneable
    {
        [JsonProperty("message_id")]
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("channel_id")]
        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }

        [JsonProperty("guild_id")]
        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; }

        public object Clone()
            => new JsonMessageReference()
            {
                MessageId = MessageId,
                ChannelId = ChannelId,
                GuildId = GuildId
            };
    }
}

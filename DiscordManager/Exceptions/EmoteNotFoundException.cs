using System;

namespace DCM.Exceptions
{
    [Serializable]
    public class EmoteNotFoundException : Exception
    {
        public EmoteNotFoundException() : this(default) { }
        public EmoteNotFoundException(ulong? id) : this(id, null) { }
        public EmoteNotFoundException(ulong? id, string emoteAlias) : this(id, emoteAlias, string.Format("The emote could not be found.{0}", id != default && !string.IsNullOrWhiteSpace(emoteAlias) ? $" (ID: {id}, Alias: '{emoteAlias}')" : id == default && !string.IsNullOrWhiteSpace(emoteAlias) ? $" (Alias: '{emoteAlias}')" : id != default && string.IsNullOrWhiteSpace(emoteAlias) ? $" (ID: {id})" : "")) { }
        public EmoteNotFoundException(ulong? id, string emoteAlias, string message) : this(id, emoteAlias, message, null) { }
        public EmoteNotFoundException(ulong? id, string emoteAlias, string message, Exception inner) : base(message, inner)
        {
            ChannelId = id;
            EmoteAlias = emoteAlias;
        }

        protected EmoteNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public ulong? ChannelId { get; }
        public string EmoteAlias { get; }
    }
}

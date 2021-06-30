using System;

namespace DCM.Exceptions
{
    [Serializable]
    public class ChannelNotFoundException : Exception
    {
        public ChannelNotFoundException() : this(default) { }
        public ChannelNotFoundException(ulong? id) : this(id, null) { }
        public ChannelNotFoundException(ulong? id, string channelAlias) : this(id, channelAlias, string.Format("The channel could not be found.{0}", id != default && !string.IsNullOrWhiteSpace(channelAlias) ? $" (ID: {id}, Alias: '{channelAlias}')" : id == default && !string.IsNullOrWhiteSpace(channelAlias) ? $" (Alias: '{channelAlias}')" : id != default && string.IsNullOrWhiteSpace(channelAlias) ? $" (ID: {id})" : "")) { }
        public ChannelNotFoundException(ulong? id, string channelAlias, string message) : this(id, channelAlias, message, null) { }
        public ChannelNotFoundException(ulong? id, string channelAlias, string message, Exception inner) : base(message, inner)
        {
            ChannelId = id;
            ChannelAlias = channelAlias;
        }
        protected ChannelNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public ulong? ChannelId { get; }
        public string ChannelAlias { get; }
    }
}

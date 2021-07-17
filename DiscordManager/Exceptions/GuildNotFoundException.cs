using System;

namespace DCM.Exceptions
{
    [Serializable]
    public class GuildNotFoundException : Exception
    {
        public GuildNotFoundException() : this(default) { }
        public GuildNotFoundException(ulong? id) : this(id, null) { }
        public GuildNotFoundException(ulong? id, string guildAlias) : this(id, guildAlias, string.Format("The guild could not be found.{0}", id != default && !string.IsNullOrWhiteSpace(guildAlias) ? $" (ID: {id}, Alias: '{guildAlias}')" : id == default && !string.IsNullOrWhiteSpace(guildAlias) ? $" (Alias: '{guildAlias}')" : id != default && string.IsNullOrWhiteSpace(guildAlias) ? $" (ID: {id})" : "")) { }
        public GuildNotFoundException(ulong? id, string guildAlias, string message) : this(id, guildAlias, message, null) { }
        public GuildNotFoundException(ulong? id, string guildAlias, string message, Exception inner) : base(message, inner)
        {
            GuildId = id;
            GuildAlias = guildAlias;
        }
        protected GuildNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public ulong? GuildId { get; }
        public string GuildAlias { get; }
    }
}

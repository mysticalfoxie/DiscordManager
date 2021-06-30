using System;

namespace DCM.Exceptions
{
    [Serializable]
    public class RoleNotFoundException : Exception
    {
        public RoleNotFoundException() : this(default) { }
        public RoleNotFoundException(ulong? id) : this(id, null) { }
        public RoleNotFoundException(ulong? id, string roleAlias) : this(id, roleAlias, string.Format("The role could not be found.{0}", id != default && !string.IsNullOrWhiteSpace(roleAlias) ? $" (ID: {id}, Alias: '{roleAlias}')" : id == default && !string.IsNullOrWhiteSpace(roleAlias) ? $" (Alias: '{roleAlias}')" : id != default && string.IsNullOrWhiteSpace(roleAlias) ? $" (ID: {id})" : "")) { }
        public RoleNotFoundException(ulong? id, string roleAlias, string message) : this(id, roleAlias, message, null) { }
        public RoleNotFoundException(ulong? id, string roleAlias, string message, Exception inner) : base(message, inner)
        {
            RoleId = id;
            RoleAlias = roleAlias;
        }
        protected RoleNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public ulong? RoleId { get; }
        public string RoleAlias { get; }
    }
}

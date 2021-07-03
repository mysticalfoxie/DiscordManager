using System;

namespace DCM.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : this(default) { }
        public UserNotFoundException(ulong? id) : this(id, null) { }
        public UserNotFoundException(ulong? id, string username) : this(id, username, string.Format("The channel could not be found.{0}", id != default && !string.IsNullOrWhiteSpace(username) ? $" (ID: {id}, Alias: '{username}')" : id == default && !string.IsNullOrWhiteSpace(username) ? $" (Alias: '{username}')" : id != default && string.IsNullOrWhiteSpace(username) ? $" (ID: {id})" : "")) { }
        public UserNotFoundException(ulong? id, string username, string message) : this(id, username, message, null) { }
        public UserNotFoundException(ulong? id, string username, string message, Exception inner) : base(message, inner)
        {
            UserId = id;
            Username = username;
        }
        protected UserNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public ulong? UserId { get; }
        public string Username { get; }
    }
}

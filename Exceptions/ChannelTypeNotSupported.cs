using System;

namespace DCM.Exceptions
{
    [Serializable]
    public class ChannelTypeNotSupported : Exception
    {
        public ChannelTypeNotSupported() : this(null) { }
        public ChannelTypeNotSupported(Type channelType) : this(channelType, null) { }
        public ChannelTypeNotSupported(Type channelType, string message) : this(channelType, message, null) { }
        public ChannelTypeNotSupported(Type channelType, string message, Exception inner) : base(message, inner)
        {
            ChannelType = channelType;
        }

        protected ChannelTypeNotSupported(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public Type ChannelType { get; }
    }
}

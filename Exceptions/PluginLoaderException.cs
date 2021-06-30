using System;
using System.Runtime.Serialization;

namespace DCM.Exceptions
{

    [Serializable]
    public class PluginLoaderException : PluginException
    {
        public PluginLoaderException() { }
        public PluginLoaderException(string message) : base(message) { }
        public PluginLoaderException(string message, Exception inner) : base(message, inner) { }
        protected PluginLoaderException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

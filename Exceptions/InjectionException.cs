using System;

namespace DCM.Exceptions
{

    [Serializable]
    public class InjectionException : Exception
    {
        public InjectionException() { }
        public InjectionException(string message) : base(message) { }
        public InjectionException(string message, Exception inner) : base(message, inner) { }
        protected InjectionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

using System;

namespace DCM.Exceptions
{
    [Serializable]
    public class EmbedLimitException: Exception
    {
        public EmbedLimitException(string fieldName, int limit) : base(string.Format("The {0} cannot have more than {1} characters.", fieldName, limit)) 
        {
            FieldName = fieldName;
            Limit = limit;
        }

        protected EmbedLimitException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public string FieldName { get; }
        public int Limit { get; }
    }
}

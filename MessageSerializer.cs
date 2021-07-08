using DCM.Models;
using DCM.Models.JsonObjects;
using Newtonsoft.Json;

namespace DCM
{
    public static class MessageSerializer
    {
        public static string Serialize(this JsonMessage data)
            => JsonConvert.SerializeObject(data);

        public static MessageData Deserialize(this string data)
            => JsonConvert.DeserializeObject<MessageData>(data);
    }
}

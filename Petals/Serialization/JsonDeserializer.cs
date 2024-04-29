using Newtonsoft.Json;
using System.IO;

namespace Petals.Serialization
{
    public class JsonDeserializer
    {
        public static T DeserializeConfig<T>(string configPath)
        {
            string json = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}

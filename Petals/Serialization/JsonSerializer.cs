using Newtonsoft.Json;
using System.IO;

namespace Petals.Serialization
{
    public class JsonSerializer
    {
        public void SerializeConfig<T>(T obj, string configPath)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(configPath, json);
        }
    }
}
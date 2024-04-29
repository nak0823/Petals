using Petals.Models;
using Petals.Serialization;
using System;
using System.IO;

namespace Petals.Utilities
{
    public class ConfigUtils
    {
        private static readonly string Path = "Settings.json";
        public static bool ConfigExists() => File.Exists("Settings.json");

        public static bool CreateConfig()
        {
            

            Settings settings = new Settings()
            {
                Renaming = new Settings.RenamingSettings()
                {
                    Enabled = true,
                    Types = true,
                    Properties = true,
                    Methods = true,
                    Events = true,
                    Fields = true,
                    Parameters = true,
                },

                LocalToField = new Settings.L2FSettings()
                {
                    Enabled = true,
                }
            };

            try
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.SerializeConfig(settings, Path);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Settings ObtainSettings()
        {
            JsonDeserializer jsonDeserializer = new JsonDeserializer();
            return JsonDeserializer.DeserializeConfig<Settings>(Path);
        }
    }
}
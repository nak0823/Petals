using System;
using System.Reflection;
using Petals.CLI.Models;
using Petals.Models;
using Petals.Utilities;

namespace Petals.CLI.Utilities.Menus
{
    public class SettingsMenu : Menu
    {
        public override string Label => "Settings";

        public override void OnSelect()
        {
            Interface.PrintLogo(Interface.AsciiLogo, Interface.Credits);
            Interface.SetTitle(Label);

            Logger logger = new Logger();
            ProcessConfig(logger);

            var settings = ConfigUtils.ObtainSettings();
            logger.ShowSuccess("Successfully processed config.", false);

            Interface.PrintLogo(Interface.AsciiLogo, Interface.Credits);
            PrintAllSettings(settings);

            Console.ReadLine();
        }

        public static void PrintAllSettings(Settings settings)
        {
            foreach (PropertyInfo prop in typeof(Settings).GetProperties())
            {
                if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
                {
                    Interface.PrintLogo(Interface.SettingsLogos[prop.Name], Interface.Credits);

                    foreach (PropertyInfo nestedProp in prop.PropertyType.GetProperties())
                    {
                        object value = nestedProp.GetValue(prop.GetValue(settings));
                        Interface.PrintSetting(nestedProp.Name, value.ToString());
                    }
                }
                else
                {
                    object value = prop.GetValue(settings);
                    Interface.PrintSetting(prop.Name, value.ToString());
                }

                Console.ReadKey();
            }
        }

        private static void ProcessConfig(Logger logger)
        {
            if (!ConfigUtils.ConfigExists())
            {
                logger.ShowWarning("Config file does not exist. Creating one now.", false);

                if (ConfigUtils.CreateConfig())
                {
                    logger.ShowSuccess("Config file created successfully.", false);
                }
                else
                {
                    logger.ShowError("Failed to create config file.", false);
                }
            }
        }
    }
}

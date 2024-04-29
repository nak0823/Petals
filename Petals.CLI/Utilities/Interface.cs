using System;
using System.Collections.Generic;
using System.Drawing;
using Console = Colorful.Console;

namespace Petals.CLI.Utilities
{
    public class Interface
    {
        public static readonly string[] AsciiLogo =
        {
            "██████╗ ███████╗████████╗ █████╗ ██╗     ███████╗",
            "██╔══██╗██╔════╝╚══██╔══╝██╔══██╗██║     ██╔════╝",
            "██████╔╝█████╗     ██║   ███████║██║     ███████╗",
            "██╔═══╝ ██╔══╝     ██║   ██╔══██║██║     ╚════██║",
            "██║     ███████╗   ██║   ██║  ██║███████╗███████║",
            "╚═╝     ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚══════╝╚══════╝"
        };

        public static readonly string Credits = "Intelligent .NET code obfuscator by Nak0823";

        private static readonly Color[] Gradient = new Color[]
        {
            ColorTranslator.FromHtml("#FFFFFF"),
            ColorTranslator.FromHtml("#F8F8F8"),
            ColorTranslator.FromHtml("#F2F2F2"),
            ColorTranslator.FromHtml("#EBEBEB"),
            ColorTranslator.FromHtml("#E5E5E5"),
            ColorTranslator.FromHtml("#DEDEDE")
        };

        public static readonly Color PrimaryColor = ColorTranslator.FromHtml("#DA411E");

        #region SettingsLogos
        private static readonly string[] RenamingLogo =
        {
            "██████╗ ███████╗███╗   ██╗ █████╗ ███╗   ███╗██╗███╗   ██╗ ██████╗ ",
            "██╔══██╗██╔════╝████╗  ██║██╔══██╗████╗ ████║██║████╗  ██║██╔════╝ ",
            "██████╔╝█████╗  ██╔██╗ ██║███████║██╔████╔██║██║██╔██╗ ██║██║  ███╗",
            "██╔══██╗██╔══╝  ██║╚██╗██║██╔══██║██║╚██╔╝██║██║██║╚██╗██║██║   ██║",
            "██║  ██║███████╗██║ ╚████║██║  ██║██║ ╚═╝ ██║██║██║ ╚████║╚██████╔╝",
            "╚═╝  ╚═╝╚══════╝╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝ ╚═════╝ "
        };

        private static readonly string[] L2FLogo =
        {
            "██╗     ██████╗ ███████╗",
            "██║     ╚════██╗██╔════╝",
            "██║      █████╔╝█████╗  ",
            "██║     ██╔═══╝ ██╔══╝  ",
            "███████╗███████╗██║     ",
            "╚══════╝╚══════╝╚═╝     "
        };
        #endregion

        public static Dictionary<string, string[]> SettingsLogos = new Dictionary<string, string[]>
        {
            { "Renaming", RenamingLogo },
            { "LocalToField", L2FLogo },
        };


        public static void PrintLogo(string[] logo, string credits)
        {
            Console.Clear();
            Console.CursorVisible = false;

            for (int i = 0; i < logo.Length; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - logo[i].Length) / 2, Console.CursorTop + 1);
                Console.Write(logo[i], Gradient[i]);
            }

            Console.SetCursorPosition((Console.WindowWidth - credits.Length) / 2, Console.CursorTop + 1);
            Console.WriteLine(credits + "\n\n", PrimaryColor);
        }

        public static void PrintInput()
        {
            Console.Write("[", PrimaryColor);
            Console.Write(">", Color.White);
            Console.Write("] ", PrimaryColor);
        }

        public static void PrintLabel(int number, string label)
        {
            Console.Write("[", PrimaryColor);
            Console.Write(number, Color.White);
            Console.Write("] ", PrimaryColor);
            Console.Write(label + "\n", Color.White);
        }

        public static void PrintSetting(string setting, string value)
        {
            Console.Write("[", PrimaryColor);
            Console.Write(setting, Color.White);
            Console.Write("] ", PrimaryColor);
            Console.Write(value + "\n", Color.White);
        }

        public static void SetTitle(string message)
        {
            Console.Title = string.Format("Petals - {0}", message);
        }
    }
}
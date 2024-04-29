using Petals.CLI.Models;
using Petals.CLI.Utilities;
using Petals.CLI.Utilities.Menus;
using System;
using System.Collections.Generic;

namespace Petals.CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var menus = CreateMenus();
            HandleMenus(menus);
        }

        private static void HandleMenus(List<Menu> menus)
        {
            while (!false)
            {
                Interface.PrintLogo(Interface.AsciiLogo, Interface.Credits);
                Interface.SetTitle("Main Menu");

                for (int i = 0; i < menus.Count; i++)
                    Interface.PrintLabel(i + 1, menus[i].Label);

                switch (GetMenuInput())
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        menus[0].OnSelect();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        menus[1].OnSelect();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        menus[2].OnSelect();
                        break;

                    default:
                        HandleMenus(menus);
                        break;
                }
            }
        }

        private static ConsoleKey GetMenuInput()
        {
            return Console.ReadKey(true).Key;
        }

        private static List<Menu> CreateMenus()
        {
            var menus = new List<Menu>
            {
                new ObfuscateMenu(),
                new SettingsMenu(),
                new CreditsMenu()
            };

            return menus;
        }
    }
}
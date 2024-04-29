using Petals.CLI.IO;
using Petals.CLI.Models;
using System;

namespace Petals.CLI.Utilities.Menus
{
    internal class ObfuscateMenu : Menu
    {
        public override string Label => "Obfuscate";

        public override void OnSelect()
        {
            Interface.SetTitle(Label);

            AssemblyReader assemblyReader = new AssemblyReader();
            var assemblyPath = assemblyReader.SelectAssembly();
            

            // From here parse assemblypath to assembly... catch errors if present.

            Console.ReadKey();
        }
    }
}
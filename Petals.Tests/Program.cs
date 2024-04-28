using System;
using Petals.Models;
using Petals.Protections.Renaming;

namespace Petals.Tests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var testPath = Console.ReadLine();
            var assembly = new Assembly(testPath);
            var renamer = new Renamer(true, true, true, true, true, true);
            renamer.Rename(assembly);
            assembly.SaveModule();
           
            Console.ReadKey();
        }
    }
}

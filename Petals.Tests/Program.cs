﻿using Petals.Models;
using Petals.Protections.Renaming;
using System;

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
            L2F.Protect(assembly);
          
            assembly.SaveModule();
            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
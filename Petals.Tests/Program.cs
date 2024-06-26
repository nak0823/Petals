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
            renamer.Protect(assembly);
          
            assembly.SaveModule();
            Console.ReadKey();
        }
    }
}
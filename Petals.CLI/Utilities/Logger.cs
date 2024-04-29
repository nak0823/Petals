using Petals.CLI.Models;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace Petals.CLI.Utilities
{
    public class Logger : ILogger
    {
        public void ShowError(string message, bool sleep)
        {
            Console.Write("[", Interface.PrimaryColor);
            Console.Write("Error", Color.White);
            Console.Write("] ", Interface.PrimaryColor);
            Console.WriteLine(message, Color.White);

            if (sleep)
                System.Threading.Thread.Sleep(1000);
        }

        public void ShowException(Exception exception, bool sleep)
        {
            Console.Write("[", Interface.PrimaryColor);
            Console.Write("Exception", Color.White);
            Console.Write("] ", Interface.PrimaryColor);
            Console.WriteLine(exception.Message, Color.White);

            if (sleep)
                System.Threading.Thread.Sleep(1000);
        }

        public void ShowInfo(string message, bool sleep)
        {
            Console.Write("[", Interface.PrimaryColor);
            Console.Write("Info", Color.White);
            Console.Write("] ", Interface.PrimaryColor);
            Console.WriteLine(message, Color.White);

            if (sleep)
                System.Threading.Thread.Sleep(1000);
        }

        public void ShowSuccess(string message, bool sleep)
        {
            Console.Write("[", Interface.PrimaryColor);
            Console.Write("Success", Color.White);
            Console.Write("] ", Interface.PrimaryColor);
            Console.WriteLine(message, Color.White);

            if (sleep)
                System.Threading.Thread.Sleep(1000);
        }

        public void ShowWarning(string message, bool sleep)
        {
            Console.Write("[", Interface.PrimaryColor);
            Console.Write("Warning", Color.White);
            Console.Write("] ", Interface.PrimaryColor);
            Console.WriteLine(message, Color.White);

            if (sleep)
                System.Threading.Thread.Sleep(1000);

        }
    }
}
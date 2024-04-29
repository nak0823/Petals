using Petals.CLI.Utilities;
using System;
using System.IO;

namespace Petals.CLI.IO
{
    public class AssemblyReader
    {
        public string SelectAssembly()
        {
            bool hasSelected = false;
            string path = string.Empty;

            Logger logger = new Logger();

            while (!hasSelected)
            {
                Console.Clear();
                Interface.PrintLogo(Interface.AsciiLogo, Interface.Credits);

                logger.ShowInfo("Enter the path to the assembly you want to protect.", false);
                Interface.PrintInput();

                path = Console.ReadLine();

                if (path == null)
                {
                    logger.ShowError("The path cannot be null.", true);
                    continue;
                };

                if (!File.Exists(path))
                {
                    logger.ShowError("The file does not exist.", true);
                    continue;
                }

                if (Path.GetExtension(path) != ".dll" && Path.GetExtension(path) != ".exe")
                {
                    logger.ShowError("The file is not a valid assembly file.", true);
                    continue;
                }

                if (IsFileInUse(path))
                {
                    logger.ShowError("The file is in use by another process.", true);
                    continue;
                }

                hasSelected = true;
            }

            logger.ShowSuccess("The assembly has been selected successfully.", true);
            return path;
        }

        private bool IsFileInUse(string path)
        {
            FileStream fs = null;

            try
            {
                fs = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                return false;
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
    }
}
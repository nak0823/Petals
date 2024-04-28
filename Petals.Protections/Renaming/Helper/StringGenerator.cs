using System;

namespace Petals.Protections.Renaming.Helper
{
    public class StringGenerator
    {
        /// <summary>
        /// Random object for generating random strings.
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Function to generate a random string of a given size.
        /// </summary>
        /// <param name="length">The length of the string</param>
        /// <returns>A random string of given size.</returns>
        public static string Generate(int length)
        {
            const string chars = "01";
            string hexString = "0b";

            for (int i = 0; i < length; i++)
            {
                hexString += chars[random.Next(chars.Length)];
            }

            return hexString;
        }
    }
}
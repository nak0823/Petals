using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petals.Protections.Renaming.Helper
{
    public class StringGenerator
    {
        public static string Generate(int size)
        {
            byte[] data = new byte[size];

            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(data);
            }

            return Convert.ToBase64String(data).Substring(0, size);
        }
    }
}

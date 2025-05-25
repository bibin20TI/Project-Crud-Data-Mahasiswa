using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Crud
{
    class HashHelper
    {//  
        public static string hashPassword(string password)
        {   // ini merupakan method static yang akan 
            using (SHA256 sha256 = SHA256.Create())
            {
                Byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                return BitConverter.ToString(bytes).Replace("-", "").ToLower();

            }
        }

    }
}

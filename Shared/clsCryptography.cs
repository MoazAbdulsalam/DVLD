using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class clsCryptography
    {
        public static string ComputeHash(string input)
        {
            //توقيع يونيك لكل داتا
            //الاماكن اللي بدك ترجع الاوريجنال داتا مابتستخدم فيها الهاشنج
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashbytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(hashbytes).Replace("-", "").ToLower();
            }
        }
    }
}

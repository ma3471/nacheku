using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.Nacheku.Logic.SecurityHelper
{
    public class ConstantLengthIntegerId
    {
        public static int CreateRandomId()
        {
            //MD5 cryptography = new MD5CryptoServiceProvider();
            //var code = cryptography.ComputeHash(Encoding.Default.GetBytes(toHach));
            var csprng = new RNGCryptoServiceProvider();
            var code = new byte[24];
            csprng.GetBytes(code);
            //int i = 0, sum = 0;
            //unchecked
            //{
            //    sum += code.Sum(item => (int) (item*Math.Pow(256, i++%4)));
            //}
            
            //return sum;
            
            return Math.Abs(Encoding.UTF8.GetString(code).GetHashCode());
        }
    }
}

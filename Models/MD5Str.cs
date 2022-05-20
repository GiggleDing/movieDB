using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
 
namespace MvcMovie.Models
{
    public class Encrypt
    {
        public static string ByMd5(string value)
        {
            var bytesMd5 = GetBytesMd5(value);
            var md5Value = BitConverter.ToString(bytesMd5);
            return md5Value;
        }
 
        private static byte[] GetBytesMd5(string value)
        {
            var bytes = Encoding.Default.GetBytes(value);
            byte[] bytesMd5;
            using (var md5 = MD5.Create())
            {
                bytesMd5 = md5.ComputeHash(bytes);
            }
            return bytesMd5;
        }
 
        public static string ByMd5_1(string value)
        {
            var md5Value = ByMd5(value);
            md5Value = md5Value.Replace("-", "");
            return md5Value;
        }
    }
}
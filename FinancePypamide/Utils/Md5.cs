using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FinancePypamide.Utils
{
    public static class Md5
    {
        public static string Encrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            MD5 md5 = new MD5CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(str);
            var encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }
    }
}
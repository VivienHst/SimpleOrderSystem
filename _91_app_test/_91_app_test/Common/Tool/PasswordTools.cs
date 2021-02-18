using System;
using System.Security.Cryptography;
using System.Text;

namespace _91_app_test.Common.Tool
{
    public class PasswordTools
    {
        public PasswordTools()
        {
        }

        /// <summary>
        /// 取得隨機字串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomString(int length)
        {
            if (length <= 0) return string.Empty;
            var str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" +
                "!@#$%^&*()_+=-.><";
            var next = new Random();
            var builder = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                builder.Append(str[next.Next(0, str.Length)]);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 取得SHA256加密字串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetSHA256Encrypt(string input)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();//建立一個SHA256
            byte[] source = Encoding.Default.GetBytes(input);//將字串轉為Byte[]
            byte[] crypto = sha256.ComputeHash(source);//進行SHA256加密
            string result = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
            return result;
        }
    }
}

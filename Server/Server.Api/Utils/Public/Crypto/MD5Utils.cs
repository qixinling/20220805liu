using System.Security.Cryptography;
using System.Text;

namespace Server.Utils.Crypto_Utils
{
    public static class MD5Utils
    {
        public static string MD5Encrypt(string Mcontent, int Bit)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] HashedDataBytes;
            HashedDataBytes = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(Mcontent));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in HashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            if (Bit == 16)
                return tmp.ToString().Substring(8, 16);
            else
                if (Bit == 32) return tmp.ToString();//默认情况
            else return string.Empty;
        }
    }
}
using System;
using System.Security.Cryptography;
using System.Text;

namespace Server.Utils.Crypto_Utils
{
    /// <summary>
    /// 前台加密解密
    /// </summary>
    public static class AESUtils
    {
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="Str">要加密字符串</param>
        /// <returns>返回加密后字符串</returns>
        public static string AES_Encrypt(String Str, String strAesKey = "123456789.012345678901234.567123")
        {
            Byte[] keyArray = Encoding.UTF8.GetBytes(strAesKey);
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(Str);

            using RijndaelManaged rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="Str">要解密字符串</param>
        /// <returns>返回解密后字符串</returns>
        public static string AES_Decrypt(String Str, String strAesKey = "123456789.012345678901234.567123")
        {
            Byte[] keyArray = Encoding.UTF8.GetBytes(strAesKey);
            Byte[] toEncryptArray = Convert.FromBase64String(Str);

            using RijndaelManaged rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        public static string RAS_Encrypt(string Str)
        {
            CspParameters param = new CspParameters
            {
                KeyContainerName = "Olive"//密匙容器的名称，保持加密解密一致才能解密成功
            };
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param);
            byte[] plaindata = Encoding.Default.GetBytes(Str);//将要加密的字符串转换为字节数组
            byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
            return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
        }

        public static string RAS_Decrypt(string Str)
        {
            CspParameters param = new CspParameters
            {
                KeyContainerName = "Olive"
            };
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param);
            byte[] encryptdata = Convert.FromBase64String(Str);
            byte[] decryptdata = rsa.Decrypt(encryptdata, false);
            return Encoding.Default.GetString(decryptdata);
        }
    }
}

using Newtonsoft.Json;
using Server.Utils.Crypto_Utils;

namespace Server.Utils.Token_Utils
{
    public class TokenMod
    {
        public string Userid { get; set; }
        public string Ip { get; set; }
        public string Os { get; set; }

        //加密Token
        public string Get_Encrypt_Token(TokenMod tkm)
        {
            string Jsonstr = JsonConvert.SerializeObject(tkm);
            string Token = AESUtils.AES_Encrypt(Jsonstr);
            return Token;
        }

        //解密Token
        public TokenMod Get_Decrypt_Token(string Token)
        {
            string DecryptStr = AESUtils.AES_Decrypt(Token);
            TokenMod tkm = JsonConvert.DeserializeObject<TokenMod>(DecryptStr);
            return tkm;
        }
    }

    public class TokenMod_Admin
    {
        public string Userid { get; set; }
        public string Ip { get; set; }
        public string Os { get; set; }

        //加密Token
        public string Get_Encrypt_Token(TokenMod_Admin tkm)
        {
            string Jsonstr = JsonConvert.SerializeObject(tkm);
            string Token = AESUtils.AES_Encrypt(Jsonstr);
            return Token;
        }

        //解密Token
        public TokenMod_Admin Get_Decrypt_Token(string Token)
        {
            string DecryptStr = AESUtils.AES_Decrypt(Token);
            TokenMod_Admin tkm = JsonConvert.DeserializeObject<TokenMod_Admin>(DecryptStr);
            return tkm;
        }
    }

}
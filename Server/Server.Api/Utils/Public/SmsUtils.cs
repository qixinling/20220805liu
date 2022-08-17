using Newtonsoft.Json;
using Server.Logs;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Server.Utils.Sms_Utils
{
    internal class SmsResult
    {
        public int Stat { get; set; }
        public string Message { get; set; }
    }

    public class SmsUtils
    {
        private static readonly string Uid = "li20180128-20220307liu";
        private static readonly string Pwd = "9e7f7070a8fff7fd97193299bc1b156b";
        private static readonly string Sign = "【金玉满堂】";

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="Tel">手机号</param>
        /// <returns></returns>
        public static Result SendCode(string Tel)
        {
            Result res = new Result();

            try
            {
                using DbConnect dbConnect = DbConnectUtils.GetDbContext();
                Random Rnd = new Random();
                int Code = Rnd.Next(1000, 9999);

                string Msg = "温馨提醒：您的验证码为{"+ Code + "}，为了您的权益，请勿向他人透漏验证码" + Sign;
                res = SendSms(Tel, Msg);
                if (res.Code == 100)
                {
                    DbCheckcode cc = new DbCheckcode
                    {
                        Code = Code.ToString(),
                        Usertel = Tel,
                        Cdate = DateTime.Now
                    };
                    dbConnect.DbCheckcode.Add(cc);
                    dbConnect.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                res.Error("发送验证码异常");

                NLogHelper._.Error(res.Msg, ex);
            }
            return res;
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="Usertel"></param>
        /// <param name="Code">验证码</param>
        /// <returns></returns>
        public static bool Code(string Usertel, string Code)
        {
            bool Pass = false;
            Result res = new Result();
            try
            {
                using DbConnect dbConnect = DbConnectUtils.GetDbContext();
                DbCheckcode cc = dbConnect.DbCheckcode.FirstOrDefault(c => c.Usertel.Equals(Usertel) && c.Code.Equals(Code));
                if (cc != null)
                {
                    dbConnect.DbCheckcode.Remove(cc);
                    dbConnect.SaveChanges();

                    Pass = true;
                }
                else
                {
                    Pass = false;
                }
            }
            catch (Exception ex)
            {
                res.Error("验证验证码异常");

                NLogHelper._.Error(res.Msg, ex);
            }
            return Pass;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Tel">手机号</param>
        /// <param name="Content">内容</param>
        /// <returns></returns>
        public static Result SendSms(string Tel, string Content)
        {
            Result res = new Result();
            if (Tel.Length != 11) { res.Fail("请输入正确的手机号码"); return res; }
            try
            {
                string Url = "ac=send&";
                Url = Url + "uid=" + Uid + "&";
                Url = Url + "pwd=" + Pwd + "&";
                Url = Url + "mobile=" + Tel + "&";
                Url = Url + "content=" + Content;

                string FormUrl = "http://api.sms.cn/sms/";
                string FormData = Url;

                //注意提交的编码 这边是需要改变的 这边默认的是Default：系统当前编码
                byte[] PostData = Encoding.UTF8.GetBytes(FormData);

                // 设置提交的相关参数 
                HttpWebRequest request = WebRequest.Create(FormUrl) as HttpWebRequest;
                Encoding myEncoding = Encoding.UTF8;
                request.Method = "POST";
                request.KeepAlive = false;
                request.AllowAutoRedirect = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR  3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.ContentLength = PostData.Length;

                // 提交请求数据 
                System.IO.Stream outputStream = request.GetRequestStream();
                outputStream.Write(PostData, 0, PostData.Length);
                outputStream.Close();

                HttpWebResponse response;
                Stream responseStream;
                StreamReader reader;
                string SrcString;
                response = request.GetResponse() as HttpWebResponse;
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
                SrcString = reader.ReadToEnd();

                //返回值赋值
                SmsResult result = JsonConvert.DeserializeObject<SmsResult>(SrcString);
                reader.Close();

                res.Code = result.Stat;
                if (res.Code != 100) { res.Code = 0; }
                res.Msg = result.Message;
                return res;
            }
            catch (Exception ex)
            {
                res.Error("发送短信异常");

                NLogHelper._.Error(res.Msg, ex);
                return res;
            }
        }
    }
}

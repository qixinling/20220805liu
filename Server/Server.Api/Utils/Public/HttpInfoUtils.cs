using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Server.Utils.Http_Utils
{
    public static class HttpInfoUtils
    {
        /// <summary>
        /// 获取客户端信息
        /// </summary>
        /// <returns></returns>
        public static string GetOSVersion()
        {
            HttpContextAccessor context = new HttpContextAccessor();
            return context.HttpContext?.Request.Headers[HeaderNames.UserAgent].ToString();
        }

        #region 获取客户端IP地址
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            HttpContextAccessor context = new HttpContextAccessor();
            string Ip = context.HttpContext?.Connection.RemoteIpAddress.ToString();
            if (string.IsNullOrEmpty(Ip))
            {
                return "0.0.0.0";
            }
            return Ip;
        }
        #endregion
    }
}

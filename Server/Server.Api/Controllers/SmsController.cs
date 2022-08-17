using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Server.Models;
using Server.Utils.Sms_Utils;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SmsController : ControllerBase
    {
        [HttpPost]
        public Result SendCode(JObject data)
        {
            string usertel = data["usertel"].ToString();
            //为防止被利用为短信轰炸,注册时提交过来的usertel应该在前后两端加上三位任意字符
            //这边收到后去除前后三位,如果去除后不满足11位手机号码,则发送失败
            usertel = usertel.Remove(0, 3);
            usertel = usertel.Remove(11, 3);
            return SmsUtils.SendCode(usertel);
        }
    }
}
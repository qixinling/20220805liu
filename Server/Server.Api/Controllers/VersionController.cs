using Microsoft.AspNetCore.Mvc;
using Server.Utils.Configuration_Utils;

namespace Server.Api.Controllers
{
    /// <summary>
    /// 版本接口
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VersionController : ControllerBase
    {
        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Getver()
        {
            return ConfigUtils.Configuration["Version"];
        }
    }
}
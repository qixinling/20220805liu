using Microsoft.AspNetCore.Mvc;
using Server.Api.Level;
using Server.Models;
using Server.Models.DataBaseModels;

namespace Server.Api.Controllers.UsersControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LevelController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public LevelController(DbConnect dbConnect,Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 获会员级别名称
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result GetUlevelList()
        {
            return _res.Done(new Ulevel().GetLevels(), "查询成功");
        }

        /// <summary>
        /// 获会员业绩级别名称
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result GetXlevelList()
        {
            return _res.Done(new Xlevel().GetLevels(), "查询成功");
        }

        /// <summary>
        /// 获服务中心级别名称
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result GetBdlevelList()
        {
            return _res.Done(new Bdlevel().GetLevels(), "查询成功");
        }
    }
}

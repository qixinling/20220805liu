using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using Newtonsoft.Json.Linq;
using Server.Api.Method;

namespace Server.Api.Controllers.SlideControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SlideController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public SlideController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询所有幻灯片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result List(JObject data)
        {
            try
            {
                int lx= Convert.ToInt32(data["lx"]);
                List<DbSlide> slidelist = new SlideMethod(_dbConnect).GetList().Where(s => s.Pagelx == lx).ToList();
                _res.Done(slidelist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询幻灯片异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
       
        }
    }
}

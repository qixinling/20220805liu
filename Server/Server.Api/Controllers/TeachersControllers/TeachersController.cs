using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Logs;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using static Server.Api.Filters;

namespace Server.Api.Controllers.TeachersControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TeachersController : ControllerBase
    {

        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public TeachersController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result List()
        {
            try
            {
                List<DbTeachers> dbTeachers = _dbConnect.DbTeachers.Where(s => s.Ispay == 1).ToList();
                _res.Done(dbTeachers, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询师资异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result Get(JObject data)
        {
            int tid = Convert.ToInt32(data["tid"]);
            try
            {
                List<DbTeachers> dbTeachers = _dbConnect.DbTeachers.Where(s =>s.Id == tid).ToList();
                _res.Done(dbTeachers, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询师资异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

    }
}

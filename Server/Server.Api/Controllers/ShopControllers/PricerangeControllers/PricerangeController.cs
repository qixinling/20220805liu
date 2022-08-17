using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using Server.Api.Method;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.ShopControllers.PricerangeControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PricerangeController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public PricerangeController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result List()
        {
            try
            {
                List<DbPricerange> sites = _dbConnect.DbPricerange.ToList();
                _res.Done(sites, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询区间异常");
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
            try
            {
                int sid = Convert.ToInt32(data["sid"]);
                DbSite site = _dbConnect.DbSite.FirstOrDefault(c=>c.Id == sid);
                _res.Done(site, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询单场次异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

    }
}

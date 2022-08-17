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
using static Server.Api.Filters;

namespace Server.Api.Controllers.ShopControllers.SiteController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SiteController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public SiteController(DbConnect dbConnect, Result res)
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
                List<DbSite> sites = _dbConnect.DbSite.ToList();
                _res.Done(sites, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询场次异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {
            try
            {
                string userid = Convert.ToString(data["userid"]);
                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { return _res.Fail("用户信息错误"); }
                DbUsers hsus = _dbConnect.DbUsers.FirstOrDefault(c => c.Id == us.Mystudioid && c.Ulevel == 1);
                if(hsus == null) { return _res.Fail("用户信息错误"); }

                List<DbSite> slist = _dbConnect.DbSite.ToList();
                _res.Done(new { slist, hsus }, "查询成功");
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

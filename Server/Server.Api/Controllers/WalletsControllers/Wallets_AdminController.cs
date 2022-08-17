using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Api.Method;

namespace Server.Api.Controllers.WalletsControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Wallets_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public Wallets_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询个人钱包
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [SignCheckFilters]
        public Result GetWallet(JObject data)
        {
            
            try
            {
                string userid = data["userid"].ToString();
                List<DbWallets> ulist = _dbConnect.DbWallets.Where(u => u.Userid.Equals(userid)).OrderBy(w => w.Cid).ToList();
                _res.Done(ulist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询个人钱包异常");
             
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

    }
}

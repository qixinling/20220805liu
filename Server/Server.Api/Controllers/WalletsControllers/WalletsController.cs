using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Server.Api.Method;

namespace Server.Api.Controllers.WalletsControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 获取所有货币
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result List(JObject data)
        {
            try
            {     
                List<DbWalletsCoin> clist = new WalletsCoinMethod(_dbConnect).GetList().Where(c => c.State == 1).ToList();
                _res.Done(clist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取所有货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询收款人信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result QueryUname(JObject data)
        {
            try
            {
                string suserid = Convert.ToString(data["suserid"]);
                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers users = um.GetByUsersid(suserid);
                if (users != null)
                {
                    _res.Done(users.Username, "查询成功");
                }
            }
            catch (Exception ex)
            {
                _res.Error("查询收款人信息异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询个人钱包
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result GetWallet(JObject data)
        {
            try
            {
                int id = Convert.ToInt32(data["id"]);

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c=>c.Id == id);
                if(us == null) { return _res.Fail("用户信息出错"); }
                List<DbWallets> ulist = new WalletsMethod(_dbConnect).GetListIncludeCoin().Where(u => u.Uid == id && u.CidNavigation.State == 1).OrderByDescending(w => w.Cid).ToList();

                _res.Done(new { ulist , us.Djxyz}, "查询成功");
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

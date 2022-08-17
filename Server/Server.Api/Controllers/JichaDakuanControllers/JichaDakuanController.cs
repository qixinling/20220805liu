using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Wallet.Utils;
using Server.Api.Method;

namespace Server.Api.Controllers.WalletsControllers.JichaDakuanControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class JichaDakuanController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public JichaDakuanController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询充值记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {
            try
            {
                int id = Convert.ToInt32(data["id"]);

                var clist = _dbConnect.DbJichaDakuan.Select(c => new
                {
                    c.Id,
                    c.Uid,
                    c.Userid,
                    c.Username,
                    c.Jine,
                    c.Suid,
                    c.Suserid,
                    c.Susername,
                    c.Cdate,
                    c.Ddate,
                    c.State,
                    c.Liushuihao,
                    c.Dkimg,
                    stateName = c.State == 0 ? "未打款" : c.State == 1 ? "已打款" : "-"
                }).FirstOrDefault(c => c.Id == id);
                _res.Done(clist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询级差打款异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询充值记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result List(JObject data)
        {
            try
            {
                int lx = Convert.ToInt32(data["lx"]);
                int uid = Convert.ToInt32(data["uid"]);

                List<DbJichaDakuan> jcList = new List<DbJichaDakuan>();

                if (lx == 1)
                {
                    jcList = _dbConnect.DbJichaDakuan.Where(c => c.Uid == uid).OrderByDescending(c => c.Id).ToList();
                }
                else if (lx == 2)
                {
                    jcList = _dbConnect.DbJichaDakuan.Where(c => c.Suid == uid).OrderByDescending(c => c.Id).ToList();
                }
                var clist = jcList.Select(c => new
                {
                    c.Id,
                    c.Uid,
                    c.Userid,
                    c.Username,
                    c.Jine,
                    c.Suid,
                    c.Suserid,
                    c.Susername,
                    c.Cdate,
                    c.Ddate,
                    c.State,
                    c.Liushuihao,
                    c.Dkimg,
                    stateName = c.State == 0 ? "未打款" : c.State == 1 ? "已打款" : "-"
                }).ToList();
                _res.Done(clist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询级差打款异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Bonus.Utils;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Bonus;
using Server.Api.Bonus.Algorithm;

namespace Server.Api.Controllers.BonusController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BonusController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public BonusController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Source_UsersList(JObject data)
        {
            try
            {
                int select_uid = Convert.ToInt32(data["select_uid"]);
                int bid = Convert.ToInt32(data["bid"]);
                BonusSourceMethod bsm = new BonusSourceMethod(_dbConnect);
                List<IBonus> bonusList = BonusUtils.BonusList;
                var bslist = bsm.GetList().Where(b => b.Uid == select_uid && b.Btid == bid && b.State == 1).OrderByDescending(m => m.Id).Select(b => new
                {
                    b.Id,
                    b.Btid,
                    b.Yid,
                    b.Yuserid,
                    b.Yusername,
                    b.Uid,
                    b.Userid,
                    b.Username,
                    b.Lx,
                    lxname=bonusList[b.Lx].BonusName,
                    b.Jine,
                    b.Bz,
                    b.Sdate
                });
                _res.Done(bslist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询奖金来源异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Source_List(JObject data)
        {
            try
            {
                int select_uid = Convert.ToInt32(data["select_uid"]);
                List<IBonus> bonusList = BonusUtils.BonusList;
                var bslist = _dbConnect.DbBonusSource.Select(s => new
                {
                    Id = s.Id,
                    Uid = s.Uid,
                    Yuserid = s.Yuserid,
                    Jine = s.Jine,
                    Bz = s.Bz,
                    Lx = s.Lx,
                    Lxname = bonusList[s.Lx].BonusName,
                    State = s.State,
                    Sdate = s.Sdate
                }).Where(b => b.Uid == select_uid && b.State == 1).OrderByDescending(m => m.Id).ToList();

                _res.Done(bslist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询奖金来源异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

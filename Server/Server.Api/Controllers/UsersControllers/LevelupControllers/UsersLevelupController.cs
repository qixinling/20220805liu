using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Api.Method;
using Server.Bonus.Utils;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Wallet.Utils;
using Server.Bill.Utils;
using Server.Api.Level;
using Server.Api.Utils;

namespace Server.Api.Controllers.UsersControllers.LevelupControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersLevelupController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public UsersLevelupController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 申请升级
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Add(JObject data)
        {

            try
            {
                int uid = Convert.ToInt32(data["uid"]);
                string userid = Convert.ToString(data["userid"]);
                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                int newulevel = Convert.ToInt32(data["newulevel"]);

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Id == uid);
                if(us == null) { return _res.Fail("用户信息有误"); }

                if(us.Ulevel > newulevel) { return _res.Fail("请申请大于自己的级别"); }

                if(_dbConnect.DbUsersLevelup.FirstOrDefault(c=>c.Uid == uid && c.State == 0) != null) { return _res.Fail("当前有申请未审核,请勿重复申请"); }

                bool ispay = false;
                if(newulevel == 1 && us.Recount >= 3 && us.Teamcount >= 15 && us.Riteamyeji >= 100000)
                {
                    ispay = true;
                }
                if (newulevel == 2 &&  us.Riteamyeji >= 200000)
                {
                    ispay = true;
                }
                if (newulevel == 3 &&  us.Riteamyeji >= 500000)
                {
                    ispay = true;
                }
                if (newulevel == 4 &&  us.Riteamyeji >= 1000000)
                {
                    ispay = true;
                }
                if (newulevel == 5  && us.Riteamyeji >= 2000000)
                {
                    ispay = true;
                }

                if (!ispay) { return _res.Fail("申请级别条件未达到"); }

                DbUsersLevelup sj = new DbUsersLevelup
                {
                    Userid = userid,
                    Username = us.Username,
                    Uid = us.Id,
                    Ylevel = us.Ulevel,
                    Level = newulevel,
                    Jine = us.Riteamyeji,
                    Sdate = DateTime.Now,
                    State = 0
                };
               _dbConnect.DbUsersLevelup.Add(sj);
                _res.Done(null, "申请成功");


            }
            catch (Exception ex)
            {
                _res.Error("申请异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  查询用户升级记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result List(JObject data)
        {

            try
            {
                int uid = Convert.ToInt32(data["uid"]);

                List<Ulevel> uLevelList = new Ulevel().GetLevels();
                var uulist = _dbConnect.DbUsersLevelup.Where(c=>c.Uid == uid).OrderByDescending(u => u.Id).Select(u => new
                {
                    u.Userid,
                    u.Username,
                    u.Ylevel,
                    ylevelname = uLevelList[u.Ylevel].Name,
                    u.Level,
                    xulevelname = uLevelList[u.Level].Name,
                    u.Jine,
                    u.Sdate,
                    ispay = u.State
                });
                _res.Done(uulist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询升级记录异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

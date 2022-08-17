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

namespace Server.Api.Controllers.WalletsControllers.ChongzhiControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsChongzhiController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsChongzhiController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询可充值货币
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result WalletsChongzhiSelect(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);

                WalletsChongzhiSelectMethod wcsm = new WalletsChongzhiSelectMethod(_dbConnect);

                var clist = wcsm.GetList().Select(c => new
                {
                    c.Id,
                    bid = c.Cid,
                    bname = c.Coinname,
                    c.Codename,
                    jijnemin = c.Jinemin,
                    jinemax = c.Jinemax,
                    jinebs = c.Jinebs,
                    jine = _dbConnect.DbWallets.FirstOrDefault(u => u.Userid.Equals(userid) && u.Cid == c.Cid).Jine
                });

                _res.Done(clist, "查询成功");

            }
            catch (Exception ex)
            {
                _res.Error("查询可充值货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }



        /// <summary>
        /// 充值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result WalletsChongzhi(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                int uid = Convert.ToInt32(data["uid"]);
                int cid = Convert.ToInt32(data["cid"]);
                int bid = Convert.ToInt32(data["bid"]);
                decimal jine = Convert.ToDecimal(data["jine"]);
                string beizhu = Convert.ToString(data["beizhu"]);
                string czimg = Convert.ToString(data["czimg"]);
                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers user = um.GetById(uid);
                if (user == null) { _res.Fail("用户信息错误"); return _res; }


                DbWalletsCoin coin = _dbConnect.DbWalletsCoin.FirstOrDefault(c => c.Id == cid && c.State == 1);

                WalletsChongzhiSelectMethod wcsm = new WalletsChongzhiSelectMethod(_dbConnect);
                DbWalletsChongzhiSelect c = wcsm.GetByCid(cid);
                if (coin == null || c == null) { _res.Fail("货币不存在"); return _res; }

                if (jine < c.Jinemin || jine > c.Jinemax) { _res.Fail("请输入" + c.Jinemin + "-" + c.Jinemax + "之间的金额"); return _res; }
                if (jine % c.Jinebs != 0) { _res.Fail("请输入" + c.Jinebs + "的倍数"); return _res; }
                int Count = _dbConnect.DbWalletsChongzhi.Where(c => c.Uid == user.Id && c.Ispay == 0 && c.Isdelete == 0).Count();
                if (Count > 0) { _res.Fail("您有充值申请正在审核中,请勿重复提交"); return _res; }

                DbWalletsChongzhi cz = new DbWalletsChongzhi
                {
                    Uid = user.Id,
                    Userid = user.Userid,
                    Username = user.Username,
                    Usertel = user.Usertel,
                    Cid = coin.Id,
                    Codename = coin.Codename,
                    Coinname = coin.Coinname,
                    Jine = jine,
                    Lx = 0,
                    Beizhu = beizhu,
                    Cdate = DateTime.Now,
                    Fdate = DateTime.Now,
                    Ispay = 0,
                    Isdelete = 0,
                    Czimg = czimg
                };
                _dbConnect.DbWalletsChongzhi.Add(cz);
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Msg = "申请充值成功,请等待审核";
                }
                else
                {
                    _res.Fail("充值失败");

                }
            }
            catch (Exception ex)
            {
                _res.Error("充值异常");

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
        public Result WalletsChongzhiGet(JObject data)
        {
            try
            {
                string userid = Convert.ToString(data["userid"]);
                int uid = Convert.ToInt32(data["uid"]);
                WalletsChongzhiMethod wcm = new WalletsChongzhiMethod(_dbConnect);
                var clist = wcm.GetList().Where(c => c.Uid == uid && c.Isdelete == 0).OrderByDescending(c => c.Id).Select(c => new
                {
                    c.Id,
                    c.Uid,
                    c.Userid,
                    c.Username,
                    c.Cid,
                    c.Codename,
                    c.Coinname,
                    c.Jine,
                    c.Usertel,
                    c.Beizhu,
                    c.Cdate,
                    c.Fdate,
                    c.Ispay,
                    ispayname = c.Ispay == 0 ? "待审核" : c.Ispay == 1 ? "已审核" : "已撤销"
                }).ToList();
                _res.Done(clist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询充值记录异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Http_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Wallet.Utils;
using Server.Bill.Utils;

namespace Server.Api.Controllers.WalletsControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsZengjian_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsZengjian_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 货币增减
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Zengjian(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }

                int zid = Convert.ToInt32(data["zid"]);
                string userid = data["userid"].ToString();
                decimal jine = Convert.ToDecimal(data["jine"]);
                string beizhu = data["beizhu"].ToString();

                using var transaction = _dbConnect.Database.BeginTransaction();

                WalletsCoinMethod wcm = new WalletsCoinMethod(_dbConnect);
                DbWalletsCoin coin = wcm.GetById(zid);
                if (coin == null) { _res.Fail("货币不存在"); return _res; }
                if (coin.State == 0) { _res.Fail("请先启用货币再修改"); return _res; }

                WalletsMethod wm = new WalletsMethod(_dbConnect);
                DbWallets uw = wm.GetListIncludeUser().FirstOrDefault(u => u.Userid.Equals(userid) && u.Cid == coin.Id);
                if (uw == null) { _res.Fail("用户不存在"); return _res; }

                if (uw.Jine + jine < 0) { _res.Fail("余额不足，操作失败"); return _res; }

                DbWalletsZengjian zj = new DbWalletsZengjian
                {
                    Uid = uw.UidNavigation.Id,
                    Userid = uw.UidNavigation.Userid,
                    Username = uw.UidNavigation.Username,
                    Cid = coin.Id,
                    Codename = coin.Codename,
                    Coinname = coin.Coinname,
                    Lx = 0,
                    Yjine = uw.Jine,
                    Jine = jine,
                    Xjine = uw.Jine + jine,
                    Bz = beizhu,
                    Zdate = DateTime.Now,
                    Isdelete = 0
                };
                _dbConnect.DbWalletsZengjian.Add(zj);

                Result res = WalletsUtils.UpdateBalance(uw.Uid, uw.Cid, jine, _dbConnect);
                if (res.Code == 0) { return res; }

                //创建账单
                IBill bill = new BillZengJian();
                bill.Create(uw.Uid, new Dictionary<int, decimal>
                    {
                        {uw.Cid,jine}
                    }, _dbConnect);

                transaction.Commit();
                _dbConnect.SaveChanges();
                _res.Done(null, "操作成功");
            }
            catch (Exception ex)
            {
                _res.Error("货币增减异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询结算记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List()
        {
            try
            {
                WalletsZengjianMethod wzm = new WalletsZengjianMethod(_dbConnect);
                List<DbWalletsZengjian> zjlist = wzm.GetList().Where(z => z.Isdelete == 0).OrderByDescending(b => b.Zdate).ToList();
                _res.Done(zjlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询增减记录异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除货币增减记录
        /// <returns></returns>
        /// </summary>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Delete(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }
                if(userid_admin != "admin" ) { _res.Fail("删除有误"); return _res; }

                string delete_id = data["delete_id"].ToString();

                string[] Dllist = delete_id.Split(',');
                string msg = "";
              
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbWalletsZengjian zj = _dbConnect.DbWalletsZengjian.FirstOrDefault(b => b.Id == Id && b.Isdelete == 0);
                    if (zj != null)
                    {
                        zj.Isdelete = 1;
                        if (_dbConnect.SaveChanges() > 0)
                        {
                            msg += zj.Userid + "删除成功. ";
                            SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 6, "删除货币增减记录:" + zj.Username);
                        }
                        else
                        {
                            msg += zj.Userid + "删除失败. ";
                        }
                    }
                }
                _res.Done(null, msg);
            }
            catch (Exception ex)
            {
                _res.Error("删除货币增减记录异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }



    }
}

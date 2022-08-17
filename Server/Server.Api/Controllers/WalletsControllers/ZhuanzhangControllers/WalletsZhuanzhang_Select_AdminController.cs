using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Api.Method;

namespace Server.Api.Controllers.WalletsControllers.ZhuanzhangControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsZhuanzhang_Select_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsZhuanzhang_Select_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询所有可转账货币
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
                _res.Done(new WalletsZhuanzhangSelectMethod(_dbConnect).GetList(), "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询所有可转账货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除可转帐货币
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Delete(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                string delete_id = data["delete_id"].ToString();

                string[] Dllist = delete_id.Split(',');
                List<string> Cname = new List<string>();
                List<int> Cid = new List<int>();
                string Msg = "";
                WalletsZhuanzhangSelectMethod wzsm = new WalletsZhuanzhangSelectMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbWalletsZhuanzhangSelect c = wzsm.GetById(Id);
                    if (c != null)
                    {
                        Cid.Add(c.Id);
                        Cname.Add(c.Coinname);

                        Msg += c.Coinname + "删除成功. ";
                        _dbConnect.DbWalletsZhuanzhangSelect.Remove(c);
                    }
                    else
                    {
                        Msg += c.Id + "不存在，删除失败. ";
                    }
                }
                _dbConnect.SaveChanges();
                _res.Done(null, Msg);
            }
            catch (Exception ex)
            {
                _res.Error("删除可转账货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 添加可转账货币
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Add(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                decimal jinemax = Convert.ToDecimal(data["jinemax"]);
                decimal jinemin = Convert.ToDecimal(data["jinemin"]);
                int jinebs = Convert.ToInt32(data["jinebs"]);
                int cid = Convert.ToInt32(data["cid"]);
                decimal shouxu = Convert.ToDecimal(data["shouxu"]);
                int state = Convert.ToInt32(data["state"]);

                WalletsCoinMethod wcm = new WalletsCoinMethod(_dbConnect);
                DbWalletsCoin coin = wcm.GetById(cid);
                if (coin == null) { _res.Fail("货币不存在"); return _res; }
                if (coin.State == 0) { _res.Fail("请先启用货币再添加"); return _res; }

                WalletsZhuanzhangSelectMethod wzsm = new WalletsZhuanzhangSelectMethod(_dbConnect);
                DbWalletsZhuanzhangSelect cs = wzsm.GetByCid(cid);
                if (cs != null) { _res.Fail("记录已存在"); return _res; }

                DbWalletsZhuanzhangSelect t = new DbWalletsZhuanzhangSelect
                {
                    Cid = coin.Id,
                    Codename = coin.Codename,
                    Coinname = coin.Coinname,
                    Jinemin = jinemin,
                    Jinemax = jinemax,
                    Jinebs = jinebs,
                    Shouxu = shouxu,
                    State = state
                };

                _dbConnect.DbWalletsZhuanzhangSelect.Add(t);
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "添加成功");
                }
                else
                {
                    _res.Fail("添加失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("添加可转账货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改转账货币
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Update(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                decimal jinemax = Convert.ToDecimal(data["jinemax"]);
                decimal jinemin = Convert.ToDecimal(data["jinemin"]);
                int jinebs = Convert.ToInt32(data["jinebs"]);
                int cid = Convert.ToInt32(data["cid"]);
                decimal shouxu = Convert.ToDecimal(data["shouxu"]);

                WalletsZhuanzhangSelectMethod wzsm = new WalletsZhuanzhangSelectMethod(_dbConnect);
                DbWalletsZhuanzhangSelect z = wzsm.GetById(cid);
                if (z == null) { _res.Fail("货币信息错误"); return _res; }
                z.Jinemax = jinemax;
                z.Jinemin = jinemin;
                z.Jinebs = jinebs;
                z.Shouxu = shouxu;

                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "修改成功");
                }
                else
                {
                    _res.Fail("修改失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改转账货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 团队限制
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result IsState(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                int zid = Convert.ToInt32(data["zid"]);

                WalletsZhuanzhangSelectMethod wzsm = new WalletsZhuanzhangSelectMethod(_dbConnect);
                DbWalletsZhuanzhangSelect z = wzsm.GetById(zid);
                if (z == null) { _res.Fail("货币信息错误"); return _res; }
                if (z.State == 1)
                {
                    z.State = 0;
                }
                else
                {
                    z.State = 1;
                }

                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "修改成功");
                }
                else
                {
                    _res.Fail("修改失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改团队限制异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

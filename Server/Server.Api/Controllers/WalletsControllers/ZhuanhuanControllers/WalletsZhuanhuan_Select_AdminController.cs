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

namespace Server.Api.Controllers.WalletsControllers.ZhuanhuanControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsZhuanhuan_Select_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsZhuanhuan_Select_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询所有可转换货币
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
                _res.Done(new WalletsZhuanhuanSelectMethod(_dbConnect).GetList(), "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询所有可转换货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除可转换货币
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
                string delete_id = data["delete_id"].ToString();

                string[] Dllist = delete_id.Split(',');
                List<string> Cname = new List<string>();
                List<int> Cid = new List<int>();
                string Msg = "";
                WalletsZhuanhuanSelectMethod wzsm = new WalletsZhuanhuanSelectMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbWalletsZhuanhuanSelect c = wzsm.GetById(Id);
                    if (c != null)
                    {
                        Cid.Add(c.Id);
                        Cname.Add(c.Coinname1 + " 转 " + c.Coinname2);

                        Msg += c.Coinname1 + " 转 " + c.Coinname2 + "删除成功. ";
                        _dbConnect.DbWalletsZhuanhuanSelect.Remove(c);
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
                _res.Error("删除可转换货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 添加可转换货币
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
                int cid1 = Convert.ToInt32(data["cid1"]);
                int cid2 = Convert.ToInt32(data["cid2"]);
                decimal bili = Convert.ToDecimal(data["bili"]);

                WalletsCoinMethod wcm = new WalletsCoinMethod(_dbConnect);
                DbWalletsCoin coin1 = wcm.GetById(cid1);
                DbWalletsCoin coin2 = wcm.GetById(cid2);
                if (coin1 == null || coin2 == null) { _res.Fail("货币不存在"); return _res; }
                if (coin1.State == 0 || coin2.State == 0) { _res.Fail("请先启用货币再添加"); return _res; }

                
                DbWalletsZhuanhuanSelect cs = _dbConnect.DbWalletsZhuanhuanSelect.FirstOrDefault(c => c.Cid1 == cid1 && c.Cid2 == cid2);
                if (cs != null) { _res.Fail("记录已存在"); return _res; }

                DbWalletsZhuanhuanSelect t = new DbWalletsZhuanhuanSelect
                {
                    Cid1 = coin1.Id,
                    Cid2 = coin2.Id,
                    Codename1 = coin1.Codename,
                    Coinname1 = coin1.Coinname,
                    Codename2 = coin2.Codename,
                    Coinname2 = coin2.Coinname,
                    Jinemin = jinemin,
                    Jinemax = jinemax,
                    Jinebs = jinebs,
                    Bili = bili
                };

                _dbConnect.DbWalletsZhuanhuanSelect.Add(t);
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
                _res.Error("添加可转换货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改转换货币
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
                decimal bili = Convert.ToDecimal(data["bili"]);

                WalletsZhuanhuanSelectMethod wzsm = new WalletsZhuanhuanSelectMethod(_dbConnect);
                DbWalletsZhuanhuanSelect z = wzsm.GetById(cid);
                if (z == null) { _res.Fail("货币信息错误"); return _res; }
                z.Jinemax = jinemax;
                z.Jinemin = jinemin;
                z.Jinebs = jinebs;
                z.Bili = bili;

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
                _res.Error("修改转换货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

    }
}

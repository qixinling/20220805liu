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

namespace Server.Api.Controllers.WalletsControllers.TixianController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsTixian_Select_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsTixian_Select_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询所有可提现货币
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
                _res.Done(new WalletsTixianSelectMethod(_dbConnect).GetList(), "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询所有可提现货币异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除可提现货币
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
                WalletsTixianSelectMethod wtsm = new WalletsTixianSelectMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbWalletsTixianSelect c = wtsm.GetById(Id);
                    if (c != null)
                    {
                        Cid.Add(c.Id);
                        Cname.Add(c.Coinname);

                        Msg += c.Coinname + "删除成功. ";
                        _dbConnect.DbWalletsTixianSelect.Remove(c);
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
                _res.Error("删除可提现货币异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 添加可提现货币
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

                WalletsCoinMethod wcm = new WalletsCoinMethod(_dbConnect);
                DbWalletsCoin coin = wcm.GetById(cid);
                if (coin == null) { _res.Fail("货币不存在");  return _res; }
                if (coin.State == 0) { _res.Fail("请先启用货币再添加");  return _res; }

                WalletsTixianSelectMethod wtsm = new WalletsTixianSelectMethod(_dbConnect);
                DbWalletsTixianSelect cs = wtsm.GetByCid(cid);
                if (cs != null) { _res.Fail("记录已存在");  return _res; }

                DbWalletsTixianSelect t = new DbWalletsTixianSelect
                {
                    Cid = coin.Id,
                    Codename = coin.Codename,
                    Coinname = coin.Coinname,
                    Jinemin = jinemin,
                    Jinemax = jinemax,
                    Jinebs = jinebs,
                    Shouxu = shouxu
                };

                _dbConnect.DbWalletsTixianSelect.Add(t);
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
                _res.Error("添加可提现货币异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改提现货币
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

                WalletsTixianSelectMethod wtsm = new WalletsTixianSelectMethod(_dbConnect);
                DbWalletsTixianSelect t = wtsm.GetById(cid);
                if (t == null) { _res.Fail("货币信息错误"); return _res; }
                t.Jinemax = jinemax;
                t.Jinemin = jinemin;
                t.Jinebs = jinebs;
                t.Shouxu = shouxu;

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
                _res.Error("修改提现货币异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

    }
}

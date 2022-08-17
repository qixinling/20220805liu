using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.DataBaseModels;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Api.Utils;
using Server.Api.Method;

namespace Server.Api.Controllers.WalletsControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsCoin_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsCoin_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 修改货币是否使用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result ChangeState(JObject data)
        {
            try
            {
                int id = Convert.ToInt32(data["id"]);

                WalletsCoinMethod wcm = new WalletsCoinMethod(_dbConnect);
                DbWalletsCoin c = wcm.GetById(id);
                if (c == null) { _res.Fail("货币不存在"); return _res; }
                if (c.State == 0)
                {
                    c.State = 1;
                }
                else
                {
                    c.State = 0;
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
                _res.Error("修改货币是否使用异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改货币英文名称
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result ChangeCoinName(JObject data)
        {


            try
            {
                string codename = data["codename"].ToString();
                int id = Convert.ToInt32(data["id"]);

                WalletsCoinMethod wcm = new WalletsCoinMethod(_dbConnect);
                DbWalletsCoin c = wcm.GetById(id);
                if (c == null) { _res.Fail("货币不存在"); return _res; }
                if (c.Codename == codename) { _res.Fail("无修改"); return _res; }

                c.Codename = codename;
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "修改成功");

                    WalletsCoinUtils.CodenameUpdate(c.Id, c.Codename, _dbConnect);
                }
                else
                {
                    _res.Fail("修改失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改货币英文名称异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改货币中文名称
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result ChangeCoinNameZh(JObject data)
        {


            try
            {
                int id = Convert.ToInt32(data["id"]);
                string coinname = data["coinname"].ToString();

                WalletsCoinMethod wcm = new WalletsCoinMethod(_dbConnect);
                DbWalletsCoin c = wcm.GetById(id);
                if (c == null) { _res.Fail("货币不存在"); return _res; }
                if (c.Coinname == coinname) { _res.Fail("无修改"); return _res; }

                c.Coinname = coinname;
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "修改成功");

                    WalletsCoinUtils.CoinnameUpdate(c.Id, c.Coinname, _dbConnect);
                }
                else
                {
                    _res.Fail("修改失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改货币中文名称异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }



        /// <summary>
        /// 查询所有货币
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
                _res.Done(new WalletsCoinMethod(_dbConnect).GetList(), "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询所有货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除货币
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
                WalletsCoinMethod wcm = new WalletsCoinMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbWalletsCoin c = wcm.GetById(Id);
                    if (c != null)
                    {
                        Cid.Add(c.Id);
                        Cname.Add(c.Coinname);

                        Msg += c.Coinname + "删除成功. ";
                        _dbConnect.DbWalletsCoin.Remove(c);
                    }
                    else
                    {
                        Msg += c.Id + "不存在，删除失败. ";
                    }
                }
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, Msg);
                    foreach (int id in Cid)
                    {
                        WalletsCoinUtils.CoinDelete(id, _dbConnect);
                    }
                }
                else
                {
                    _res.Fail(Msg);
                }
            }
            catch (Exception ex)
            {
                _res.Error("删除货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 添加货币
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
                string codename = data["codename"].ToString();
                string coinname = data["coinname"].ToString();

                Result res = new Result();

                DbWalletsCoin c = new DbWalletsCoin
                {
                    Codename = codename,
                    Coinname = coinname,
                    State = 1
                };
                _dbConnect.DbWalletsCoin.Add(c);
                if (_dbConnect.SaveChanges() > 0)
                {
                    res = this.Add_wallet(c.Id, c.Codename, c.Coinname);

                    if (res.Code == 100)
                    {
                        _res.Done(null, c.Coinname + "添加成功");
                        return _res;
                    }
                    else
                    {
                        _res.Fail("添加失败");
                        _dbConnect.Database.ExecuteSqlRaw("delete from `coin` where id=" + c.Id + "");
                    }
                }
                else
                {
                    _res.Done(null, "添加成功");
                }
            }
            catch (Exception ex)
            {
                _res.Error("添加货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 给用户添加新增货币记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        public Result Add_wallet(int Cid, string Cname, string Cname_zh)
        {
            try
            {
                UsersMethod um = new UsersMethod(_dbConnect);
                List<DbUsers> ulist = um.GetList();
                foreach (DbUsers u in ulist)
                {
                    DbWallets uw = new DbWallets
                    {
                        Uid = u.Id,
                        Userid = u.Userid,
                        Cname = Cname,
                        CnameZh = Cname_zh,
                        Jine = 0,
                        Cid = Cid,
                        Wdate = DateTime.Now
                    };

                    _dbConnect.DbWallets.Add(uw);
                }
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
                _res.Error("添加货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


    }
}

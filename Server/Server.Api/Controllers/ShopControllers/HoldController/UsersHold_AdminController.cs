using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Http_Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Api.Utils.Public;
using Server.Wallet.Utils;
using Server.Bill.Utils;
using Microsoft.EntityFrameworkCore;
using Server.Api.Utils;

namespace Server.Api.Controllers.ShopControllers.HoldController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersHold_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public UsersHold_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[TokenAdminCheckFilters]
        //[PermissionCheckFilters]
        //[SignCheckFilters]
        //public Result Update(JObject data)
        //{

        //    try
        //    {
        //        string userid_admin = data["userid_admin"].ToString();
        //        int oid = Convert.ToInt32(data["oid"]);
        //        decimal jine = Convert.ToDecimal(data["jine"]);
        //        DbShopOrder order = _dbConnect.DbShopOrder.FirstOrDefault(c => c.Id == oid);
        //        order.Zsjine = jine;
                
              
        //        _dbConnect.SaveChanges();
        //        _res.Done(null, "修改成功");
        //    }
        //    catch (Exception ex)
        //    {
        //        _res.Error("修改异常");

        //        NLogHelper._.Error(_res.Msg, ex);
        //    }
        //    return _res;
        //}

        /// <summary>
        /// 首单
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
                string suserid = data["suserid"].ToString();
                int cid = Convert.ToInt32(data["cid"]);
                decimal cprice = Convert.ToDecimal(data["cprice"]);
               // decimal zshouyi = Convert.ToDecimal(data["zshouyi"]);
                string userid_admin = Convert.ToString(data["userid_admin"]);

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(suserid));
                if (us == null) { _res.Fail("用户不存在"); return _res; }
                DbJewellery cons = _dbConnect.DbJewellery.FirstOrDefault(c => c.Id == cid);
                if (cons == null) { _res.Fail("产品不存在"); return _res; }

                if (cprice <= 0) { _res.Fail("请填写售卖价格"); return _res; }

                DbUsersBank bank = _dbConnect.DbUsersBank.FirstOrDefault(c => c.Userid.Equals(suserid));
                if (bank == null) { _res.Fail("请先完善账户信息在购买"); return _res; }

               // string repath = us.Repath + us.Id + ",";
               // DbUsers hus = _dbConnect.DbUsers.FirstOrDefault(c => EF.Functions.Like(repath, "%," + c.Id + ",%") && c.Ulevel == 1);
                if (us.Mystudioid == 0) { _res.Fail("没有画室长"); return _res; }

                // decimal mey = cprice * cons.Sybili / 100;
                // decimal kou = cprice * cons.Sjbili / 100;

                Dictionary<string, decimal> bonusDic = SystemSettingBonusUtils.GetBonusParameter(_dbConnect);
                string Orderno = RandomUtils.GetRandom3();

                DbHold newhold = new DbHold();
                newhold.Holdno = Orderno;
                newhold.Uid = us.Id;//卖方
                newhold.Userid = us.Userid;
                newhold.Username = us.Username;
                newhold.Usertel = us.Usertel;
                newhold.Jid = cons.Id;
                newhold.Jname = cons.Name;
                newhold.Jimg = cons.Img;

                newhold.Jprice= cprice;//交易价格
                newhold.Rishouyi = 0;
                newhold.Jsjbili = 0;
                newhold.Jsybili = 0;
                newhold.Zshouyi = 0;
                newhold.State = 0;
                newhold.Hdate = DateTime.Now;
                newhold.Sjjine = cprice * (decimal)2.5 / 100;
                newhold.Hsuid = us.Mystudioid;
                newhold.Oldhsuid = us.Mystudioid;
                newhold.Issd = 1;
                newhold.Hbjine = bonusDic["bs1"];
                _dbConnect.DbHold.Add(newhold);


                DateTime dtime = DateTime.Now;
                DbShoudan sd = _dbConnect.DbShoudan.Where(a => a.Cdate.Year == dtime.Year && a.Cdate.Month == dtime.Month && a.Cdate.Day == dtime.Day && a.Jid == cons.Id && suserid == a.Suserid).FirstOrDefault();
                if (sd != null)
                {
                    sd.Znum += 1;
                    sd.Zprice += cprice;
                    //sd.Zyijia += zshouyi;
                }
                else
                {
                    sd = new DbShoudan
                    {
                       Jid = cons.Id,
                       Jname = cons.Name,
                       Suserid = suserid,
                       Znum = 1,
                       Zprice = cprice,
                       //Zyijia = zshouyi,
                       Cdate = DateTime.Now
                    };
                    _dbConnect.DbShoudan.Add(sd);
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
                _res.Error("手动添加卖方信息异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 卖方列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result SellList(JObject data)
        {

            try
            {
                
                string userid_admin = Convert.ToString(data["userid_admin"]);
                int lx = Convert.ToInt32(data["lx"]);
                List<DbHold> hlist = new List<DbHold>();
                if (lx == 1)
                {
                     hlist = _dbConnect.DbHold.Where(s => s.State > 0 && s.State < 4 && s.Isdelete == 0 && s.Isfc == 0).ToList();
                }
                else
                {
                    hlist = _dbConnect.DbHold.Where(s => s.State == lx && s.Isdelete == 0 && s.Isfc == 0).ToList();
                }
               
                
                _res.Done(hlist, "查询成功");

            }
            catch (Exception ex)
            {
                _res.Error("卖家列表异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        ///// <summary>
        ///// 拆单列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[TokenAdminCheckFilters]
        //[PermissionCheckFilters]
        //[SignCheckFilters]
        //public Result CaidanList(JObject data)
        //{

        //    try
        //    {

        //        string userid_admin = Convert.ToString(data["userid_admin"]);

        //        List<DbHold> hlist = _dbConnect.DbHold.Where(s => s.State == 3 && s.Issj == 1 && s.Iscf == 1 && s.Isdelete == 0).ToList();

        //        _res.Done(hlist, "查询成功");

        //    }
        //    catch (Exception ex)
        //    {
        //        _res.Error("拆单列表异常");

        //        NLogHelper._.Error(_res.Msg, ex);
        //    }
        //    return _res;
        //}

        ///// <summary>
        ///// 拆单
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[TokenAdminCheckFilters]
        //[PermissionCheckFilters]
        //[SignCheckFilters]
        //public Result Caidan(JObject data)
        //{

        //    try
        //    {

        //        string userid_admin = Convert.ToString(data["userid_admin"]);
        //        decimal price = Convert.ToDecimal(data["price"]);
        //        int count = Convert.ToInt32(data["count"]);
        //        decimal zshouyi = Convert.ToDecimal(data["zshouyi"]);
        //        string ids = Convert.ToString(data["ids"]);

        //        List<DbHold> hlist = _dbConnect.DbHold.Where(c => EF.Functions.Like(ids, "%," + c.Id + ",%")).ToList();
        //        foreach (DbHold hold in hlist)
        //        {
        //            decimal mey = Math.Round(price * (decimal)hold.Jsybili / 100, 2);
        //            decimal kou = Math.Round(price * (decimal)hold.Jsjbili / 100, 2);

        //            for (int i = 0; i < count; i++)
        //            {
        //                string Orderno = RandomUtils.GetRandom3();
        //                DbHold newhold = new DbHold();
        //                newhold.Holdno = Orderno;
        //                newhold.Uid = hold.Buid;
        //                newhold.Userid = hold.Buserid;
        //                newhold.Username = hold.Busername;
        //                newhold.Usertel = hold.Busertel;
        //                newhold.Jid = hold.Jid;
        //                newhold.Jname = hold.Jname;
        //                newhold.Jimg = hold.Jimg;
        //                newhold.Jprice = price;//交易价格
        //                newhold.Rishouyi = mey;
        //                newhold.Jsjbili = hold.Jsjbili;
        //                newhold.Jsybili = hold.Jsybili;
        //                newhold.Zshouyi = zshouyi;
        //                newhold.State = 0;
        //                newhold.Issj = 0;
        //                newhold.Hdate = DateTime.Now;
       

        //                _dbConnect.DbHold.Add(newhold);
        //            }
        //            hold.State = 4;//原交易结束
        //        }
        //        _dbConnect.SaveChanges();
        //        _res.Done(null, "拆单成功");

        //    }
        //    catch (Exception ex)
        //    {
        //        _res.Error("拆单异常");

        //        NLogHelper._.Error(_res.Msg, ex);
        //    }
        //    return _res;
        //}

        ///// <summary>
        ///// 返回大厅
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[TokenAdminCheckFilters]
        //[PermissionCheckFilters]
        //[SignCheckFilters]
        //public Result Dating(JObject data)
        //{

        //    try
        //    {

        //        string userid_admin = Convert.ToString(data["userid_admin"]);
        //        string ids = Convert.ToString(data["ids"]);

        //        string zuserid = Convert.ToString(data["zuserid"]);
        //        int zuid = 0;
        //        if (zuserid != "")
        //        {
        //            DbUsers zus = _dbConnect.DbUsers.FirstOrDefault(u => u.Userid.Equals(zuserid));
        //            if (zus == null) { return _res.Fail("指定用户不存在"); }
        //            zuid = zus.Id;
        //        }

        //        List<DbHold> hlist = _dbConnect.DbHold.Where(c => EF.Functions.Like(ids, "%," + c.Id + ",%")).ToList();
        //        foreach (DbHold hold in hlist)
        //        {
                   
        //            hold.State = 4;//交易结束，开始新的卖单


        //            string Orderno = RandomUtils.GetRandom3();
        //            DbHold newhold = new DbHold();
        //            newhold.Holdno = Orderno;
        //            newhold.Uid = hold.Buid;
        //            newhold.Userid = hold.Buserid;
        //            newhold.Username = hold.Busername;
        //            newhold.Usertel = hold.Busertel;
        //            newhold.Jid = hold.Jid;
        //            newhold.Jname = hold.Jname;
        //            newhold.Jimg = hold.Jimg;
        //            newhold.Jprice = hold.Jprice;//交易价格
        //            newhold.Rishouyi = hold.Rishouyi;
        //            newhold.Zshouyi = hold.Zshouyi;
        //            newhold.Jsjbili = hold.Jsjbili;
        //            newhold.Jsybili = hold.Jsybili;
        //            newhold.State = 0;//分红中
        //            newhold.Hdate = DateTime.Now;
        //            newhold.Edate = DateTime.Now.AddHours(24);
        //            newhold.Path = hold.Path + "," + hold.Id;
                  

        //            //指定抢单人
        //            newhold.Zuid = zuid;
        //            newhold.Zuserid= zuserid;

        //            _dbConnect.DbHold.Add(newhold);
        //        }
        //        _dbConnect.SaveChanges();
        //        _res.Done(null, "退回大厅成功");

        //    }
        //    catch (Exception ex)
        //    {
        //        _res.Error("退回大厅异常");

        //        NLogHelper._.Error(_res.Msg, ex);
        //    }
        //    return _res;
        //}


        ///// <summary>
        ///// 申诉列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[TokenAdminCheckFilters]
        //[PermissionCheckFilters]
        //[SignCheckFilters]
        //public Result ShensuList(JObject data)
        //{

        //    try
        //    {

        //        string userid_admin = Convert.ToString(data["userid_admin"]);
        //        List<DbHold> hlist = _dbConnect.DbHold.Where(s => (s.Buyissu == 1 || s.Sellissu == 1) && s.Isdelete == 0).ToList();
        //        _res.Done(hlist, "查询成功");

        //    }
        //    catch (Exception ex)
        //    {
        //        _res.Error("申诉列表异常");

        //        NLogHelper._.Error(_res.Msg, ex);
        //    }
        //    return _res;
        //}


        /// <summary>
        /// 首单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result SdList(JObject data)
        {

            try
            {

                string userid_admin = Convert.ToString(data["userid_admin"]);
                List<DbShoudan> hlist = _dbConnect.DbShoudan.OrderByDescending(c=>c.Cdate).ToList();
                _res.Done(hlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("首单列表异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 
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
                string ids = Convert.ToString(data["ids"]);

                List<DbHold> hlist = _dbConnect.DbHold.Where(s => EF.Functions.Like(ids, "%," + s.Id + ",%") && s.Isdelete == 0).ToList();

                foreach (DbHold item in hlist)
                {
                    item.Isdelete = 1;


                }
                _dbConnect.SaveChanges();
                _res.Done(null, "删除成功");

            }
            catch (Exception ex)
            {
                _res.Error("删除异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        ///// <summary>
        ///// 恢复
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[TokenAdminCheckFilters]
        //[PermissionCheckFilters]
        //[SignCheckFilters]
        //public Result Recovery(JObject data)
        //{

        //    try
        //    {
        //        string ids = Convert.ToString(data["ids"]);

        //        List<DbHold> hlist = _dbConnect.DbHold.Where(s => EF.Functions.Like(ids, "%," + s.Id + ",%") && s.Isdelete == 0).ToList();

        //        foreach (DbHold hold in hlist)
        //        {
        //            if(hold.Zhimg == null)
        //            {
        //                hold.State = 1;
        //            }
        //            else
        //            {
        //                hold.State = 3;
        //                hold.Issj = 0;
                        
        //            }
        //        }

        //        _dbConnect.SaveChanges();
        //        _res.Done(null, "恢复成功");
        //    }
        //    catch (Exception ex)
        //    {
        //        _res.Error("恢复交易异常");

        //        NLogHelper._.Error(_res.Msg, ex);
        //    }
        //    return _res;
        //}

        ///// <summary>
        ///// 交易中的撤销（撤销买家）
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[TokenAdminCheckFilters]
        //[PermissionCheckFilters]
        //[SignCheckFilters]
        //public Result Revoke(JObject data)
        //{

        //    try
        //    {
        //        string ids = Convert.ToString(data["ids"]);
        //        string zuserid = Convert.ToString(data["zuserid"]);
        //        int zuid = 0;
        //        if (zuserid != "")
        //        {
        //            DbUsers zus = _dbConnect.DbUsers.FirstOrDefault(u => u.Userid.Equals(zuserid));
        //            if (zus == null) { return _res.Fail("指定用户不存在"); }
        //            zuid = zus.Id;
        //        }
        //        List<DbHold> hlist = _dbConnect.DbHold.Where(s => EF.Functions.Like(ids, "%," + s.Id + ",%") && s.Isdelete == 0).ToList();

        //        foreach (DbHold hold in hlist)
        //        {
        //           // decimal zjine = hold.Jprice - hold.Zshouyi - hold.Yajin - hold.Zshouyi;
        //           // decimal zshouyi = hold.Zshouyi - hold.Yajin - hold.Rishouyi;

        //            hold.State = 0;
        //            hold.Buid = null;
        //            hold.Buserid = null;
        //            hold.Busername = null;
        //            hold.Busertel = null;
        //            hold.Zrdate = null;
        //            hold.Sellissu = 0;
                 
        //            hold.Zuid = zuid;
        //            hold.Zuserid = zuserid;


        //        }

        //        _dbConnect.SaveChanges();
        //        _res.Done(null, "撤销成功");
        //    }
        //    catch (Exception ex)
        //    {
        //        _res.Error("撤销交易异常");

        //        NLogHelper._.Error(_res.Msg, ex);
        //    }
        //    return _res;
        //}

        ///// <summary>
        ///// 取消申述
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[TokenAdminCheckFilters]
        //[PermissionCheckFilters]
        //[SignCheckFilters]
        //public Result Quxiao(JObject data)
        //{

        //    try
        //    {
        //        string ids = Convert.ToString(data["ids"]);

        //        List<DbHold> hlist = _dbConnect.DbHold.Where(s => EF.Functions.Like(ids, "%," + s.Id + ",%") && s.Isdelete == 0 && s.Buyissu == 1).ToList();

        //        foreach (DbHold hold in hlist)
        //        {
        //            hold.Buyissu = 0;
        //        }

        //        _dbConnect.SaveChanges();
        //        _res.Done(null, "取消申述成功");
        //    }
        //    catch (Exception ex)
        //    {
        //        _res.Error("取消申述异常");

        //        NLogHelper._.Error(_res.Msg, ex);
        //    }
        //    return _res;
        //}

        ///// <summary>
        ///// 修改场次
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[TokenAdminCheckFilters]
        //[PermissionCheckFilters]
        //[SignCheckFilters]
        //public Result UpdateSite(JObject data)
        //{

        //    try
        //    {
        //        string ids = data["ids"].ToString();
        //        int sid = Convert.ToInt32(data["sid"]);

        //        List<DbHold> holdlist = _dbConnect.DbHold.Where(h =>EF.Functions.Like(ids,"%,"+ h.Id + ",%")&& h.State == 0 && h.Isdelete == 0).ToList();
                
        //        DbSite site = _dbConnect.DbSite.FirstOrDefault(h => h.Id == sid);
        //        if (site == null) { return _res.Fail("场次信息出错"); }
        //        foreach (DbHold hold in holdlist)
        //        {
                   

        //            DateTime scdate = Convert.ToDateTime(DateTime.Now.Date + site.Edate);
        //            if (DateTime.Now < scdate)
        //            {
        //                hold.Scdate = scdate;
        //            }
        //            else
        //            {
        //                hold.Scdate = scdate.AddDays(1);
        //            }

        //            DateTime qgdate = Convert.ToDateTime(DateTime.Now.Date + site.Sdate);
        //            if (DateTime.Now < qgdate)
        //            {
        //                hold.Qgdate = qgdate;
        //            }
        //            else
        //            {
        //                hold.Qgdate = qgdate.AddDays(1);
        //            }
        //        }
        //        _dbConnect.SaveChanges();
        //        _res.Done(null, "修改场次成功");
        //    }
        //    catch (Exception ex)
        //    {
        //        _res.Error("修改场次异常");

        //        NLogHelper._.Error(_res.Msg, ex);
        //    }
        //    return _res;
        //}
    }
}

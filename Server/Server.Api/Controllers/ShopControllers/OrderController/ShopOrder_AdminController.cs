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
using Server.Api.Utils;
using Server.Utils.Msg_Utils;
using Microsoft.EntityFrameworkCore;
using Server.Bill.Utils;
using Server.Wallet.Utils;

namespace Server.Api.Controllers.ShopControllers.OrderControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShopOrder_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public ShopOrder_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 修改快递名
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result UpDateKuaidiName(JObject data)
        {


            try
            {
                string kuaidiname = data["kuaidiname"].ToString();
                int id = Convert.ToInt32(data["id"]);
                ShopOrderMethod som = new ShopOrderMethod(_dbConnect );
                DbShopOrder or =som.GetById(id);
                if (or == null) { _res.Fail("订单不存在"); return _res; }
                or.Kuaidiname = kuaidiname;
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "修改已保存");

                }
                else
                {
                    _res.Fail("修改失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改快递名异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改快递编号
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result UpDateKuaidiNo(JObject data)
        {

            try
            {

                string kuaidino = data["kuaidino"].ToString();
                int id = Convert.ToInt32(data["id"]);
                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
                DbShopOrder or =som.GetById (id);
                if (or == null) { _res.Fail("订单不存在"); return _res; }
                or.KuaidiNo = kuaidino;
                if (_dbConnect.SaveChanges() > 0) { _res.Done(null, "修改已保存"); }
                else { _res.Fail("修改失败"); };
            }
            catch (Exception ex)
            {

                _res.Error("修改快递编号异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Pass(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                string ids = data["ids"].ToString();
                if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }

                List<string> Namelist = new List<string>();
                List<decimal> Jinelist = new List<decimal>();


                string[] Add_list = ids.Split(',');//将前端传过来的数据以逗号进行分割

                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
                IBill bill = new BillPay();
                foreach (string Adopt in Add_list)//adopt"通过"
                {
                    int Id = Convert.ToInt32(Adopt);
                    DbShopOrder or = som.GetById(Id);
                    if (or != null)
                    {
                        if (or.Orderstate == 1)
                        {
                            

                            Namelist.Add(or.Username);
                            Jinelist.Add(or.Sjine);

                            if((or.Lx == 2 || or.Lx == 4) && or.Zsjine > 0)
                            {
                                int cid = 2;
                                if(or.Lx == 4)
                                {
                                    cid = 3;//文票积分
                                }
                                Result res = WalletsUtils.UpdateBalance(or.Uid, cid, (decimal)or.Zsjine, _dbConnect);
                                if (res.Code == 0) { return res; }
                                bill.Create(or.Uid, new Dictionary<int, decimal>
                                {
                                    {cid,(decimal)or.Zsjine}
                                }, _dbConnect, "赠送", 1);
                            }
                            or.Orderstate = 2;
                            or.Fdate = DateTime.Now;
                            MsgUtils.Send(0, "订单", "您的订单:" + or.OrderNo + "已发货", 0, "系统消息", (int)or.Uid, or.Userid);

                            if (_dbConnect.SaveChanges() > 0)//新的记录产生判定
                            {
                                _res.Done(null, "发货成功");

                                int i = 0;
                                foreach (var name in Namelist)
                                {
                                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 8, "订单发货:" + name);
                                    i++;
                                }
                            }
                            else
                            {
                                _res.Fail("发货失败");

                            }
                        }
                        else
                        {
                            _res.Fail("发货失败");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _res.Error("数据出错，没有任何订单异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Revoke(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                string ids = data["ids"].ToString();
                if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }

                using var transaction = _dbConnect.Database.BeginTransaction();

                List<string> Namelist = new List<string>();
                List<decimal> Jinelist = new List<decimal>();
                string[] Add_list = ids.Split(',');//将前端传过来的数据以逗号进行分割

                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
                foreach (string Adopt in Add_list)//adopt"通过"
                {
                    int Id = Convert.ToInt32(Adopt);
                    DbShopOrder or = som.Get(Id);
                    if (or.Orderstate == 1 || or.Orderstate == 2)
                    {
                        or.Orderstate = 4;
                        int Uid = or.Uid;

                        decimal jine = 0;

                        //创建账单
                        IBill bill = new BillPay();


                        jine += or.Sjine;

                        int cid = (int)1;
                        decimal amount = or.Sjine;
                        Result res = WalletsUtils.UpdateBalance(or.Uid, 1, amount, _dbConnect);
                        if (res.Code == 0) { return res; }
                        bill.Create(or.Uid, new Dictionary<int, decimal>
                            {
                                {cid,amount}
                            }, _dbConnect, "退款",1);



                        Namelist.Add(or.Username);
                        Jinelist.Add(jine);

                        _dbConnect.SaveChanges();
                        transaction.Commit();

                        MsgUtils.Send(0, "订单", "您的订单:" + or.OrderNo + "已退款", 0, "系统消息", or.Uid, or.Userid);

                        _res.Done(null, "退款成功");

                        int i = 0;
                        foreach (var name in Namelist)
                        {
                            SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 8, "订单退款:" + name);
                            i++;
                        }
                    }
                    else
                    {
                        _res.Fail("退款失败");
                    }
                }
            }
            catch (Exception ex)
            {
                _res.Error("数据出错，没有任何订单异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取子订单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Child_List(JObject data)
        {
            try
            {
                int oc_id = Convert.ToInt32(data["oc_id"]);
                List<DbShopOrderChild> oclist = _dbConnect.DbShopOrderChild.Where(o => o.Oid == oc_id).OrderBy(o => o.Id).ToList();
                _res.Done(oclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询子订单数据异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 根据类型查询待付款、待发货、待收货、已退款数据
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
                int up_id = Convert.ToInt32(data["up_id"]);
                string kuaidiNo = data["kuaidiNo"].ToString();
                string kuaidiname = data["kuaidiname"].ToString();
                string beizhu = data["beizhu"].ToString();

                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
                DbShopOrder or = som.GetById(up_id);
                if (or == null) { _res.Fail("订单信息出错"); return _res; }
                or.KuaidiNo = kuaidiNo;
                or.Kuaidiname = kuaidiname;
                or.Beizhu = beizhu;
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
                _res.Error("修改订单信息异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 根据类型查询单个订单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {

            try
            {
                int id = Convert.ToInt32(data["id"]);
                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
                DbShopOrder or =som.Get(id);
                if (or == null) { _res.Fail("订单信息出错"); return _res; }
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "id", or.Id.ToString() },
                    { "orderNo", or.OrderNo },
                    { "uid", or.Uid.ToString() },
                    { "userid", or.Userid },
                    { "username", or.Username },
                    { "usertel", or.Usertel },
                    { "sheng", or.Sheng },
                    { "shi", or.Shi },
                    { "xian", or.Xian },
                    { "useraddress", or.Useraddress },
                    { "sjine", or.Sjine.ToString() },
                    { "yjine", or.Yjine.ToString() },
                    { "goodslist", or.Goodslist },
                    { "goodsnum", or.Goodsnum.ToString() },
                    { "odate", or.Odate.ToString() },
                     { "zsjine", or.Zsjine.ToString() }
                };
                if (or.Orderstate == 0)
                {
                    dic.Add("orderstate", "待付款");
                }
                else if (or.Orderstate == 1)
                {
                    dic.Add("orderstate", "待发货");
                }
                else if (or.Orderstate == 2)
                {
                    dic.Add("orderstate", "已发货");
                }
                else if (or.Orderstate == 3)
                {
                    dic.Add("orderstate", "已收货");
                }
                else if (or.Orderstate == 4)
                {
                    dic.Add("orderstate", "已退款");
                }
                dic.Add("kuaidiname", or.Kuaidiname);
                dic.Add("kuaidiNo", or.KuaidiNo);
                dic.Add("fdate", DateTime.Now.ToString());
                dic.Add("beizhu", or.Beizhu);

                dic.Add("cost", or.Cost.ToString());
                dic.Add("isdelete", or.Isdelete.ToString());
                dic.Add("bill", or.Bill);
                _res.Done(dic, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询信息异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 根据类型查询待付款、待发货、待收货、已退款数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List(JObject data)
        {
            try
            {

                //1商城订单2自由交割4强制交割3激活订单

                int lx = Convert.ToInt32(data["lx"]);

                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
 
                    var orlist = som.List().Where(o => o.Lx == lx && o.Orderstate > 0 && o.Isdelete == 0).OrderByDescending(m => m.Odate).Select(o => new
                    {
                        o.Id,
                        o.OrderNo,
                        o.Uid,
                        o.Userid,
                        o.Username,
                        o.Usertel,
                        o.Sheng,
                        o.Shi,
                        o.Xian,
                        o.Useraddress,
                        o.Sjine,
                        o.Yjine,
                        o.Goodslist,
                        o.Goodsnum,
                        o.Odate,
                        o.Orderstate,
                        ordername = ShopOrderUtils.Getordername(o.Orderstate),
                        o.Kuaidiname,
                        o.KuaidiNo,
                        kuaidi = o.Kuaidiname + " : " + o.KuaidiNo,
                        fdate = DateTime.Now.ToString(),
                        o.Beizhu,
                        o.Cost,
                        o.Isdelete,
                        o.Bill,
                        o.Lx,
                        o.Zsjine

                    });

                
                    _res.Done(orlist, "查询成功");


            }
            catch (Exception ex)
            {
                _res.Error("查询订单信息异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="data"></param>
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
                if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }
                List<string> Namelist = new List<string>();
                List<decimal> Jinelist = new List<decimal>();
                string[] Dllist = delete_id.Split(',');

                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbShopOrder or = som.GetById(Id);
                    if (or.Isdelete == 0)
                    {
                        or.Isdelete = 1;
                        Namelist.Add(or.Username);
                        Jinelist.Add(or.Sjine);

                    }
                    else
                    {
                        _res.Fail("该状态发生改变,删除失败");

                    }
                }
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "删除成功");

                    int i = 0;
                    foreach (var name in Namelist)
                    {
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 8, "删除订单:" + name);
                        i++;
                    }
                }
                else
                {
                    _res.Fail("删除失败");

                }
            }
            catch (Exception ex)
            {
                _res.Error("删除订单异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

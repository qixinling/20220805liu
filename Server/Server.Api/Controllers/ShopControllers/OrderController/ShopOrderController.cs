using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Api.Method;
using Server.Bonus.Utils;
using Server.Models;
using Server.Models.DataBaseModels;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Server.Api.Controllers.ShopControllers.OrderController;
using Newtonsoft.Json.Linq;
using Server.Bill.Utils;
using Server.Wallet.Utils;
using Server.Bonus;

namespace Server.Api.Controllers.ShopControllers.OrderControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShopOrderController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public ShopOrderController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }


        /// <summary>
        /// 用户激活生成订单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result JihuoAdd(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                int gid = Convert.ToInt32(data["gid"]);
                int aid = Convert.ToInt32(data["aid"]);
                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }


                using var transaction = _dbConnect.Database.BeginTransaction();

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers users = um.GetByUsersid(userid);
                if (users == null) { _res.Fail("用户信息异常"); return _res; }
                if(users.Ispay == 1) { _res.Fail("您的帐号已是激活状态"); return _res; }

                DbShopGoods goods = _dbConnect.DbShopGoods.FirstOrDefault(c => c.Id == gid);
                if (goods == null) { return _res.Fail("产品信息错误"); }

                DbUsersAddress ua = _dbConnect.DbUsersAddress.FirstOrDefault(a => a.Uid == users.Id && a.Id == aid);
                if (ua == null) { _res.Fail("地址信息异常"); return _res; }

                DbWallets uw = _dbConnect.DbWallets.FirstOrDefault(c => c.Uid == users.Id && c.Cid == 2);//信用值
                if (ua == null) { _res.Fail("钱包信息异常"); return _res; }
                if(uw.Jine + users.Djxyz < goods.Xyzjine) { _res.Fail("余额不足"); return _res; }
                DbShopOrder or = new DbShopOrder();

                string Orderno = RandomUtils.OrderNo("");

                or.OrderNo = Orderno;
                or.Uid = users.Id;
                or.Userid = userid;
                or.Username = ua.Username;
                or.Usertel = ua.Usertel;
                or.Sheng = ua.Sheng;
                or.Shi = ua.Shi;
                or.Xian = ua.Xian;
                or.Useraddress = ua.Address;
                List<DbShopOrderChild> Goodslist = new List<DbShopOrderChild>();

                if (goods.Stock < 1) { _res.Fail("库存不足"); return _res; }
                goods.Stock -= 1;
                goods.Sales += 1;

                //子订单
                DbShopOrderChild oc = new DbShopOrderChild
                {
                    OrderNo = Orderno,
                    Oid = or.Id,
                    Gid = goods.Id,
                    Goodsname = goods.Goodsname,
                    Uid = users.Id,
                    Userid = users.Userid,
                    Num = 1,
                    Sjine = goods.Goodsprice,
                    Yjine = goods.Goodsprice
                };
                Goodslist.Add(oc);

                oc.Goodsimg = goods.Goodsimg;
                oc.Cdate = DateTime.Now;

                or.DbShopOrderChild.Add(oc);

                or.Goodslist = JsonConvert.SerializeObject(Goodslist);

                or.Odate = DateTime.Now;
                or.Goodsnum = 1;
                or.Cost = 0;
                or.Kuaidiname = "-";
                or.KuaidiNo = "-";
                or.Sjine = goods.Goodsprice;
                or.Yjine = goods.Goodsprice;
                or.Beizhu = "-";
                or.Orderstate = 1;
                or.Isdelete = 0;
                or.Lx = 3;

                _dbConnect.DbShopOrder.Add(or);

                users.Ispay = 1;
                _dbConnect.SaveChanges();
                transaction.Commit();

                _res.Done(null, "激活成功");
            }
            catch (Exception ex)
            {
                _res.Error("激活异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;


        }


        /// <summary>
        /// 生成订单
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
                string userid = Convert.ToString(data["userid"]);
                string order_childlist = Convert.ToString(data["order_childlist"]);
                int aid = Convert.ToInt32(data["aid"]);
                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }
                List<ShopCarMod> list = JsonConvert.DeserializeObject<List<ShopCarMod>>(order_childlist);

                using var transaction = _dbConnect.Database.BeginTransaction();

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers users = um.GetByUsersid(userid);
                if (users == null) { _res.Fail("用户信息异常"); return _res; }

                
                DbUsersAddress ua = _dbConnect.DbUsersAddress.FirstOrDefault(a => a.Uid == users.Id && a.Id == aid);
                if (ua == null) { _res.Fail("地址信息异常"); return _res; }

                DbShopOrder or = new DbShopOrder();

                string Orderno = RandomUtils.OrderNo("");

                or.OrderNo = Orderno;
                or.Uid = users.Id;
                or.Userid = userid;
                or.Username = ua.Username;
                or.Usertel = ua.Usertel;
                or.Sheng = ua.Sheng;
                or.Shi = ua.Shi;
                or.Xian = ua.Xian;
                or.Useraddress = ua.Address;

                decimal totalPrice = 0;
                List<DbShopOrderChild> Goodslist = new List<DbShopOrderChild>();
                
                foreach (ShopCarMod scm in list)
                {
                    DbShopGoods goods = _dbConnect.DbShopGoods.FirstOrDefault(g => g.Id == scm.Id && g.Ispay == 1);
                    if (goods == null) { _res.Fail("商品已下架"); return _res; }
                    if (goods.Stock < scm.Num) { _res.Fail("库存不足"); return _res; }
                    goods.Stock -= scm.Num;
                    goods.Sales += scm.Num;

                    totalPrice += goods.Goodsprice * scm.Num;

                    or.Pv += goods.Goodspv * scm.Num;
                    or.Goodsnum += scm.Num;

                    //子订单
                    DbShopOrderChild oc = new DbShopOrderChild
                    {
                        OrderNo = Orderno,
                        Oid = or.Id,
                        Gid = goods.Id,
                        Goodsname = goods.Goodsname,
                        Uid = users.Id,
                        Userid = users.Userid,
                        Num = scm.Num,
                        Sjine = goods.Goodsprice,
                        Yjine = goods.Goodsprice
                    };
                    Goodslist.Add(oc);

                    oc.Goodsimg = goods.Goodsimg;
                    oc.Cdate = DateTime.Now;

                    or.DbShopOrderChild.Add(oc);
                }
                or.Goodslist = JsonConvert.SerializeObject(Goodslist);

                or.Odate = DateTime.Now;

                or.Cost = 0;
                or.Kuaidiname = "-";
                or.KuaidiNo = "-";
                or.Sjine = totalPrice;
                or.Yjine = totalPrice;
                or.Beizhu = "-";
                or.Orderstate = 0;
                or.Isdelete = 0;
                or.Lx = 1;

                _dbConnect.DbShopOrder.Add(or);

                //int cid = 3;//默认购物币
                //创建账单
                //IBill bill = new BillPay();
                //Result res = bill.Create(or.Uid, new Dictionary<int, decimal>
                //    {
                //        {cid,totalPrice}
                //    }, _dbConnect, "消费", 0);
                //DbBill newBill = (DbBill)res.Data;
                //newBill.DbShopOrder.Add(or);

                _dbConnect.SaveChanges();
                transaction.Commit();

                _res.Done(null, "生成订单成功");
            }
            catch (Exception ex)
            {
                _res.Error("生成订单异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;


        }

        /// <summary>
        /// 订单支付
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]

        public Result Pay(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                int id = Convert.ToInt32(data["id"]);
                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                using var transaction = _dbConnect.Database.BeginTransaction();

                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
                DbShopOrder or = som.GetById(id);
                if (or == null) { _res.Fail("订单信息错误"); return _res; }
                if (!or.Userid.Equals(userid)) { return _res.Fail("订单信息错误"); }
                if (or.Orderstate != 0) { _res.Fail("订单信息错误"); return _res; }

                DbUsers us = new UsersMethod(_dbConnect).GetByUsersid(userid);
                if (us == null) { return _res.Fail("用户不存在"); }

                decimal jine = or.Sjine;
                Result res = WalletsUtils.PayBalance(us.Id, 1, jine, _dbConnect);
                if (res.Code == 0) { return res.Fail("您的余额不足"); }
                //创建账单
                IBill bill = new BillPay();
                Result res2 = bill.Create(or.Uid, new Dictionary<int, decimal>
                {
                    {1,jine}
                }, _dbConnect, "消费", 0);

                or.Orderstate = 1;
                or.Odate = DateTime.Now;
               // us.Lsk += jine;
                //业绩统计
                YejiUtils ym = new YejiUtils(_dbConnect);
                ym.AddShop(1, or.Goodsnum, jine);


                //尾部参数0为首次激活,升级页面,购物页面传1
               // UsersUtils.AddReteamYeji(us.Reid, us.Repath, yejiJine, or.Goodsnum, 1, _dbConnect);
                _dbConnect.SaveChanges();
                transaction.Commit();

                //BonusUtils.FaFang();

                _res.Done(null, "支付成功");
            }
            catch (Exception ex)
            {
                _res.Error("支付异常");
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
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Delete(JObject data)
        {

            try
            {
                int del_id = Convert.ToInt32(data["del_id "]);
                string userid = Convert.ToString(data["userid"]);
                
                DbShopOrder or = _dbConnect.DbShopOrder.Where(o => o.Id == del_id && o.Userid.Equals(userid)).FirstOrDefault();
                if (or == null) { _res.Fail("订单信息错误"); return _res; }
                if (or.Orderstate > 0) { _res.Fail("订单信息无法修改，无法删除"); return _res; }
                or.Isdelete = 1;

                if (_dbConnect.SaveChanges() != 0)
                {
                    _res.Done(null, "删除成功");
                }
                else
                {
                    _res.Fail("删除失败");

                }
            }
            catch (Exception ex)
            {
                _res.Error("删除异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  订单收货
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Shouhuo(JObject data)
        {

            try
            {
                int sid = Convert.ToInt32(data["sid"]);
                string userid = Convert.ToString(data["userid"]);
               
                DbShopOrder or =_dbConnect.DbShopOrder.Where(o => o.Id == sid && o.Userid.Equals(userid)).FirstOrDefault();
                if (or == null) { _res.Fail("订单不存在"); return _res; }
                if (or.Orderstate != 2) { _res.Fail("请勿重复操作"); return _res; }
                or.Orderstate = 3;

                _dbConnect.SaveChanges();
                _res.Done(null, "收货成功");
            }
            catch (Exception ex)
            {
                _res.Error("收货异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="lx">1商城订单，2提货订单</param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result List(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                int lx = Convert.ToInt32(data["lx"]);
                int orderstate = Convert.ToInt32(data["orderstate"]);
                List<Dictionary<string, object>> diclist = new List<Dictionary<string, object>>();

                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
                List<DbShopOrder> olist = new List<DbShopOrder>();
                if(lx == 2)//提货（交割）订单
                {
                    olist = som.List3().Where(o => o.Isdelete == 0 && (o.Lx == 2 || o.Lx == 4) && o.Userid.Equals(userid) && o.Orderstate == orderstate).OrderByDescending(o => o.Odate).ToList();
                }
                else
                {
                    olist = som.List3().Where(o => o.Isdelete == 0 && (o.Lx == 1 || o.Lx == 3) && o.Userid.Equals(userid) && o.Orderstate == orderstate).OrderByDescending(o => o.Odate).ToList();
                }
                 
                
                foreach (DbShopOrder or in olist)
                {
                    Dictionary<string, object> dic = OrderController.ShopOrderUtils.GetOrderContent(or);
                    diclist.Add(dic);
                }

                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("订单列表异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                int id = Convert.ToInt32(data["id"]);

                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
                DbShopOrder or =som.List3().FirstOrDefault(o => o.Id == id && o.Userid.Equals(userid));
                List<Dictionary<string, object>> diclist = new List<Dictionary<string, object>>();
                Dictionary<string, object> dic = OrderController.ShopOrderUtils.GetOrderContent(or);
                diclist.Add(dic);

                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("订单详情异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
        /// <summary>
        /// 获取订单数量
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]

        [SignCheckFilters]
        public Result Allordernum(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                int lx = Convert.ToInt32(data["lx"]);
                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
                List<DbShopOrder> olist = new List<DbShopOrder>();
                if (lx == 2)
                {
                    olist = som.GetList().Where(o => o.Userid.Equals(userid) && o.Lx == lx && o.Isdelete == 0).ToList();
                }
                else
                {
                    olist = som.GetList().Where(o => o.Userid.Equals(userid) && (o.Lx == 1|| o.Lx == 3) && o.Isdelete == 0).ToList();
                }
                
              
                List<DbShopOrder> newlist = olist.Where(o => o.Orderstate > 0).ToList();
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                foreach (DbShopOrder or in olist.Where(o => o.Orderstate == 0))
                {
                    if (or.Orderstate == 0)
                    {
                        //未支付的订单,10分钟后删除
                        DateTime lastLoginTime = or.Odate.AddMinutes(10);//数据时间
                        if (DateTime.Now >= lastLoginTime)
                        {
                           som.Remove(or.Id);
                            _dbConnect.SaveChanges();
                        }
                        else
                        {
                            newlist.Add(or);
                        }
                    }
                }

                List<int> ocount = new List<int>();
                for (int i = 0; i <= 4; i++)
                {
                    ocount.Add(newlist.Where(o => o.Orderstate == i).Count());
                }
                Dictionary<string, string> dic = new Dictionary<string, string>
                {
                    { "ocount", JsonConvert.SerializeObject(ocount) }
                };
                diclist.Add(dic);
                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询订单数量异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 填写地址
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Dizhi(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                int oid = Convert.ToInt32(data["oid"]);
                int aid = Convert.ToInt32(data["aid"]);

                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
                DbShopOrder or = som.List3().FirstOrDefault(o => o.Id == oid && o.Userid.Equals(userid));
                if(or == null) { _res.Fail("订单信息出错");return _res; }
                DbUsersAddress ua = _dbConnect.DbUsersAddress.FirstOrDefault(c => c.Userid.Equals(userid) && c.Id == aid);
                if (ua == null) { _res.Fail("地址信息出错"); return _res; }
                or.Username = ua.Username;
                or.Usertel = ua.Usertel;
                or.Sheng = ua.Sheng;
                or.Shi = ua.Shi;
                or.Xian = ua.Xian;
                or.Useraddress = ua.Address;
                _dbConnect.SaveChanges();
                _res.Done(null, "操作成功");
            }
            catch (Exception ex)
            {
                _res.Error("填写地址异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

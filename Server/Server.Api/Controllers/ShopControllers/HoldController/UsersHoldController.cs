using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Utils.Msg_Utils;
using Server.Api.Bonus.Algorithm;
using Server.Bonus;
using Server.Utils.Crypto_Utils;
using Microsoft.EntityFrameworkCore;
using Server.Bonus.Utils;
using StackExchange.Redis;
using Server.Api.Utils;
using System.Threading;
using Server.Wallet.Utils;
using Server.Bill.Utils;

namespace Server.Api.Controllers.ShopControllers.HoldControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersHoldController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        private readonly IDatabase _redis;

        public UsersHoldController(DbConnect dbConnect, Result res, RedisUtils redisUtils)
        {
            _dbConnect = dbConnect;
            _res = res;
            _redis = redisUtils.GetDatabase();
        }

        /// <summary>
        ///预约
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result yuyue(JObject data)
        {
            

            int uid = Convert.ToInt32(data["uid"]);
            string userid = data["userid"].ToString();
            int pid = Convert.ToInt32(data["pid"]);

            //判断用户是否访问接口,访问时锁住后续操作,防止用户多次点击
            if (!_redis.StringGet(userid).IsNull) { return _res.Fail("操作过快,请稍后"); }

            try
            {
                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Id == uid);
                if (us == null) { return _res.Fail("用户信息出错"); }

               DbPricerange price = _dbConnect.DbPricerange.FirstOrDefault(c => c.Id == pid);
               if (price == null) { return _res.Fail("价位信息出错"); }

               DbHold hold = _dbConnect.DbHold.FirstOrDefault(c => c.Isyy == 1&& c.Isdelete == 0 && c.Buid == uid && c.Qgdate.Date == DateTime.Now.Date );
                if(hold != null) { return _res.Fail("今天已预约过"); }

                decimal jprice = _dbConnect.DbHold.Where(c=>c.Buid == uid && c.Qgdate.Date == DateTime.Now.Date && c.Isdelete == 0).Sum(c=>c.Jprice);
                if(jprice >= 31888) { return _res.Fail("今日抢购已达上限"); }

                DbHold dbHold = null;
                if (pid < 4)
                {
                     dbHold = _dbConnect.DbHold.FirstOrDefault(c => c.State == 0 && c.Isdelete == 0 && c.Isfc == 0 && c.Jprice >= price.Minprice && c.Jprice < price.Maxprice  && c.Hsuid == us.Mystudioid);
                }
                else
                {
                    dbHold = _dbConnect.DbHold.FirstOrDefault(c => c.State == 0 && c.Isdelete == 0 && c.Isfc == 0 && c.Jprice >= price.Minprice && c.Hsuid == us.Mystudioid);
                }
                if (dbHold == null) { return _res.Fail("该预约已满"); }

                Dictionary<string, decimal> bonusDic = SystemSettingBonusUtils.GetBonusParameter(_dbConnect);

                decimal yajin = bonusDic["bs2"];

                DbWallets uw = _dbConnect.DbWallets.FirstOrDefault(c => c.Uid == us.Id && c.Cid == 1);
                if(uw.Jine < yajin) { return _res.Fail("画贝余额不足"); }
                do
                {
                    using DbConnect dbConnect = DbConnectUtils.GetDbContext();

                    Random r = new Random();

                    if (dbConnect.Database.ExecuteSqlRaw($"update `db_hold` set version='{$"{DateTime.Now.Ticks}{r.Next(10000, 99999)}"}' where id={dbHold.Id} and version='{dbHold.Version}'") == 1)
                    {
                        
                        us.Lsk += dbHold.Jprice;
                        us.Ylsk += dbHold.Jprice;//用于记录今日买入
                        dbHold.State = 1;
                        dbHold.Buid = us.Id;
                        dbHold.Buserid = us.Userid;
                        dbHold.Busername = us.Username;
                        dbHold.Busertel = us.Usertel;
                        dbHold.Qgdate = DateTime.Now;
                        dbHold.Isyy = 1;
                        dbHold.Pid = pid;
                        dbHold.Yajin = yajin;
                        if(yajin > 0)
                        {
                            Result res = WalletsUtils.PayBalance(uw.Uid, uw.Cid, yajin, _dbConnect);
                            if (res.Code == 0) { break; }
                            IBill bill = new BillPay();
                            bill.Create(us.Id, new Dictionary<int, decimal> { { 1, yajin } }, _dbConnect, "冻结", 0);

                            res = WalletsUtils.UpdateBalance(uw.Uid, 3, yajin, _dbConnect);
                            bill.Create(us.Id, new Dictionary<int, decimal> { { 3, yajin } }, _dbConnect, "冻结", 0);
                            if (res.Code == 0) { break; }
                        }
                        

                        break;
                    }
                    else
                    {
                        return _res.Fail("预约失败");
                    }

                } while (true);

                _dbConnect.SaveChanges();

               

                _res.Done(null, "预约成功，请尽快完成打款");


            }
            catch (Exception ex)
            {
                _res.Error("预约失败");
                NLogHelper._.Error(_res.Msg, ex);
            }
            
            return _res;
        }



        /// <summary>
        ///抢购
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Buy(JObject data)
        {

            int hid = Convert.ToInt32(data["hid"]);
            int uid = Convert.ToInt32(data["uid"]);
            string userid = data["userid"].ToString();

            //判断用户是否访问接口,访问时锁住后续操作,防止用户多次点击
            if (!_redis.StringGet(userid).IsNull) { return _res.Fail("操作过快,请稍后"); }
            _redis.StringSet(userid, hid, TimeSpan.FromSeconds(2)); //接口结束时弹出,才能重新打开其他接口

            try
            {
                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Id == uid);
                if (us == null) { return _res.Fail("用户信息出错"); }

                decimal jprice = _dbConnect.DbHold.Where(c => c.Buid == uid && c.Qgdate.Date == DateTime.Now.Date && c.Isdelete == 0).Sum(c => c.Jprice);
                if (jprice >= 31888) { return _res.Fail("今日抢购已达上限"); }

                //判断单子ID是否存在,不存在时该用户是第一个人,锁住hid不让其他人进入
                if (!_redis.StringGet(hid.ToString()).IsNull) { return _res.Fail("抢单失败"); }
               

                DbHold hold = _dbConnect.DbHold.FirstOrDefault(h => h.Id == hid);
                if (hold == null) { return _res.Fail("抢购信息出错"); }
                //if (hold.Userid.Equals(userid)) { return _res.Fail("不可与自己卖单交易"); }
                if (hold.State != 0) { return _res.Fail("该单已售出"); }
                if (hold.Isdelete != 0) { return _res.Fail("该单已删除"); }

                Dictionary<string, decimal> bonusDic = SystemSettingBonusUtils.GetBonusParameter(_dbConnect);

                decimal yajin = bonusDic["bs2"];
               
                DbWallets uw = _dbConnect.DbWallets.FirstOrDefault(c => c.Uid == us.Id && c.Cid == 1);
                if (uw.Jine < yajin) { return _res.Fail("画贝余额不足"); }

                do
                {
                    using DbConnect dbConnect = DbConnectUtils.GetDbContext();

                    Random r = new Random();

                    if(dbConnect.Database.ExecuteSqlRaw($"update `db_hold` set version='{$"{DateTime.Now.Ticks}{r.Next(10000, 99999)}"}' where id={hid} and version='{hold.Version}'") == 1)
                    {
                        us.Lsk += hold.Jprice;
                        us.Ylsk += hold.Jprice;//用于记录今日买入
                        hold.State = 1;
                        hold.Buid = us.Id;
                        hold.Buserid = us.Userid;
                        hold.Busername = us.Username;
                        hold.Busertel = us.Usertel;
                        hold.Qgdate = DateTime.Now;
                        hold.Yajin = yajin;


                        if(yajin > 0)
                        {
                            Result res = WalletsUtils.PayBalance(uw.Uid, uw.Cid, yajin, dbConnect);
                            if (res.Code == 0) { break; }
                            IBill bill = new BillPay();
                            bill.Create(us.Id, new Dictionary<int, decimal> { { 1, yajin } }, dbConnect, "冻结", 0);

                            res = WalletsUtils.UpdateBalance(uw.Uid, 3, yajin, _dbConnect);
                            bill.Create(us.Id, new Dictionary<int, decimal> { { 3, yajin } }, dbConnect, "冻结", 0);
                            if (res.Code == 0) { break; }
                        }

                        break;
                    }
                    else
                    {
                        return _res.Fail("抢购失败");
                    }
                  
                } while(true);


                _dbConnect.SaveChanges();
               
                _res.Done(null, "竞拍成功，请尽快完成打款");

            }
            catch (Exception ex)
            {
                _res.Error("竞拍失败");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }



        /// <summary>
        /// 竞拍大厅列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result List(JObject data)
        {
            try
            {
                string userid = Convert.ToString(data["userid"]);
                int uid = Convert.ToInt32(data["uid"]);
                int pageIndex = Convert.ToInt32(data["pageIndex"]);
                int pageSize = Convert.ToInt32(data["pageSize"]);

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Id == uid);
                if (us == null) { return _res.Fail("用户信息错误"); }

                List<DbHold> hlist = new List<DbHold>();
                int pagecount = 0;
                DbSite site = _dbConnect.DbSite.FirstOrDefault(c => c.Id == 2 && c.Ispay == 1);//开抢的时段
                int week = (int)DateTime.Now.DayOfWeek;
                
                hlist = _dbConnect.DbHold.Where(c => c.State == 0 && c.Isfc == 0 && (DateTime.Now.Date > c.Sjdate.Date || c.Issd == 1 || c.Iscf ==1) && c.Hsuid == us.Mystudioid &&  c.Isdelete == 0).OrderByDescending(c => c.Jprice).ToList();

                pagecount = (int)Math.Ceiling((float)hlist.Count / pageSize);
                hlist = hlist.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                
                _res.Done(new { week, hlist, pagecount }, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 我的订单（预约,抢购）
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result MyOrderList(JObject data)
        {
            try
            {
                string userid = Convert.ToString(data["userid"]);
                int lx = Convert.ToInt32(data["state"]);

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }
                DbSite site = _dbConnect.DbSite.FirstOrDefault(c => c.Id == 2);//打款时间

                var hlist = _dbConnect.DbHold.Where(h => (h.Buserid.Equals(userid) || h.Userid.Equals(userid)) && h.Isdelete == 0 && h.State > 0 && h.Isfc == 0).Select(c => new
                {
                    c.Id,
                    c.Holdno,
                    c.Uid,
                    c.Userid,
                    c.Username,
                    c.Usertel,
                    c.Jid,
                    c.Jimg,
                    c.Jname,
                    c.Jprice,
                    c.Rishouyi,
                    c.Zshouyi,
                    c.State,
                    Statename = ShopOrderUtils.Getstatename((int)c.State),
                    c.Buid,
                    c.Buserid,
                    c.Busertel,
                    c.Busername,
                    c.Qgdate,
                    c.Dkdate,
                    c.Skdate,
                    c.Issj,
                    c.Sjimg,
                    c.Sjjine,
                    c.Zhimg,
                    banklist = _dbConnect.DbUsersBank.Where(u => u.Uid == c.Uid).ToList(),
                    hsbanklist = _dbConnect.DbUsersBank.Where(u => u.Uid == c.Hsuid).ToList()
                }).ToList();


                if (lx == 99)//我的订单(买家待付款)
                {
                    hlist = hlist.Where(c => c.Buid == us.Id && c.State != 4).ToList();
                }
                if (lx == 1)//我的订单(买家待付款)
                {
                    hlist = hlist.Where(c=>c.Buid == us.Id && (c.State == 1 || c.State == 2)).ToList();
                }
                if(lx == 2)//我的订单(卖家待收款)
                {
                    hlist = hlist.Where(c => c.Uid == us.Id && c.State >= 1 && c.State <=2).ToList();
                }
                if (lx == 3)//我的订单(买家待上架)
                {
                    hlist = hlist.Where(c => c.Buid == us.Id && c.State == 3).ToList();
                }
                if (lx == 4)//我的订单(卖家已出售)
                {
                    hlist = hlist.Where(c => c.Uid == us.Id && c.State >=3).ToList();
                }

                _res.Done(new { hlist, site }, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 我的订单sell
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result MyOrderList2(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                int state = Convert.ToInt32(data["state"]);

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                var hlist = _dbConnect.DbHold.Where(c => c.Userid.Equals(userid)&& c.Isdelete == 0 && c.State <3 ).OrderByDescending(c => c.Qgdate).Select(c => new
                {
                    c.Id,
                    c.Holdno,
                    c.Userid,
                    c.Username,
                    c.Usertel,
                    c.Jid,
                    c.Jimg,
                    c.Jname,
                    c.Jprice,
                    c.Rishouyi,
                    c.Zshouyi,
                    c.State,
                    Statename = ShopOrderUtils.Getstatename((int)c.State),
                    c.Buid,
                    c.Buserid,
                    c.Busertel,
                    c.Busername,
                    c.Zhimg,
                    c.Dkdate,
                    c.Skdate,
                    c.Issj,
                    c.Isfc,
                    c.Zrdate,
                    c.Sjjine,
                    banklist = _dbConnect.DbUsersBank.Where(u => u.Uid == c.Uid).ToList()
                }).ToList();

                if(state > 0 && state < 3)//我的字画(持有中)
                {
                    hlist = hlist.Where(c => c.State == state && c.Isfc == 0).ToList();
                }
                if(state == 3)
                {
                    hlist = hlist.Where(c => c.Isfc == 1).ToList();
                }
               
               _res.Done(hlist, "查询成功");
 
            }
            catch (Exception ex)
            {
                _res.Error("查询异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///买方提交付款凭证
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Fukuan(JObject data)
        {
            try
            {
                int hid = Convert.ToInt32(data["hid"]);
                string zfimg = data["zfimg"].ToString();
                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();
               // string password2 = data["password2"].ToString();

                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }
                DbHold hold = _dbConnect.DbHold.FirstOrDefault(c => c.Buid == uid && c.Id == hid && c.Isdelete == 0);
                if (hold == null) { _res.Fail("交易信息出错"); return _res; }
                if (hold.State == 2) { _res.Fail("已确认过付款"); return _res; }
                if (zfimg == "") { _res.Fail("请上传付款凭证"); return _res; }

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                //if (string.IsNullOrWhiteSpace(password2)) { _res.Fail("支付密码不能为空"); return _res; }
                //string Password2md5 = MD5Utils.MD5Encrypt(password2, 32);
                //if (!us.Password2.Equals(Password2md5)) { _res.Fail("支付密码错误"); return _res; }

                hold.Zhimg = zfimg;
                hold.Dkdate = DateTime.Now;
                hold.State = 2;

                _dbConnect.SaveChanges();
                MsgUtils.Send(0, "交易", hold.Jname + "该交易买家已确定付款，注意查收", 0, "系统消息", (int)hold.Uid, hold.Userid);
                _res.Done(null, "付款成功");
            }
            catch (Exception ex)
            {
                _res.Error("提交凭证异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///买方提交上架申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Fukuan2(JObject data)
        {
            try
            {
                int hid = Convert.ToInt32(data["hid"]);
                
                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();

                int lx = Convert.ToInt32(data["lx"].ToString());//1画贝2线下
                // string password2 = data["password2"].ToString();

                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                DbHold hold = _dbConnect.DbHold.FirstOrDefault(c => c.Buid == uid && c.Id == hid && c.Isdelete == 0);
                if (hold == null) { _res.Fail("交易信息出错"); return _res; }

                string msg = "";
                if(lx == 2) {
                    string sjimg = data["sjimg"].ToString();
                    if (hold.Sjimg != null) { _res.Fail("您已上传过凭证"); return _res; }
                    if (sjimg == "") { _res.Fail("请上传付款凭证"); return _res; }
                    hold.Sjimg = sjimg;
                    hold.Issj = 1;
                    hold.Sjdate = DateTime.Now.AddDays(1);//用于判断显示大厅时间
                    msg = "申请成功";
                }
                else if(lx == 1)//上架画贝支付
                {
                    decimal zshouyi = hold.Jprice * 4 / 100;//新一轮打款金额打款
                    decimal zjine = hold.Jprice + zshouyi;
                    decimal sjjine = zjine * (decimal)2.5 / 100;

                    Result res = WalletsUtils.PayBalance(us.Id, 1, hold.Sjjine, _dbConnect);
                    if (res.Code == 0) { return _res.Fail(res.Msg); }
                    IBill bill = new BillPay();
                    bill.Create(us.Id, new Dictionary<int, decimal> { { 1, hold.Sjjine } }, _dbConnect, "上架费", 0);

                    res = WalletsUtils.UpdateBalance(hold.Hsuid, 1, hold.Sjjine, _dbConnect);
                    bill.Create(us.Id, new Dictionary<int, decimal> { { 1, hold.Sjjine } }, _dbConnect, us.Userid+"上架打款", 0);
                    if (res.Code == 0) { return _res.Fail(res.Msg); }

                    hold.State = 4;//交易结束，开始新的卖单
                    string Orderno = RandomUtils.GetRandom3();
                    DbHold newhold = new DbHold();
                    newhold.Holdno = Orderno;
                    newhold.Uid = hold.Buid;
                    newhold.Userid = hold.Buserid;
                    newhold.Username = hold.Busername;
                    newhold.Usertel = hold.Busertel;
                    newhold.Jid = hold.Jid;
                    newhold.Jname = hold.Jname;
                    newhold.Jimg = hold.Jimg;
                    newhold.Jprice = zjine;//交易价格
                    newhold.Rishouyi = 0;
                    newhold.Zshouyi = zshouyi;
                    newhold.Jsjbili = 0;
                    newhold.Jsybili = 0;
                    newhold.State = 0;//分红中
                    newhold.Hdate = DateTime.Now;
                    newhold.Path = hold.Path + "," + hold.Id;
                    newhold.Repath = us.Repath;
                    newhold.Sjjine = sjjine;
                    newhold.Hsuid = hold.Hsuid;
                    newhold.Oldhsuid = hold.Oldhsuid;
                    newhold.Hbjine = hold.Hbjine;
                    _dbConnect.DbHold.Add(newhold);
                    _dbConnect.SaveChanges();

                    List<IBonus> bonusList = BonusUtils.BonusList;
                    bonusList[1].Execute((int)hold.Buid, hold.Jprice);
                    BonusUtils.JiCha(hold.Hsuid, hold.Jprice);
                    BonusUtils.FaFang();

                    msg = "上架成功";

                }
                else
                {
                    _res.Fail("未知类型"); return _res;
                }

                _dbConnect.SaveChanges();

                DbUsers hsus = _dbConnect.DbUsers.FirstOrDefault(c=>c.Id == hold.Hsuid);              
                if(hsus != null)
                {
                    MsgUtils.Send(0, "交易", hold.Jname + "该交易买家已申请上架，注意查收", 0, "系统消息", (int)hsus.Id, hold.Userid);
                }
               
                _res.Done(null, msg);
            }
            catch (Exception ex)
            {
                _res.Error("提交凭证异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///卖方确认收款
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Shoukuan(JObject data)
        {

            try
            {
                int hid = Convert.ToInt32(data["hid"]);
                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();
               // string password2 = data["password2"].ToString();

                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                //if (string.IsNullOrWhiteSpace(password2)) { _res.Fail("支付密码不能为空"); return _res; }
                //string Password2md5 = MD5Utils.MD5Encrypt(password2, 32);
                //if (!us.Password2.Equals(Password2md5)) { _res.Fail("支付密码错误"); return _res; }

                DbHold hold = _dbConnect.DbHold.FirstOrDefault(c => c.Uid == uid && c.Id == hid && c.State == 2 && c.Isdelete == 0);
                if (hold == null) { _res.Fail("交易信息出错"); return _res; }
                if (hold.State != 2) { _res.Fail("买家未确认付款"); return _res; }

                hold.State = 3;
                hold.Skdate = DateTime.Now;

                Result res = WalletsUtils.PayBalance((int)hold.Uid, 3, hold.Yajin, _dbConnect);
                IBill bill = new BillPay();
                bill.Create(us.Id, new Dictionary<int, decimal> { { 3, hold.Yajin } }, _dbConnect, "解冻", 0);

                res = WalletsUtils.UpdateBalance((int)hold.Uid, 1, hold.Yajin, _dbConnect);
                bill.Create(us.Id, new Dictionary<int, decimal> { { 1, hold.Yajin } }, _dbConnect, "解冻", 0);

                DbUsers buyus = _dbConnect.DbUsers.FirstOrDefault(c => c.Id == hold.Buid);

                //完成交易算业绩
                UsersUtils.AddReteamYeji(buyus.Reid, buyus.Repath, hold.Jprice, 1, 1, _dbConnect);

                _dbConnect.SaveChanges();
                _res.Done(null, "您已收款，交易成功");
            }
            catch (Exception ex)
            {
                _res.Error("卖家确认收款异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        ///上架(转售)通过
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result sjpass(JObject data)
        {

            try
            {
                int hid = Convert.ToInt32(data["hid"]);

                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();

                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }
                DbUsersBank bank = _dbConnect.DbUsersBank.FirstOrDefault(c => c.Uid == uid);
                if (bank == null) { _res.Fail("请先完善账户信息"); return _res; }

                DbHold hold = _dbConnect.DbHold.FirstOrDefault(c => c.Id == hid && c.Isdelete == 0);
                if (hold == null) { _res.Fail("交易信息出错"); return _res; }

                //DateTime edate = Convert.ToDateTime(hold.Zsdate.Date + site.Edate);
                if (hold.State == 4) { _res.Fail("已通过审核,请勿重复操作"); return _res; }

                decimal zshouyi = hold.Jprice * 4 / 100;//新一轮打款金额打款
                decimal zjine = hold.Jprice + zshouyi;
                decimal sjjine = zjine * (decimal)2.5 / 100;

                hold.State = 4;//交易结束，开始新的卖单
                string Orderno = RandomUtils.GetRandom3();
                DbHold newhold = new DbHold();
                newhold.Holdno = Orderno;
                newhold.Uid = hold.Buid;
                newhold.Userid = hold.Buserid;
                newhold.Username = hold.Busername;
                newhold.Usertel = hold.Busertel;
                newhold.Jid = hold.Jid;
                newhold.Jname = hold.Jname;
                newhold.Jimg = hold.Jimg;
                newhold.Jprice = zjine;//交易价格
                newhold.Rishouyi = 0;
                newhold.Zshouyi = zshouyi;
                newhold.Jsjbili = 0;
                newhold.Jsybili = 0;
                newhold.State = 0;//分红中
                newhold.Hdate = DateTime.Now;
               // newhold.Edate = DateTime.Now.AddHours(24);
                newhold.Path = hold.Path + "," + hold.Id;
                newhold.Repath = us.Repath;
                newhold.Sjjine = sjjine;
                newhold.Hsuid = hold.Hsuid;
                newhold.Hbjine = hold.Hbjine;
                newhold.Oldhsuid = hold.Oldhsuid;
                _dbConnect.DbHold.Add(newhold);
                _dbConnect.SaveChanges();

                List<IBonus> bonusList = BonusUtils.BonusList;
                bonusList[1].Execute((int)hold.Buid, hold.Jprice);
                BonusUtils.JiCha(hold.Hsuid, hold.Jprice);
                BonusUtils.FaFang();

                _res.Done(null, "上架成功");

            }
            catch (Exception ex)
            {
                _res.Error("转售异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        ///画室长审核列表 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result SjList(JObject data)
        {

            try
            {
                int state = Convert.ToInt32(data["state"]);

                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                List<DbHold> hlist = _dbConnect.DbHold.Where(c => c.Hsuid == us.Id && c.Isdelete == 0).ToList();
                if(state == 1)//待审
                {
                    hlist = hlist.Where(c => c.Issj == 1 && c.State == 3).ToList();

                }else if(state == 2)//未上架
                {
                    hlist = hlist.Where(c => (c.State > 0 && c.State <= 3) && c.Issj == 0).OrderByDescending(c=>c.Qgdate).ToList();
                   
                }
                else if (state == 3)//已审
                {
                    hlist = hlist.Where(c => c.State == 4).ToList();
                }

                _res.Done(hlist, "查询成功");

            }
            catch (Exception ex)
            {
                _res.Error("查询异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///画室长转画列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result ZhList(JObject data)
        {

            try
            {
                int state = Convert.ToInt32(data["state"]);

                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                List<DbHold> hlist = new List<DbHold>();
                if (state == 0)//待转画
                {
                    hlist = _dbConnect.DbHold.Where(c => c.Hsuid == us.Id && c.State == 0 && c.Isdelete == 0 && c.Isfc == 0).ToList();

                }
                else if (state == 1)//转出
                {
                    hlist = _dbConnect.DbHold.Where(c => c.Oldhsuid == us.Id && c.Hsuid != us.Id  && c.Isdelete == 0 && c.Isfc == 0).ToList();

                }
                else if (state == 2)//转入
                {
                    hlist = _dbConnect.DbHold.Where(c => c.Oldhsuid != us.Id && c.Hsuid == us.Id  && c.Isdelete == 0 && c.Isfc == 0).ToList();
                }

                _res.Done(hlist, "查询成功");

            }
            catch (Exception ex)
            {
                _res.Error("查询异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///画室长转画
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Zhuanhua(JObject data)
        {

            try
            {
                string suserid =data["suserid"].ToString();
                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();
                int hid = Convert.ToInt32(data["hid"]);

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                DbUsers sus = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(suserid));
                if (sus == null) { _res.Fail("用户信息出错"); return _res; }
                if(sus.Ulevel < 1) { return _res.Fail("收画人不是画室长"); }


                DbHold hold = _dbConnect.DbHold.FirstOrDefault(c => c.Id == hid && c.Isdelete == 0);
                if(hold == null) { return _res.Fail("转画信息错误"); }
                if(hold.State != 0) { return _res.Fail("转画状态错误"); }

                DbUsers hsus = _dbConnect.DbUsers.FirstOrDefault(c => c.Id == hold.Uid);
                if (hsus == null) { _res.Fail("用户信息出错"); return _res; }

                hsus.Mystudioid = sus.Id;
                hsus.Mystudioname = sus.Userid;
                hold.Hsuid = sus.Id;
                _dbConnect.SaveChanges();

                _res.Done(null, "转画成功");

            }
            catch (Exception ex)
            {
                _res.Error("转画异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 画室长推广订单
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result TgOrderList(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                int state = Convert.ToInt32(data["state"]);

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                int jinrinum = _dbConnect.DbHold.Where(c => c.Hsuid == us.Id && c.Qgdate.Date == DateTime.Now.Date && c.Isdelete == 0 && c.Isfc == 0).Count();
                decimal jinriprice = _dbConnect.DbHold.Where(c => c.Hsuid == us.Id && c.Qgdate.Date == DateTime.Now.Date && c.Isdelete == 0 && c.Isfc == 0).Sum(c => c.Jprice);

                if (state != 99)//
                {
                    var hlist = _dbConnect.DbHold.Where(c => c.Hsuid == us.Id && c.State == state && c.Isdelete == 0 && c.Isfc == 0).OrderByDescending(c => c.Qgdate).Select(c => new
                    {
                        c.Id,
                        c.Holdno,
                        c.Userid,
                        c.Username,
                        c.Usertel,
                        c.Jid,
                        c.Jimg,
                        c.Jname,
                        c.Jprice,
                        c.Rishouyi,
                        c.Zshouyi,
                        c.State,
                        Statename = ShopOrderUtils.Getstatename((int)c.State),
                        c.Buid,
                        c.Buserid,
                        c.Busertel,
                        c.Busername,
                        c.Zhimg,
                        c.Dkdate,
                        c.Skdate,
                        c.Issj,
                        c.Sjimg,
                        c.Sjjine, 
                        c.Qgdate
                    }).ToList();

                   

                    _res.Done(new { jinrinum, jinriprice, hlist }, "查询成功");
                }
                else
                {
                    var hlist = _dbConnect.DbHold.Where(c => c.Hsuid == us.Id && c.Isdelete == 0 && c.Isfc == 0).OrderByDescending(c => c.Qgdate).Select(c => new
                    {
                        c.Id,
                        c.Holdno,
                        c.Userid,
                        c.Username,
                        c.Usertel,
                        c.Jid,
                        c.Jimg,
                        c.Jname,
                        c.Jprice,
                        c.Rishouyi,
                        c.Zshouyi,
                        c.State,
                        Statename = ShopOrderUtils.Getstatename((int)c.State),
                        c.Buid,
                        c.Buserid,
                        c.Busertel,
                        c.Busername,
                        c.Zhimg,
                        c.Dkdate,
                        c.Skdate,
                        c.Issj,
                        c.Sjimg,
                        c.Sjjine,
                        c.Qgdate
                    }).ToList();
                    _res.Done(new { jinrinum, jinriprice, hlist }, "查询成功");
                }
            }
            catch (Exception ex)
            {
                _res.Error("查询异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///拆分
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result chaifen(JObject data)
        {

            try
            {
                
                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();
                int hid = Convert.ToInt32(data["hid"]);

                int num = Convert.ToInt32(data["num"]);

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                DbHold hold = _dbConnect.DbHold.FirstOrDefault(c => c.Id == hid && c.State == 0 && c.Isdelete == 0);
                if (hold == null) { return _res.Fail("信息错误"); }

              
                decimal zjine = hold.Jprice / num;
                decimal sjjine = hold.Sjjine / num;
                decimal zshouyi = hold.Zshouyi / num;
                for (int i = 0; i < num; i++)
                {
                    string Orderno = RandomUtils.GetRandom3();
                    DbHold newhold = new DbHold();
                    newhold.Holdno = Orderno;
                    newhold.Uid = hold.Uid;
                    newhold.Userid = hold.Userid;
                    newhold.Username = hold.Username;
                    newhold.Usertel = hold.Usertel;
                    newhold.Jid = hold.Jid;
                    newhold.Jname = hold.Jname;
                    newhold.Jimg = hold.Jimg;
                    newhold.Jprice = zjine;//交易价格
                    newhold.Rishouyi = 0;
                    newhold.Zshouyi = zshouyi;
                    newhold.Jsjbili = 0;
                    newhold.Jsybili = 0;
                    newhold.State = 0;//分红中
                    newhold.Hdate = DateTime.Now;
                    // newhold.Edate = DateTime.Now.AddHours(24);
                    newhold.Path = hold.Path + "," + hold.Id;
                    newhold.Repath = us.Repath;
                    newhold.Sjjine = sjjine;
                    newhold.Hsuid = hold.Hsuid;
                    newhold.Oldhsuid = hold.Oldhsuid;
                    newhold.Iscf = 1;
                    _dbConnect.DbHold.Add(newhold);

                   
                }
                hold.Isdelete = 1;
                _dbConnect.SaveChanges();
                _res.Done(null, "拆分成功");

            }
            catch (Exception ex)
            {
                _res.Error("拆分异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }



        /// <summary>
        ///字画管理正常字画：等待预约匹配:state=0
        ///lx = 0（正常） ,1(封存)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result ZhglList(JObject data)
        {

            try
            {

                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();
                int lx = Convert.ToInt32(data["lx"]);

              

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid) && c.Ulevel == 1);
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

               
                    var hlist = _dbConnect.DbHold.Where(c => c.Hsuid == us.Id && c.Isfc == lx && c.State == 0 && c.Isdelete == 0).OrderByDescending(c => c.Qgdate).Select(c => new
                    {
                        c.Id,
                        c.Holdno,
                        c.Userid,
                        c.Username,
                        c.Usertel,
                        c.Jid,
                        c.Jimg,
                        c.Jname,
                        c.Jprice,
                        c.Rishouyi,
                        c.Zshouyi,
                        c.State,
                        Statename = ShopOrderUtils.Getstatename((int)c.State),
                        c.Buid,
                        c.Buserid,
                        c.Busertel,
                        c.Busername,
                        c.Zhimg,
                        c.Dkdate,
                        c.Skdate,
                        c.Issj,
                        c.Sjimg,
                        c.Sjjine
                    }).ToList();

                _res.Done(hlist, "查询成功");



            }
            catch (Exception ex)
            {
                _res.Error("字画管理查询异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        ///画室统计
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result TjList(JObject data)
        {

            try
            {
                
                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();
                DateTime time = Convert.ToDateTime(data["time"]);             

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid) && c.Ulevel == 1);
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                var hlist = _dbConnect.DbHold.Where(c => c.Hsuid == us.Id && c.Isfc == 0 && c.Hdate.Date == time.Date &&  c.State == 0 && c.Isdelete == 0).OrderByDescending(c => c.Qgdate).Select(c => new
                {
                    c.Id,
                    c.Holdno,
                    c.Userid,
                    c.Username,
                    c.Usertel,
                    c.Jid,
                    c.Jimg,
                    c.Jname,
                    c.Jprice,
                    c.Rishouyi,
                    c.Zshouyi,
                    c.State,
                    Statename = ShopOrderUtils.Getstatename((int)c.State),
                    c.Buid,
                    c.Buserid,
                    c.Busertel,
                    c.Busername,
                    c.Zhimg,
                    c.Dkdate,
                    c.Skdate,
                    c.Issj,
                    c.Issd,
                    c.Sjimg,
                    c.Sjjine
                }).ToList();

               decimal jine =  hlist.Where(c=>c.Issd == 0).Sum(c => c.Jprice);
               decimal jine2 = hlist.Where(c => c.Issd == 1).Sum(c => c.Jprice);
               decimal yjjine = jine * (decimal)0.04;
                decimal zjine = jine2 + jine - yjjine;

                decimal count = hlist.Count();
               decimal sjjine = hlist.Sum(c => c.Sjjine);
                _res.Done(new { hlist,count,sjjine, zjine }, "查询成功");
              


            }
            catch (Exception ex)
            {
                _res.Error("封存异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///封存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Fengcun(JObject data)
        {

            try
            {
                int hid = Convert.ToInt32(data["hid"]);

                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();
               

                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                DbHold hold = _dbConnect.DbHold.FirstOrDefault(c => c.Id == hid && c.State == 0 && c.Isdelete == 0);
                if (hold == null) { _res.Fail("信息出错"); return _res; }
                hold.Isfc = 1;
                _dbConnect.SaveChanges();
                _res.Done(null, "封存成功");


            }
            catch (Exception ex)
            {
                _res.Error("封存异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///解封
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Jiefeng(JObject data)
        {

            try
            {
                int hid = Convert.ToInt32(data["hid"]);

                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();


                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                DbHold hold = _dbConnect.DbHold.FirstOrDefault(c => c.Id == hid && c.State == 0 && c.Isdelete == 0);
                if (hold == null) { _res.Fail("信息出错"); return _res; }
                hold.Isfc = 0;
                _dbConnect.SaveChanges();
                _res.Done(null, "解封成功");


            }
            catch (Exception ex)
            {
                _res.Error("解封异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }



        /// <summary>
        ///预约记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result YList(JObject data)
        {

            try
            {

                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();
                int lx = Convert.ToInt32(data["lx"]);

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                if(lx == 1)
                {
                    var hlist = _dbConnect.DbHold.Where(c => c.Buid == us.Id && c.Isfc == 0 && c.Isyy == 1 && c.Isdelete == 0).OrderByDescending(c => c.Qgdate).Select(c => new
                    {
                        c.Id,
                        c.Holdno,
                        c.Userid,
                        c.Username,
                        c.Usertel,
                        c.Rishouyi,
                        c.Zshouyi,
                        c.State,
                        c.Buid,
                        c.Buserid,
                        c.Busertel,
                        c.Busername,
                        c.Qgdate,
                        minprice = _dbConnect.DbPricerange.FirstOrDefault(p => p.Id == c.Pid).Minprice,
                        maxprice = _dbConnect.DbPricerange.FirstOrDefault(p => p.Id == c.Pid).Maxprice,
                        hsname = _dbConnect.DbUsers.FirstOrDefault(u => u.Id == c.Hsuid).StudioName

                    }).ToList();
                    _res.Done(hlist, "查询成功");
                }else if(lx == 2)
                {
                    var hlist = _dbConnect.DbHold.Where(c => c.Hsuid == us.Id && c.Isfc == 0 && c.Isyy == 1 && c.Isdelete == 0).OrderByDescending(c => c.Qgdate).Select(c => new
                    {
                        c.Id,
                        c.Holdno,
                        c.Userid,
                        c.Username,
                        c.Usertel,
                        c.Rishouyi,
                        c.Zshouyi,
                        c.State,
                        c.Buid,
                        c.Buserid,
                        c.Busertel,
                        c.Busername,
                        c.Qgdate,
                        minprice = _dbConnect.DbPricerange.FirstOrDefault(p => p.Id == c.Pid).Minprice,
                        maxprice = _dbConnect.DbPricerange.FirstOrDefault(p => p.Id == c.Pid).Maxprice,
                        hsname = _dbConnect.DbUsers.FirstOrDefault(u => u.Id == c.Hsuid).StudioName

                    }).ToList();
                    _res.Done(hlist, "查询成功");
                }
                



            }
            catch (Exception ex)
            {
                _res.Error("查询预约异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        ///重拍
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result chongpai(JObject data)
        {

            try
            {
                int hid = Convert.ToInt32(data["hid"]);

                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();


                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                DbHold hold = _dbConnect.DbHold.FirstOrDefault(c => c.Id == hid && c.State == 1 && c.Isdelete == 0);
                if (hold == null) { _res.Fail("信息出错"); return _res; }

                hold.State = 0;
                hold.Buid = null;
                hold.Buserid = null;
                hold.Busername = null;
                hold.Busertel = null;
                hold.Isyy = 0;
                hold.Pid = 0;
                _dbConnect.SaveChanges();
                _res.Done(null, "操作成功");


            }
            catch (Exception ex)
            {
                _res.Error("重拍异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        ///画室长帮助卖家收款
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result HsShoukuan(JObject data)
        {

            try
            {
                int hid = Convert.ToInt32(data["hid"]);

                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();


                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                DbHold hold = _dbConnect.DbHold.FirstOrDefault(c => c.Id == hid && c.State == 2 && c.Isdelete == 0);
                if (hold == null) { _res.Fail("信息出错"); return _res; }

                hold.State = 3;
                hold.Skdate = DateTime.Now;
                _dbConnect.SaveChanges();
                _res.Done(null, "确认成功");


            }
            catch (Exception ex)
            {
                _res.Error("确认异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///查看详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {

            try
            {
                int hid = Convert.ToInt32(data["hid"]);

                int uid = Convert.ToInt32(data["uid"]);
                string userid = data["userid"].ToString();

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                var hlist = _dbConnect.DbHold.Where(h =>h.Id == hid  && h.Isdelete == 0 && h.Isfc == 0).Select(c => new
                {
                    c.Id,
                    c.Holdno,
                    c.Uid,
                    c.Userid,
                    c.Username,
                    c.Usertel,
                    c.Jid,
                    c.Jimg,
                    c.Jname,
                    c.Jprice,
                    c.Rishouyi,
                    c.Zshouyi,
                    c.State,
                    Statename = ShopOrderUtils.Getstatename((int)c.State),
                    c.Buid,
                    c.Buserid,
                    c.Busertel,
                    c.Busername,
                    c.Qgdate,
                    c.Dkdate,
                    c.Skdate,
                    c.Issj,
                    c.Sjjine,
                    banklist = _dbConnect.DbUsersBank.Where(u => u.Uid == c.Uid).ToList(),
                    hsbanklist = _dbConnect.DbUsersBank.Where(u => u.Uid == c.Hsuid).ToList()
                }).ToList();

                _res.Done(hlist, "查询成功");


            }
            catch (Exception ex)
            {
                _res.Error("查询详情异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        ///// <summary>
        /////买方申诉
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[TokenCheckFilters]
        //[SignCheckFilters]
        //public Result BuyShenshu(JObject data)
        //{

        //    try
        //    {
        //        int hid = Convert.ToInt32(data["hid"]);

        //        int uid = Convert.ToInt32(data["uid"]);
        //        string userid = data["userid"].ToString();
        //        string bbz = data["bbz"].ToString();

        //        if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

        //        DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid));
        //        if (us == null) { _res.Fail("用户信息出错"); return _res; }

        //        DbHold hold = _dbConnect.DbHold.FirstOrDefault(c => c.Buid == uid && c.Id == hid && c.Isdelete == 0);
        //        if (hold == null) { _res.Fail("交易信息出错"); return _res; }
        //        if (hold.Zhimg == null) { _res.Fail("您还未上传支付凭证，不可申诉"); return _res; }


        //        hold.Buyissu = 1;
        //        hold.Bbz = bbz;

        //        _dbConnect.SaveChanges();
        //        _res.Done(null, "申诉成功");


        //    }
        //    catch (Exception ex)
        //    {
        //        _res.Error("申诉异常");

        //        NLogHelper._.Error(_res.Msg, ex);
        //    }
        //    return _res;
        //}



    }
}

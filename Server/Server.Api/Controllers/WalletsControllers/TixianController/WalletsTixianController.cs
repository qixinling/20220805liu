using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Crypto_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Bill.Utils;
using Server.Wallet.Utils;
using Server.Api.Method;
using Microsoft.EntityFrameworkCore;
using Server.Utils.Msg_Utils;
using Server.Utils.Http_Utils;

namespace Server.Api.Controllers.WalletsControllers.TixianController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsTixianController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsTixianController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询可提现货币
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result TixianSelect(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                WalletsTixianSelectMethod wtsm = new WalletsTixianSelectMethod(_dbConnect);
                var tlist = wtsm.GetList().Select(t => new
                {
                    id = t.Id,
                    bid = t.Cid,
                    bname = t.Coinname,
                    codename = t.Codename,
                    jijnemin = t.Jinemin,
                    jinemax = t.Jinemax,
                    jinebs = t.Jinebs,
                    shouxu = t.Shouxu,
                    jine = _dbConnect.DbWallets.FirstOrDefault(u => u.Userid.Equals(userid) && u.Cid == t.Cid).Jine
                });
                _res.Done(tlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询可提现货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 提现
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Tixian(JObject data)
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
                string password2 = Convert.ToString(data["password2"]);
                //string suserid = Convert.ToString(data["suserid"]);

                using var transaction = _dbConnect.Database.BeginTransaction();

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers user = um.GetById(uid);
                if (user == null) { _res.Fail("用户信息错误"); return _res; }


                DbWalletsCoin coin = _dbConnect.DbWalletsCoin.FirstOrDefault(c => c.Id == cid && c.State == 1);

                WalletsTixianSelectMethod wtsm = new WalletsTixianSelectMethod(_dbConnect);
                DbWalletsTixianSelect t = wtsm.GetByCid(cid);
                if (coin == null || t == null) { _res.Fail("货币不存在"); return _res; }


                DbWallets uw = _dbConnect.DbWallets.FirstOrDefault(u => u.Uid == user.Id && u.Cid == cid);
                if (uw == null) { _res.Fail("钱包信息错误"); return _res; }

                //if (string.IsNullOrWhiteSpace(password2)) { _res.Fail("支付密码不能为空"); return _res; }
                //string Password2md5 = MD5Utils.MD5Encrypt(password2, 32);
                //if (!user.Password2.Equals(Password2md5)) { _res.Fail("支付密码错误"); return _res; }


                DbUsersBank ub = _dbConnect.DbUsersBank.Where(b => b.Id == bid && b.Uid == uw.Uid).FirstOrDefault();
                if (ub == null) { _res.Fail("收款账户信息错误"); return _res; }

                if (jine < t.Jinemin || jine > t.Jinemax) { _res.Fail("请输入" + t.Jinemin + " - " + t.Jinemax + "之间的金额"); return _res; }
                if (jine % t.Jinebs != 0) { _res.Fail("请输入" + t.Jinebs + "的倍数"); return _res; }
                int Count = _dbConnect.DbWalletsTixian.Where(t => t.Uid == user.Id && t.Ispay == 0 && t.Isdelete == 0).Count();
                if (Count > 0) { _res.Fail("您有提现申请正在审核中,请勿重复提交"); return _res; }

                decimal Shouxu = jine * t.Shouxu / 100;
                if (uw.Jine < jine + Shouxu) { _res.Fail(t.Coinname + "余额不足"); return _res; }
                decimal Zjine = jine + Shouxu;

                DbWalletsTixian tx = new DbWalletsTixian
                {
                    Uid = user.Id,
                    Userid = user.Userid,
                    Username = user.Username,
                    Usertel = user.Usertel,
                    Cid = coin.Id,
                    Codename = coin.Codename,
                    Coinname = coin.Coinname,
                    Jine = jine,
                    Shouxu = Shouxu,
                    Lx = 0,
                    Bankname = ub.Bankname,
                    Bankcard = ub.Bankcard,
                    Bankaddress = ub.Bankaddress,
                    Bankimg = ub.Bankimg,
                    Beizhu = beizhu,
                    Tdate = DateTime.Now,
                    Fdate = DateTime.Now,
                    Ispay = 0,
                    Isdelete = 0
                };
                _dbConnect.DbWalletsTixian.Add(tx);

                Result res = WalletsUtils.PayBalance(uw.Uid, uw.Cid, Zjine, _dbConnect);
                if (res.Code == 0) { return res; }

                //创建账单
                IBill bill = new BillTiXian();
                bill.Create(uw.Uid, new Dictionary<int, decimal>
                    {
                        {uw.Cid,Zjine}
                    }, _dbConnect);

                _dbConnect.SaveChanges();
                transaction.Commit();

                _res.Done(null, "申请提现成功,请等待审核");
            }
            catch (Exception ex)
            {
                _res.Error("提现异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }



        /// <summary>
        /// 查询提现记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result TixianGet(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                int uid = Convert.ToInt32(data["uid"]);
                string[] ispaynameArr = new[] { "待审核", "已审核", "已撤销" };
                WalletsTixianMethod wtm = new WalletsTixianMethod(_dbConnect);
                var tlist = wtm.GetList().Where(c => c.Uid == uid && c.Isdelete == 0).OrderByDescending(c => c.Id).Select(c => new
                {
                    id = c.Id,
                    uid = c.Uid,
                    userid = c.Userid,
                    username = c.Username,
                    cid = c.Cid,
                    codename = c.Codename,
                    coinname = c.Coinname,
                    jine = c.Jine,
                    shouxu = c.Shouxu,
                    bankname = c.Bankname,
                    bankcard = c.Bankcard,
                    bankaddress = c.Bankaddress,
                    usertel = c.Usertel,
                    beizhu = c.Beizhu,
                    tdate = c.Tdate,
                    fdate = c.Fdate,
                    ispay = c.Ispay,
                    ispayname = ispaynameArr[c.Ispay],
                }).ToList();
                _res.Done(tlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询提现记录异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询画室长提现记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result TxList(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                int uid = Convert.ToInt32(data["uid"]);
                int state = Convert.ToInt32(data["state"]);
                string[] ispaynameArr = new[] { "待审核", "已审核", "已撤销" };

                WalletsTixianMethod wtm = new WalletsTixianMethod(_dbConnect);
                var tlist = _dbConnect.DbWalletsTixian.Include(c=>c.UidNavigation).Where(c => c.UidNavigation.Mystudioid == uid && c.Ispay == state && c.Isdelete == 0).OrderByDescending(c => c.Id).Select(c => new
                {
                    id = c.Id,
                    uid = c.Uid,
                    userid = c.Userid,
                    username = c.Username,
                    cid = c.Cid,
                    codename = c.Codename,
                    coinname = c.Coinname,
                    jine = c.Jine,
                    shouxu = c.Shouxu,
                    bankname = c.Bankname,
                    bankcard = c.Bankcard,
                    bankaddress = c.Bankaddress,
                    bankimg = c.Bankimg,
                    usertel = c.Usertel,
                    beizhu = c.Beizhu,
                    tdate = c.Tdate,
                    fdate = c.Fdate,
                    ispay = c.Ispay,
                    ispayname = ispaynameArr[c.Ispay],
                }).ToList();
                _res.Done(tlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询提现记录异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 画室长提现
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result TxPass(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                int uid = Convert.ToInt32(data["uid"]);
                int tid = Convert.ToInt32(data["tid"]);

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c=>c.Id == uid);
                if(us == null) { return _res.Fail("用户信息错误"); }

                DbWalletsTixian tx = _dbConnect.DbWalletsTixian.FirstOrDefault(c=>c.Id == tid);
                if(tx == null) { return _res.Fail("提现信息错误"); }
                if(tx.Ispay == 1) { return _res.Fail("请勿重复审核"); }

                tx.Fdate = DateTime.Now;
                tx.Ispay = 1;
                DbWallets uw = _dbConnect.DbWallets.FirstOrDefault(c=>c.Uid == uid && c.Cid == tx.Cid);
                Result res = WalletsUtils.UpdateBalance(uw.Uid, uw.Cid, tx.Jine, _dbConnect);
                if (res.Code == 0) { return res; }

                //创建账单
                IBill bill = new BillTiXian();
                bill.Create(uw.Uid, new Dictionary<int, decimal>
                    {
                        {uw.Cid,tx.Jine}
                    }, _dbConnect,"通过"+tx.Userid+"提现");

                _dbConnect.SaveChanges();

                MsgUtils.Send(0, "提现", "您的提现申请:" + tx.Jine + "已通过", 0, "系统消息", tx.Uid, tx.Userid);
                SystemLogMethod.Add(userid, HttpInfoUtils.GetIP(), 3, "审核提现:" + tx.Userid);

                _res.Done(null, "审核成功");
            }
            catch (Exception ex)
            {
                _res.Error("提现审核异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

    }
}

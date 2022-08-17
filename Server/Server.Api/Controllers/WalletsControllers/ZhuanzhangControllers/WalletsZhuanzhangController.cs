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
using Server.Wallet.Utils;
using Server.Bill.Utils;
using Server.Api.Method;
using Microsoft.EntityFrameworkCore;

namespace Server.Api.Controllers.WalletsControllers.ZhuanzhangControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsZhuanzhangController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsZhuanzhangController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询可转账货币
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result ZhuanzhangSelect(JObject data)
        {
            try
            {
                string userid = Convert.ToString(data["userid"]);
                WalletsZhuanzhangSelectMethod wzsm = new WalletsZhuanzhangSelectMethod(_dbConnect);
                var zlist = wzsm.GetList().Select(c => new
                {
                    c.Id,
                    bid = c.Cid,
                    c.Coinname,
                    c.Codename,
                    c.Jinemin,
                    c.Jinemax,
                    c.Jinebs,
                    c.Shouxu,
                    jine = _dbConnect.DbWallets.FirstOrDefault(u => u.Userid.Equals(userid) && u.Cid == c.Cid).Jine
                }) ;
                _res.Done(zlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询可转账货币异常");


                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 转账
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Zhuanzhang(JObject data)
        {
            try
            {
                string userid = Convert.ToString(data["userid"]);
                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                int uid = Convert.ToInt32(data["uid"]);
                int cid = Convert.ToInt32(data["cid"]);
                decimal jine = Convert.ToDecimal(data["jine"]);
                string beizhu = Convert.ToString(data["beizhu"]);
                string password2 = Convert.ToString(data["password2"]);
                string susertel = Convert.ToString(data["suserid"]);

                using var transaction = _dbConnect.Database.BeginTransaction();

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers user = um.GetById(uid);
                if (user == null) { _res.Fail("用户信息错误"); return _res; }

                DbUsers suser = _dbConnect.DbUsers.FirstOrDefault(c => c.Usertel.Equals(susertel));
                if (suser == null) { _res.Fail("收款人不存在"); return _res; }

                
                DbWalletsCoin coin = _dbConnect.DbWalletsCoin.FirstOrDefault(c => c.Id == cid && c.State == 1);

                WalletsZhuanzhangSelectMethod wzsm = new WalletsZhuanzhangSelectMethod(_dbConnect);
                DbWalletsZhuanzhangSelect z = wzsm.GetByCid(cid);
                if (coin == null || z == null) { _res.Fail("货币不存在"); return _res; }

               // if (user.Zzxz == 1)
                //{
                    if (z.State == 1)//user.Repath.Contains(sid) == false 
                    {
                        string zid = "," + user.Id + ",";
                        string sid = "," + suser.Id + ",";
                       if (suser.Repath.Contains(zid) == false) { _res.Fail("该货币只能转给下级团队人员"); return _res; }
                    }
               // }

                WalletsMethod wm = new WalletsMethod(_dbConnect);
                DbWallets uw = wm.GetList().FirstOrDefault(u => u.Uid == user.Id && u.Cid == cid);
                if (uw == null) { _res.Fail("转款人钱包错误"); return _res; }


                if (string.IsNullOrWhiteSpace(password2)) { _res.Fail("支付密码不能为空"); return _res; }
                string Password2md5 = MD5Utils.MD5Encrypt(password2, 32);
                if (!user.Password2.Equals(Password2md5)) { _res.Fail("支付密码错误"); return _res; }

                if (jine < z.Jinemin || jine > z.Jinemax) { _res.Fail("请输入" + z.Jinemin + "-" + z.Jinemax + "之间的金额"); return _res; }
                if (jine % z.Jinebs != 0) { _res.Fail("请输入" + z.Jinebs + "的倍数"); return _res; }
                decimal Shouxu = jine * z.Shouxu / 100;
                if (uw.Jine < jine + Shouxu) { _res.Fail(z.Coinname + "余额不足"); return _res; }

                DbWallets suw = wm.GetList().FirstOrDefault(u => u.Uid == suser.Id && u.Cid == cid);
                if (suw == null) { _res.Fail("收款人钱包错误"); return _res; }

                decimal Zzhjine = uw.Jine - (jine + Shouxu);//转账人转账后金额
                decimal Szhjine = suw.Jine + jine;//收款人收款后金额

                decimal Zzjind = jine + Shouxu;

                DbWalletsZhuanzhang zz = new DbWalletsZhuanzhang
                {
                    Uid = user.Id,
                    Userid = user.Userid,
                    Username = user.Username,
                    Cid = coin.Id,
                    Codename = coin.Codename,
                    Coinname = coin.Coinname,
                    Zqjine = uw.Jine,
                    Zhjine = Zzhjine,
                    Jine = jine,
                    Lx = 0,
                    Sid = suw.Uid,
                    Suserid = suw.Userid,
                    Susername = suser.Username,
                    Szqjine = suw.Jine,
                    Szhjine = Szhjine,
                    Beizhu = beizhu,
                    Zdate = DateTime.Now,
                    Isdelete = 0
                };

                Result res = WalletsUtils.PayBalance(uw.Uid, uw.Cid, Zzjind, _dbConnect);
                if (res.Code == 0) { return res; }

                res = WalletsUtils.UpdateBalance(suw.Uid, suw.Cid, jine, _dbConnect);
                if (res.Code == 0) { return res; }

                //创建账单
                IBill bill = new BillZhuanZhang();
                bill.Create(uw.Uid, new Dictionary<int, decimal>
                    {
                        {uw.Cid,0-Zzjind},
                    }, _dbConnect);

                bill.Create(suw.Uid, new Dictionary<int, decimal>
                    {
                        {suw.Cid,jine}
                    }, _dbConnect);

                _dbConnect.DbWalletsZhuanzhang.Add(zz);

                if (cid == 2)
                {
                    decimal kou = 200;
                    DbWallets u = _dbConnect.DbWallets.Include(c => c.UidNavigation).FirstOrDefault(c => c.Uid == suw.Uid && c.Cid == cid);
                    if (u.UidNavigation.Djxyz < kou)
                    {
                        decimal cha = kou - u.UidNavigation.Djxyz;
                        if (u.Jine >= cha)
                        {
                            u.Jine -= cha;
                            u.UidNavigation.Djxyz += cha;
                        }
                    }
                }

                _dbConnect.SaveChanges();
                transaction.Commit();

                _res.Done(null, "转账成功");
            }
            catch (Exception ex)
            {
                _res.Error("转账异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }



        /// <summary>
        /// 查询转账记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result ZhuanzhangGet(JObject data)
        {

            try
            {
                int uid = Convert.ToInt32(data["uid"]);
                List<DbWalletsZhuanzhang> zlist = _dbConnect.DbWalletsZhuanzhang.Where(c => c.Uid == uid && c.Isdelete == 0).ToList();
                _res.Done(zlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询转账记录异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

    }
}

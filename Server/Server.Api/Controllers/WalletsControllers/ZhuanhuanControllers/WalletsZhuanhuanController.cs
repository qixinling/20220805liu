using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Wallet.Utils;
using Server.Bill.Utils;
using Server.Api.Method;
using Server.Bonus.Utils;

namespace Server.Api.Controllers.WalletsControllers.ZhuanhuanControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsZhuanhuanController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsZhuanhuanController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询可转换货币
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result ZhuanhuanSelect(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                WalletsZhuanhuanSelectMethod wzsm = new WalletsZhuanhuanSelectMethod(_dbConnect);
                var zlist = wzsm.GetList().Select(z => new
                {
                    id=z.Id,
                    bid1=z.Cid1,
                    bname1=z.Coinname1,
                    codename1=z.Codename1,
                    bid2=z.Cid2,
                    bname2=z.Coinname2,
                    codename2=z.Codename2,
                    jijnemin=z.Jinemin,
                    jinemax=z.Jinemax,
                    jinebs=z.Jinebs,
                    bili=z.Bili,
                    jine1= _dbConnect.DbWallets.FirstOrDefault(u => u.Userid.Equals(userid) && u.Cid == z.Cid1).Jine,
                    jine2 = _dbConnect.DbWallets.FirstOrDefault(u => u.Userid.Equals(userid) && u.Cid == z.Cid2).Jine
                });
                _res.Done(zlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询可转换货币异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 转换
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Zhuanhuan(JObject data)
        {
            try
            {
                string userid = Convert.ToString(data["userid"]);
                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                int uid = Convert.ToInt32(data["uid"]);
                int zhid = Convert.ToInt32(data["zhid"]);
                decimal jine = Convert.ToDecimal(data["jine"]);

                using var transaction = _dbConnect.Database.BeginTransaction();

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers user = um.GetById(uid);
                if (user == null) { _res.Fail("用户信息错误"); return _res; }

                WalletsZhuanhuanSelectMethod wzsm = new WalletsZhuanhuanSelectMethod(_dbConnect);
                DbWalletsZhuanhuanSelect z = wzsm.GetById(zhid);
                if (z == null) { _res.Fail("货币不存在"); return _res; }

                WalletsCoinMethod wcm = new WalletsCoinMethod(_dbConnect);
                DbWalletsCoin coin = wcm.GetList().FirstOrDefault(c => c.Id == z.Cid1 && c.State == 1);

                DbWalletsCoin coin2 = wcm.GetById(z.Cid2);
                if (coin == null || coin2 == null) { _res.Fail("货币不存在"); return _res; }

                WalletsMethod wm = new WalletsMethod(_dbConnect);
                // 转款人
                DbWallets uw = wm.GetList().FirstOrDefault(u => u.Uid == user.Id && u.Cid == coin.Id);
                if (uw == null) { _res.Fail("钱包信息错误"); return _res; }
                // 收款人
                DbWallets uw2 = wm.GetList().FirstOrDefault(u => u.Uid == user.Id && u.Cid == coin2.Id);
                if (uw2 == null) { _res.Fail("钱包信息错误"); return _res; }

                if (jine < z.Jinemin || jine > z.Jinemax) { _res.Fail("请输入" + z.Jinemin + "-" + z.Jinemax + "之间的金额"); return _res; }

                if (jine % z.Jinebs != 0) { _res.Fail("请输入" + z.Jinebs + "的倍数"); return _res; }
                if (uw.Jine < jine) { _res.Fail(z.Coinname1 + "余额不足"); return _res; }

                decimal Zzjine = jine * z.Bili / 100;
                string Bz = coin.Coinname + " 转 " + coin2.Coinname;

                DbWalletsZhuanhuan zh = new DbWalletsZhuanhuan
                {
                    Uid = user.Id,
                    Userid = user.Userid,
                    Username = user.Username,
                    Cid1 = coin.Id,
                    Codename1 = coin.Codename,
                    Coinname1 = coin.Coinname,
                    Cid2 = coin2.Id,
                    Codename2 = coin2.Codename,
                    Coinname2 = coin2.Coinname,
                    Jine = jine,
                    Lx = 0,
                    Zdate = DateTime.Now,
                    Isdelete = 0,
                    Beizhu = Bz
                };
                _dbConnect.DbWalletsZhuanhuan.Add(zh);

                Result res = WalletsUtils.PayBalance(uw.Uid, uw.Cid, jine, _dbConnect);
                if (res.Code == 0) { return res; }

                res = WalletsUtils.UpdateBalance(uw2.Uid, uw2.Cid, Zzjine, _dbConnect);
                if (res.Code == 0) { return res; }

                //创建账单
                IBill bill = new BillZhuanHuan();
                bill.Create(uw.Uid, new Dictionary<int, decimal>
                {
                    {uw.Cid,0-jine},
                    {uw2.Cid,Zzjine}
                }, _dbConnect);

                _dbConnect.SaveChanges();
                if(z.Cid1 == 1 && z.Cid2 == 2)
                {
                    YejiUtils ym = new YejiUtils();
                    ym.AddZhuanhuan(jine);
                }
               

                transaction.Commit();

                _res.Done(null, "转换成功");

            }
            catch (Exception ex)
            {
                _res.Error("转换异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }




    }
}

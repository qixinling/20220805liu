using Server.Bonus.Utils;
using Server.Logs;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Linq;

namespace Server.Wallet.Utils
{
    public static class WalletsUtils
    {

        /// <summary>
        /// 钱包余额支付
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="cid">币种id</param>
        /// <param name="amount">支付金额,请输入正数</param>
        /// <returns></returns>
        public static Result PayBalance(int uid, int cid, decimal amount, DbConnect dbConnect)
        {
            Result res = new Result();
            try
            {
                if (amount == 0) { return res.Done(null, "无修改"); }
                if (amount < 0) { throw new ArgumentNullException("支付金额请输入正数"); }
                DbWallets wallet = dbConnect.DbWallets.FirstOrDefault(w => w.Uid == uid && w.Cid == cid);
                if (wallet == null) { return res.Fail("钱包" + cid + "不存在"); }
                if (wallet.Jine < amount) { return res.Fail(wallet.CnameZh + "余额不足"); }
                wallet.Jine -= amount;
                dbConnect.SaveChanges();
                res.Done(wallet, "支付成功");

                YejiUtils ym = new YejiUtils();
                ym.AddXyzZonge(0);
            }
            catch (Exception ex)
            {
                res.Error("修改" + cid + "余额异常");
                NLogHelper._.Error(res.Msg, ex);
            }
            return res;
        }

        /// <summary>
        /// 修改钱包余额
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="cid">币种id</param>
        /// <param name="amount">修改金额,增加正数,减少负数</param>
        /// <returns></returns>
        public static Result UpdateBalance(int uid, int cid, decimal amount, DbConnect dbConnect)
        {
            Result res = new Result();
            try
            {
                if (amount == 0) { return res.Done(null, "无修改"); }
                DbWallets wallet = dbConnect.DbWallets.FirstOrDefault(w => w.Uid == uid && w.Cid == cid);
                if (wallet == null) { return res.Fail("钱包"+ cid + "不存在"); }
                wallet.Jine += amount;
                dbConnect.SaveChanges();
                res.Done(wallet, "修改成功");
            }
            catch (Exception ex)
            {
                res.Error("修改" + cid + "余额异常");
                NLogHelper._.Error(res.Msg, ex);
            }
            return res;
        }
    }
}

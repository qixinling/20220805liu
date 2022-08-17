using Server.Logs;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Bill.Utils
{
    public class BillTiXian : IBill
    {
        public int BillLx => 2;
        public string BillName => "提现";
        public Result Create(int uid, Dictionary<int, decimal> cidAmount, DbConnect dbConnect, string bz = null, int state = 1)
        {
            Result res = new Result();
            try
            {
                if (cidAmount.Count == 0) { throw new ArgumentNullException("未指定账单金额"); }

                DbBill dbBill = new DbBill
                {
                    Uid = uid,
                    Blx = BillLx,
                    Bname = BillName,
                    Bdate = DateTime.Now,
                    Bz = bz,
                    State = state
                };
                List<DbWalletsCoin> walletsCoin = dbConnect.DbWalletsCoin.ToList();
                foreach (KeyValuePair<int, decimal> obj in cidAmount)
                {
                    dbBill.DbBillAmount.Add(new DbBillAmount
                    {
                        Bid = dbBill.Id,
                        Cid = obj.Key,
                        Cname = walletsCoin.FirstOrDefault(c => c.Id == obj.Key).Coinname,
                        Amount = obj.Value
                    });
                }

                dbConnect.DbBill.Add(dbBill);

                dbConnect.SaveChanges();

                res.Done(dbBill, "");
            }
            catch (Exception ex)
            {
                res.Error("创建" + BillName + "账单异常");
                NLogHelper._.Error(res.Msg, ex);
            }
            return res;
        }
    }
}

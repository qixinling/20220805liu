using Newtonsoft.Json;
using Server.Api.Method;
using Server.Logs;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Utils
{
    public static class ShopOrderUtils
    {

        /// <summary>
        /// 收款
        /// </summary>
        /// <returns></returns>
        public static Result shoukuan(DbHold hold, DbConnect dbConnect)
        {
            Result res = new Result();
            try
            {
                
                   
               
                dbConnect.SaveChanges();
                res.Done(null, "收款成功");
            }
            catch (Exception ex)
            {
                res.Error("收款" + hold.Id + "异常");
                NLogHelper._.Error(res.Msg, ex);
            }
            return res;
        }

        public static string Getordername(int Ispay)
        {
            string Ordername = "";
            if (Ispay == 1)
            {
                Ordername = "待付款";
            }
            else if (Ispay == 2)
            {
                Ordername = "待发货";
            }
            else if (Ispay == 3)
            {
                Ordername = "待收货";
            }
            else if (Ispay == 4)
            {
                Ordername = "已收货";
            }
            else if (Ispay == 5)
            {
                Ordername = "已退款";
            }
            return Ordername;
        }

        public static string Getstatename(int Ispay)
        {
            string Ordername = "";
            if (Ispay == 0)
            {
                Ordername = "待匹配";
            }
            if (Ispay == 1)
            {
                Ordername = "待付款";
            }
            else if (Ispay == 2)
            {
                Ordername = "待收款";
            }
            else if (Ispay == 3)
            {
                Ordername = "待上架";
            }
            else if (Ispay == 4)
            {
                Ordername = "已完成";
            }
            
            return Ordername;
        }
    }
}

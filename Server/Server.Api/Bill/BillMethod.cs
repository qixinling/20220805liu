using Server.Logs;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Bill.Utils
{
    public static class BillMethod
    {
        public static List<IBill> BillList
        {
            get => new List<IBill> {
                { new BillBonus() }, //0奖金
                { new BillChongZhi() },  //1充值
                { new BillTiXian() },    //2提现
                { new BillZhuanZhang() },    //3转账
                { new BillZhuanHuan() }, //4转换
                { new BillZengJian() },  //5增减
                { new BillPay() }    //6消费,除上面6条外,其他任意支出统一调用消费生成账单,通过备足bz区分用于什么消费,如:购物,投资等
            };
        }
    }
}

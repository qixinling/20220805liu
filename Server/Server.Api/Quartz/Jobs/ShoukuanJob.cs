using Quartz;
using Server.Utils.Configuration_Utils;
using System;
using System.IO;
using System.Threading.Tasks;
using Server.Logs;
using Server.Models;
using Server.Models.DataBaseModels;
using System.Collections.Generic;
using System.Linq;
using Org.BouncyCastle.Asn1.Cms;
using System.Globalization;
using Server.Api.Method;
using Newtonsoft.Json;
using Server.Api.Utils;
using Microsoft.EntityFrameworkCore;

namespace Server.Quartz.Jobs
{
    public class ShoukuanJob : IJob
    {
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public async Task Execute(IJobExecutionContext context)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            try
            {

                using DbConnect dbConnect = DbConnectUtils.GetDbContext();
               
                List<DbHold> hlist = dbConnect.DbHold.Where(u => u.Dkdate != null && u.Sellissu == 0 && u.State == 1 && u.Isdelete == 0 ).ToList();
                Dictionary<string, decimal> bonusDic = SystemSettingBonusUtils.GetBonusParameter(dbConnect);
                decimal bs15 = bonusDic["bs15"];
                decimal koujine = bonusDic["bs16"];

                foreach (DbHold hold in hlist)
                {
                    TimeSpan hous = DateTime.Now - Convert.ToDateTime(hold.Dkdate);
                    if (hous.Hours >= (int)bs15)
                    {
                        DbWallets uw = dbConnect.DbWallets.Include(c=>c.UidNavigation).FirstOrDefault(c => c.Uid == hold.Uid && c.Cid == 2);
                        if (uw == null) { NLogHelper._.Info("自动收款钱包出错"); continue; }
                        
                        uw.Jine -= koujine;

                        DbBill dbBill = new DbBill
                        {
                            Uid = hold.Uid,
                            Blx = 6,
                            Bname = "消费",
                            Bdate = DateTime.Now,
                            Bz = $"不按时收款{hold.Id}",
                            State = 1
                        };
                        dbBill.DbBillAmount.Add(new DbBillAmount
                        {
                            Bid = dbBill.Id,
                            Cid = uw.Cid,
                            Cname = uw.CnameZh,
                            Amount = 0 - koujine
                        });
                        dbConnect.DbBill.Add(dbBill);

                        Result res = ShopOrderUtils.shoukuan(hold, dbConnect);
                        if(res.Code == 100)
                        {
                            uw.UidNavigation.Djjine += hold.Rishouyi;
                        }
                        
                        //if (hold.Isqx == 0)
                        //{
                        //    hold.State = 3;
                        //    hold.Skdate = DateTime.Now;
                        //    hold.Edate = DateTime.Now.AddHours(24);
                        //}
                        //else if (hold.Isqx == 1)
                        //{
                        //    hold.State = 4;
                        //    hold.Skdate = DateTime.Now;

                        //    DbUsers bus = dbConnect.DbUsers.FirstOrDefault(c => c.Id == hold.Buid);
                        //    bus.Djjine -= 5000;

                        //    DbShopOrder or = new DbShopOrder();
                        //    string Orderno = RandomUtils.OrderNo("");
                        //    or.OrderNo = Orderno;
                        //    or.Uid = (int)hold.Buid;
                        //    or.Userid = hold.Buserid;
                        //    List<DbShopOrderChild> Goodslist = new List<DbShopOrderChild>();
                        //    //子订单
                        //    DbShopOrderChild oc = new DbShopOrderChild
                        //    {
                        //        OrderNo = Orderno,
                        //        Oid = or.Id,
                        //        Gid = hold.Jid,
                        //        Goodsname = hold.Jname,
                        //        Uid = (int)hold.Buid,
                        //        Userid = hold.Buserid,
                        //        Num = 1,
                        //        Sjine = hold.Jprice,
                        //        Yjine = hold.Jprice
                        //    };
                        //    Goodslist.Add(oc);

                        //    oc.Goodsimg = hold.Jimg;
                        //    oc.Cdate = DateTime.Now;

                        //    or.DbShopOrderChild.Add(oc);

                        //    or.Goodslist = JsonConvert.SerializeObject(Goodslist);
                        //    or.Odate = DateTime.Now;
                        //    or.Cost = 0;
                        //    or.Goodsnum = 1;
                        //    or.Kuaidiname = "-";
                        //    or.KuaidiNo = "-";
                        //    or.Sjine = hold.Jprice;
                        //    or.Yjine = hold.Jprice;
                        //    or.Beizhu = "-";
                        //    or.Orderstate = 1;
                        //    or.Isdelete = 0;
                        //    or.Lx = 2;
                        //    dbConnect.DbShopOrder.Add(or);
                        //}
                    }
                }              
                dbConnect.SaveChanges();
                

            }
            catch (Exception ex)
            {
                NLogHelper._.Error("自动收款出错", ex);
            }
        }
    }
}

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

namespace Server.Quartz.Jobs
{
    public class PepeiJob : IJob
    {
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public async Task Execute(IJobExecutionContext context)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            try
            {
                //晚上剩余的单自动和公司匹配
                using DbConnect dbConnect = DbConnectUtils.GetDbContext();
                DbUsers us = dbConnect.DbUsers.FirstOrDefault(c=>c.Id == 1);

                List<DbHold> hlist = dbConnect.DbHold.Where(c => c.State == 0 && c.Isdelete == 0).ToList();
                foreach (DbHold hold in hlist)
                {
                    decimal yejijine = hold.Jprice;


                    //decimal yjprice = hold.Rishouyi + hold.Yajin;
                    decimal mey = yejijine * (decimal)hold.Jsybili / 100;
                    decimal kou = yejijine * (decimal)hold.Jsjbili / 100;
                    decimal shouyi = mey + kou;
                    decimal zjine = hold.Jprice + shouyi + hold.Zshouyi;
                    decimal mey2 = zjine * (decimal)hold.Jsybili / 100;
                    decimal kou2 = zjine * (decimal)hold.Jsjbili / 100;

                    hold.Jprice = zjine;
                    hold.Zshouyi += shouyi;
                    hold.Rishouyi = mey;
                   // hold.Yajin = kou;
                    hold.State = 1;
                    hold.Buid = us.Id;
                    hold.Buserid = us.Userid;
                    hold.Busername = us.Username;
                    hold.Busertel = us.Usertel;
                    hold.Zrdate = DateTime.Now;
                    hold.Edate = DateTime.Now.AddHours(24);
                }
                dbConnect.SaveChanges();

            }
            catch (Exception ex)
            {
                NLogHelper._.Error("匹配出错", ex);
            }
        }
    }
}

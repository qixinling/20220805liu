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

namespace Server.Quartz.Jobs
{
    public class KangqiangJob : IJob
    {
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public async Task Execute(IJobExecutionContext context)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            try
            {
                
                using DbConnect dbConnect = DbConnectUtils.GetDbContext();
                DateTime a = DateTime.Now;

                TimeSpan ts1 = new TimeSpan(a.Hour, a.Minute, a.Second);

                TimeSpan sts1 = new TimeSpan(00, 00, 00);
                TimeSpan ets1 = new TimeSpan(23, 59, 59);

                List<DbSite> slist = dbConnect.DbSite.Where(s=>s.Iskf == 1).ToList();
                foreach (DbSite item in slist)
                {
                    if (ts1 >= item.Sdate && ts1 < item.Edate)
                    {
                        item.Ispay = 1;

                    }
                    else
                    {
                        item.Ispay = 0;
                    }
                   
                }
                dbConnect.SaveChanges();
            }
            catch (Exception ex)
            {
                NLogHelper._.Error("开抢时间出错", ex);
            }
        }
    }
}

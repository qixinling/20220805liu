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
using Microsoft.EntityFrameworkCore;

namespace Server.Quartz.Jobs
{
    public class YejiJob : IJob
    {
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public async Task Execute(IJobExecutionContext context)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            try
            {
                //每日清空日团队业绩riteamyeji
                using DbConnect dbConnect = DbConnectUtils.GetDbContext();
                dbConnect.Database.ExecuteSqlRaw("update `db_users` set riteamyeji=0 where riteamyeji > 0");
            }
            catch (Exception ex)
            {
                NLogHelper._.Error("清空日团队业绩出错", ex);
            }
        }
    }
}

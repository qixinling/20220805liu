using Quartz;
using Server.Utils.Configuration_Utils;
using System;
using System.IO;
using System.Threading.Tasks;
using Server.Logs;
using Server.Models;

namespace Server.Quartz.Jobs
{
    public class BackUpJob : IJob
    {
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public async Task Execute(IJobExecutionContext context)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            try
            {
                NLogHelper._.Info("开始自动备份");

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BackUp");
                DirectoryInfo folder = new DirectoryInfo(path);

                DateTime date = DateTime.Now;

                TimeSpan ts1 = new TimeSpan(date.Date.Ticks);

                //删除超过7天的auto文件
                foreach (FileInfo fileinfo in folder.GetFiles("auto*"))
                {
                    TimeSpan ts2 = new TimeSpan(fileinfo.CreationTime.Date.Ticks).Subtract(ts1).Duration();
                    if (ts2.TotalDays > 7)
                    {
                        fileinfo.Delete();
                    }
                }
                string fileName = string.Format("auto{0}{1}{2}{3}{4}{5}", date.Year.ToString(), date.Month.ToString(), date.Day.ToString(), date.Hour.ToString(), date.Minute.ToString(), date.Second.ToString());

#if DEBUG
                string _server = ConfigUtils.Configuration["AppSettings:server_debug"];
#else
                string _server = ConfigUtils.Configuration["AppSettings:server"];
#endif

                BackupUtils.Backup(_server, ConfigUtils.Configuration["AppSettings:user"], ConfigUtils.Configuration["AppSettings:pwd"], ConfigUtils.Configuration["AppSettings:database"], path, fileName);

                NLogHelper._.Info("自动备份完成");
            }
            catch (Exception ex)
            {
                NLogHelper._.Error("自动备份任务出错", ex);
            }
        }
    }
}

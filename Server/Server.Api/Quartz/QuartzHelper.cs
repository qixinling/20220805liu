using Quartz;
using Quartz.Impl;
using Server.Api.Utils;
using Server.Quartz.Jobs;
using StackExchange.Redis;
using System;

namespace Server.Quartz
{
    public static class QuartzHelper
    {
        public static async void Run()
        {
            //获取调度器实例
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            //开启调度器
            await scheduler.Start();

            //每天2点随机分钟执行备份
            Random rd = new Random();
            int min = rd.Next(0, 59);
            await JobHelper<BackUpJob>.JobAddAsync("job_backup", "trigger_backup", "group_backup", "0 " + min.ToString() + " 2/23 * * ? ", 1);
            // 0 16 0/1 * * ?  

            //交易匹配
            // await JobHelper<PepeiJob>.JobAddAsync("job_Pipei", "trigger_Pipei", "group_Pipei", "0 0/1 * * * ?", 1);
#if !DEBUG
            //await JobHelper<KangqiangJob>.JobAddAsync("job_kaiqiang", "trigger_kaiqiang", "group_kaiqiang", "0 0/1 * * * ? ", 1);
          
            
#endif
            await JobHelper<KangqiangJob>.JobAddAsync("job_kaiqiang", "trigger_kaiqiang", "group_kaiqiang", "0 0/1 * * * ? ", 1);
            
        }
    }
}

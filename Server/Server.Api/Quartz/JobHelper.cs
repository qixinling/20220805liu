using Quartz;
using Quartz.Impl;
using Server.Logs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Quartz
{
    public static class JobHelper<T> where T : IJob
    {
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="jobName">任务名</param>
        /// <param name="groupName">分组名</param>
        /// /// <param name="triggerName">触发器名</param>
        /// <param name="corn">corn表达式 https://cron.qqe2.com 可以生成</param>
        /// <param name="startAt">任务添加后等待几秒启动</param>
        /// <param name="jdm">传递参数</param>
        /// <returns></returns>
        public static async Task JobAddAsync(string jobName, string groupName, string triggerName, string corn, int startAt, JobDataMap jdm = null)
        {
            try
            {
                //获取调度器实例
                IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();

                //创建任务
                IJobDetail job = JobBuilder.Create<T>()
                    .WithIdentity(jobName, groupName)
                    .Build();

                /*
                 * 传递参数
                 * 取值context.MergedJobDataMap.GetString("keyName");
                 */
                if (jdm != null)
                {
                    foreach (KeyValuePair<string, object> kvp in jdm)
                    {
                        job.JobDataMap.Add(kvp);
                    }
                }

                //创建触发器
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(triggerName, groupName)
                    .StartAt(DateTime.Now.AddSeconds(startAt))
                    .WithCronSchedule(corn)  //corn 表达式 
                    .Build();

                //把任务和触发器加入调度器.
                await scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(string.Format("添加任务出错{0}", jobName), ex);
            }
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="jobName">任务名</param>
        /// <param name="groupName">分组名</param>
        /// <returns></returns>
        public static async Task JobRemove(string jobName, string groupName)
        {
            try
            {
                //获取调度器实例
                IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();

                //创建任务
                IJobDetail job = JobBuilder.Create<T>()
                    .WithIdentity(jobName, groupName)
                    .Build();

                //删除任务需要任务名,组名与当前正在运行中的任务相同,否则无法删除
                await scheduler.DeleteJob(job.Key);
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(string.Format("移除任务出错{0}", jobName), ex);
            }
        }
    }
}

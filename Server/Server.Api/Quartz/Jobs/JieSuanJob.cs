using Quartz;
using System;
using System.Threading.Tasks;
using Server.Logs;
using StackExchange.Redis;
using Newtonsoft.Json;
using Server.Api.Utils;
using System.Collections.Generic;
using Server.Models.DataBaseModels;
using Server.Models;
using System.Linq;
using Server.Bonus.Utils;
using Server.Bonus;
using Server.Api.Bonus.Algorithm;
using Server.Api.Method;
using Server.Api.Level;

namespace Server.Quartz.Jobs
{
    public class JieSuanJob : IJob
    {
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public async Task Execute(IJobExecutionContext context)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            try
            {
                using RedisUtils redisUtils = new RedisUtils();
                IDatabase _reids = redisUtils.GetDatabase();

                if (_reids.StringGet("jsDate").IsNull)
                {
                    NLogHelper._.Info("没有结算预约");
                }
                else
                {
                    DateTime jsDate = DateTime.Parse(_reids.StringGet("jsDate"));
                    NLogHelper._.Info($"结算预约:{jsDate}");
                    if (jsDate < DateTime.Now)
                    {
                        NLogHelper._.Info("开始结算");
                        _reids.KeyDelete("jsDate"); //不管结算是否成功,为了避免重复结算,都应该先删除预约

                        using DbConnect _dbConnect = DbConnectUtils.GetDbContext();

                        using var transaction = _dbConnect.Database.BeginTransaction();

                        Dictionary<string,decimal> bonusDic = SystemSettingBonusUtils.GetBonusParameter(_dbConnect);

                        List<IBonus> bonusList = BonusUtils.BonusList;

                        Ulevel ulevel = new Ulevel();

                        decimal sjjineTotal = 0;
                        decimal yjjine = 0;
                        List<DbUsers> usList = _dbConnect.DbUsers.Where(u => u.Yjjine > 0 || u.Sjjine > 0).ToList();
                        foreach (DbUsers us in usList)
                        {
                            sjjineTotal += us.Sjjine;
                            us.Sjjine = 0;

                            if (us.Yjjine > 0)
                            {
                                yjjine += us.Yjjine;
                                us.Lastyjjine = us.Yjjine;
                                us.Yjjine = 0;

                                bonusList[1].Execute(us.Id, us.Lastyjjine, _dbConnect);
                                bonusList[2].Execute(us.Id, us.Lastyjjine, _dbConnect);

                                UsersUtils.AddReteamYeji(us.Reid, us.Repath, us.Lastyjjine, 1, 1, _dbConnect);
                                //升级移到AddReteamYeji里
                                //ulevel.LevelUp(bonusDic, _dbConnect);
                            }
                        }

                        YejiUtils ym = new YejiUtils(_dbConnect);
                        ym.AddShangjiaZonge(sjjineTotal);
                        ym.AddUser(0, yjjine);

                        _dbConnect.SaveChanges();
                        transaction.Commit();

                        BonusUtils.FaFang();

                        NLogHelper._.Info("结算完成");
                    }
                }
            }
            catch (Exception ex)
            {
                NLogHelper._.Error("自动结算出错", ex);
            }
        }
    }
}

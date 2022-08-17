using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Api.Bonus.Algorithm;
using Server.Api.Level;
using Server.Api.Method;
using Server.Api.Utils;
using Server.Api.Utils.Public;
using Server.Bonus;
using Server.Bonus.Utils;
using Server.Logs;
using Server.Models;
using Server.Models.DataBaseModels;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    /// <summary>
    /// 测试接口
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly IDatabase _redis;
        /// <summary>
        /// 数据上下文依赖注入
        /// </summary>
        /// <param name="dbConnect">数据上下文</param>
        /// <param name="redisUtils">redis数据库</param>
        public TestController(DbConnect dbConnect, RedisUtils redisUtils)
        {
            _dbConnect = dbConnect;
            _redis = redisUtils.GetDatabase();
        }

        /// <summary>
        /// 测试
        /// </summary>
        [HttpGet]
        public string Test()
        {
            using DbConnect _dbConnect = DbConnectUtils.GetDbContext();

            using var transaction = _dbConnect.Database.BeginTransaction();

            Dictionary<string, decimal> bonusDic = SystemSettingBonusUtils.GetBonusParameter(_dbConnect);

            List<IBonus> bonusList = BonusUtils.BonusList;

            Ulevel ulevel = new Ulevel();

            decimal sjjineTotal = 0;
            List<DbUsers> usList = _dbConnect.DbUsers.Where(u => u.Yjjine > 0 || u.Sjjine > 0).ToList();
            foreach (DbUsers us in usList)
            {
                sjjineTotal += us.Sjjine;
                us.Sjjine = 0;

                if (us.Yjjine > 0)
                {
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

            _dbConnect.SaveChanges();
            transaction.Commit();

            BonusUtils.FaFang();

            return "cg";
        }

        [HttpGet]
        public List<string> Testxxx()
        {
            List<string> msgs = new List<string>();
            /*List<DbWallets> walletsList = _dbConnect.DbWallets.Where(w => w.Cid == 1 && w.Jine > 0).ToList();
            foreach (DbWallets wallet in walletsList)
            {
                decimal zjine = _dbConnect.DbBonusSource.Where(b => b.Uid == wallet.Uid).Sum(b => b.Jine);
                if (wallet.Jine != zjine)
                {
                    msgs.Add($"uid:{wallet.Uid} userid:{wallet.Userid} 奖金来源统计:{zjine} 钱包余额:{wallet.Jine}");
                    wallet.Jine = zjine;
                }
            }
            _dbConnect.SaveChanges();*/
            return msgs;
        }
    }
}

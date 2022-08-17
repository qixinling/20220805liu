using Server.Models;
using System;
using System.Collections.Generic;
using Server.Logs;
using Server.Bill.Utils;
using Server.Models.DataBaseModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Server.Wallet.Utils;
using Server.Api.Bonus.Algorithm;
using Server.Bonus.Utils;

namespace Server.Bonus
{
    public static class BonusUtils
    {
        /// <summary>
        /// 暴露奖金算法相关信息
        /// </summary>
        public static List<IBonus> BonusList
        {
            get => new List<IBonus>
            {
                new BonusNotImplemented(0, "总计", true),
                new BonusTuiJianJiang(1, "直推奖", true),
                new BonusNotImplemented(2, "", false),
                new BonusNotImplemented(3, "", false),
                new BonusNotImplemented(4, "-", false),
                new BonusNotImplemented(5, "-", false),
                new BonusNotImplemented(6, "-", false),
                new BonusNotImplemented(7, "-", false),
                new BonusNotImplemented(8, "-", false),
                new BonusNotImplemented(9, "-", false),
                new BonusNotImplemented(10, "-", false),
                new BonusNotImplemented(11, "-", false),
                new BonusNotImplemented(12, "-", false),
                new BonusNotImplemented(13, "-", false),
                new BonusNotImplemented(14, "-", false),
                new BonusNotImplemented(15, "-", false)
            };
        }

        /// <summary>
        /// 奖金发放
        /// </summary>
        /// <returns></returns>
        public static Result FaFang()
        {
            Result res = new Result();
            try
            {
                using DbConnect dbConnect = DbConnectUtils.GetDbContext();
                using var transaction = dbConnect.Database.BeginTransaction();

                //待发放的奖金累计
                List<PrepareBonus> prepareBonusList = new List<PrepareBonus>();

                #region 统计每个用户应发多少
                List<DbBonusSource> bsList = dbConnect.DbBonusSource.FromSqlRaw("select * from `db_bonus_source` where state = 0 for update").ToList();
                if (bsList.Count == 0) { return res; }

                DbBonus bonus = dbConnect.DbBonus.FirstOrDefault(b => b.Btdate.Date == DateTime.Now.Date);
                if (bonus == null)
                {
                    bonus = new DbBonus
                    {
                        Btdate = DateTime.Now
                    };
                    dbConnect.DbBonus.Add(bonus);
                }

                foreach (DbBonusSource bs in bsList.OrderBy(s => s.Uid).ThenBy(s => s.Cid))
                {
                    bs.State = 1;
                    bs.Edate = DateTime.Now;

                    bonus.DbBonusSource.Add(bs);

                    if (prepareBonusList.Count == 0)
                    {
                        prepareBonusList.Add(new PrepareBonus
                        {
                            Uid = bs.Uid,
                            Userid = bs.Userid,
                            Username = bs.Username,
                            Dic = new Dictionary<int, decimal>
                            {
                                {bs.Cid,bs.Jine }
                            }
                        });
                    }
                    else
                    {
                        if (prepareBonusList[^1].Uid == bs.Uid)
                        {
                            if (prepareBonusList[^1].Dic.ContainsKey(bs.Cid))
                            {
                                prepareBonusList[^1].Dic[bs.Cid] += bs.Jine;
                            }
                            else
                            {
                                prepareBonusList[^1].Dic.Add(bs.Cid, bs.Jine);
                            }
                        }
                        else
                        {
                            prepareBonusList.Add(new PrepareBonus
                            {
                                Uid = bs.Uid,
                                Userid = bs.Userid,
                                Username = bs.Username,
                                Dic = new Dictionary<int, decimal>
                                {
                                    {bs.Cid,bs.Jine }
                                }
                            });
                        }
                    }
                }
                #endregion

                decimal meyjine = 0;
                #region 开始发放奖金
                foreach (PrepareBonus pb in prepareBonusList)
                {
                    DbUsers us = dbConnect.DbUsers.FirstOrDefault(c=>c.Id == pb.Uid);
                    if(us == null) { continue; }
                   
                    Dictionary<int, decimal> cidAmount = new Dictionary<int, decimal>();
                    foreach (KeyValuePair<int, decimal> kv in pb.Dic)
                    {
                        decimal Jine = kv.Value;
                        WalletsUtils.UpdateBalance(pb.Uid, kv.Key, Jine, dbConnect);  //奖金进入钱包余额
                        cidAmount.Add(kv.Key, Jine);
                        meyjine += Jine;

                        us.Djjine += Jine;
                    }

                    IBill bill = new BillBonus();
                    bill.Create(pb.Uid, cidAmount, dbConnect);
                }

                #endregion

                dbConnect.SaveChanges();

                transaction.Commit();

                YejiUtils ym = new YejiUtils();
                ym.AddBonus(meyjine);
            }
            catch (Exception ex)
            {
                res.Error("奖金发放异常");
                NLogHelper._.Error(res.Msg, ex);
            }
            return res;
        }

        /// <summary>
        /// 待发奖金模型
        /// </summary>
        private class PrepareBonus
        {
            public int Uid { get; set; }
            public string Userid { get; set; }
            public string Username { get; set; }
            public Dictionary<int,decimal> Dic { get; set; }
        }
    }
}
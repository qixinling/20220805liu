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
        /// 计算级差
        /// </summary>
        /// <param name="yid">会员绑定画室长的id</param>
        /// <param name="jine">会员上架画的价格</param>
        public static void JiCha(int yid, decimal jine)
        {
            /*
             *初级    0.5%
             *中一级  0.6%
             *中二级  0.8%
             *中三级  1%
             *高级    1.2%
            */
            try
            {
                using DbConnect _dbConnect = DbConnectUtils.GetDbContext();
                DbUsers yus = _dbConnect.DbUsers.FirstOrDefault(u => u.Id == yid && u.Xlevel >= 1);
                if (yus != null)
                {
                    decimal bl = 0;
                    switch (yus.Xlevel)
                    {
                        case 1:
                            bl = (decimal)0.5;
                            break;
                        case 2:
                            bl = (decimal)0.6;
                            break;
                        case 3:
                            bl = (decimal)0.8;
                            break;
                        case 4:
                            bl = 1;
                            break;
                        case 5:
                            bl = (decimal)1.2;
                            break;
                    }
                    DbUsers lastUs = yus;
                    List<DbUsers> usList = _dbConnect.DbUsers.Where(u => EF.Functions.Like(yus.Repath, "%," + u.Id + ",%") && u.Xlevel > yus.Xlevel).OrderByDescending(u => u.Relevel).ToList();
                    foreach (var us in usList)
                    {
                        if (us.Xlevel > lastUs.Xlevel)
                        {
                            decimal bili = 0;
                            switch (us.Xlevel)
                            {
                                case 1:
                                    bili = (decimal)0.5;
                                    break;
                                case 2:
                                    bili = (decimal)0.6;
                                    break;
                                case 3:
                                    bili = (decimal)0.8;
                                    break;
                                case 4:
                                    bili = 1;
                                    break;
                                case 5:
                                    bili = (decimal)1.2;
                                    break;
                            }

                            decimal amount = jine * (bili - bl) / 100;

                            //生成打款记录
                            DbJichaDakuan jichaDakuan = new DbJichaDakuan
                            {
                                Uid = lastUs.Id,
                                Userid = lastUs.Userid,
                                Username = lastUs.Username,
                                Jine = amount,
                                Suid = us.Id,
                                Suserid = us.Userid,
                                Susername = us.Username,
                                Cdate = DateTime.Now
                            };

                            _dbConnect.DbJichaDakuan.Add(jichaDakuan);

                            bl = bili;
                            lastUs = us;
                        }
                    }

                    //结束后强制写入一条平台的记录,平台拿2%
                    _dbConnect.DbJichaDakuan.Add(new DbJichaDakuan
                    {
                        Uid = lastUs.Id,
                        Userid = lastUs.Userid,
                        Username = lastUs.Username,
                        Jine = jine * (2 - bl) / 100,
                        Suid = 1,
                        Suserid = "平台",
                        Susername = "平台",
                        Cdate = DateTime.Now
                    });
                }
                _dbConnect.SaveChanges();
            }
            catch (Exception ex)
            {
                NLogHelper._.Error("计算级差异常", ex);
            }
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
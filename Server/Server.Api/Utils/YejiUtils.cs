using Server.Models.DataBaseModels;
using Server.Models;
using System;
using System.Linq;
using Server.Logs;

namespace Server.Bonus.Utils
{
    public class YejiUtils
    {
        private DbConnect _dbConnect;
        public YejiUtils(DbConnect dbConnect = null)
        {
            if (dbConnect == null)
            {
                _dbConnect = DbConnectUtils.GetDbContext();
            }
            else
            {
                _dbConnect = dbConnect;
            }
        }

        /// <summary>
        /// 统计新增会员业绩
        /// </summary>
        /// <param name="Num">新增人数</param>
        /// <param name="Jine">新增业绩</param>
        public Result AddUser(int Num, decimal Jine)
        {
            Result res = new Result();
            try
            {
                DateTime dtime = DateTime.Now;
                DbSystemAchievement at = _dbConnect.DbSystemAchievement.Where(a => a.Adate.Year == dtime.Year && a.Adate.Month == dtime.Month && a.Adate.Day == dtime.Day).FirstOrDefault();
                if (at != null)
                {
                    at.Userscount += Num;
                    at.Usersjine += Jine;
                }
                else
                {
                    at = new DbSystemAchievement
                    {
                        Userscount = Num,
                        Usersjine = Jine,
                        Adate = DateTime.Now
                    };
                    _dbConnect.DbSystemAchievement.Add(at);
                }

                _dbConnect.SaveChanges();

                res.Code = 100;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(res.Msg = "统计会员业绩出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 统计奖金发放业绩
        /// </summary>
        /// <param name="Jine">发放金额</param>
        public Result AddBonus(decimal Jine)
        {
            Result res = new Result();
            try
            {
                DateTime dtime = DateTime.Now;
                DbSystemAchievement at = _dbConnect.DbSystemAchievement.Where(a => a.Adate.Year == dtime.Year && a.Adate.Month == dtime.Month && a.Adate.Day == dtime.Day).FirstOrDefault();
                if (at != null)
                {
                    at.Bonus += Jine;
                }
                else
                {
                    at = new DbSystemAchievement
                    {
                        Bonus = Jine,
                        Adate = DateTime.Now
                    };
                    _dbConnect.DbSystemAchievement.Add(at);
                }
                _dbConnect.SaveChanges();

                res.Code = 100;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(res.Msg = "统计奖金业绩出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 统计商城业绩
        /// </summary>
        /// <param name="OrderNum">订单数量</param>
        /// <param name="GoodsNum">产品出售数量</param>
        /// <param name="Jine">出售金额</param>
        public Result AddShop(int OrderNum, int GoodsNum, decimal Jine)
        {
            Result res = new Result();
            try
            {
                DateTime dtime = DateTime.Now;
                DbSystemAchievement at = _dbConnect.DbSystemAchievement.Where(a => a.Adate.Year == dtime.Year && a.Adate.Month == dtime.Month && a.Adate.Day == dtime.Day).FirstOrDefault();
                if (at != null)
                {
                    at.Ordercount += OrderNum;
                    at.Gouwucount += GoodsNum;
                    at.Gouwujine += Jine;
                }
                else
                {
                    at = new DbSystemAchievement
                    {
                        Ordercount = OrderNum,
                        Gouwucount = GoodsNum,
                        Gouwujine = Jine,
                        Adate = DateTime.Now
                    };
                    _dbConnect.DbSystemAchievement.Add(at);
                }
                _dbConnect.SaveChanges();

                res.Code = 100;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(res.Msg = "统计商城业绩出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 统计充值金额
        /// </summary>
        /// <param name="Jine">金额</param>
        public Result AddChongzhi(decimal Jine)
        {
            Result res = new Result();
            try
            {
                DateTime dtime = DateTime.Now;
                DbSystemAchievement at = _dbConnect.DbSystemAchievement.Where(a => a.Adate.Year == dtime.Year && a.Adate.Month == dtime.Month && a.Adate.Day == dtime.Day).FirstOrDefault();
                if (at != null)
                {
                    at.Chongzhijine += Jine;
                }
                else
                {
                    at = new DbSystemAchievement
                    {
                        Chongzhijine = Jine,
                        Adate = DateTime.Now
                    };
                    _dbConnect.DbSystemAchievement.Add(at);
                }
                _dbConnect.SaveChanges();

                res.Code = 100;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(res.Msg = "统计充值金额出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 统计提现金额
        /// </summary>
        /// <param name="Jine">金额</param>
        public Result AddTixian(decimal Jine)
        {
            Result res = new Result();
            try
            {
                DateTime dtime = DateTime.Now;
                DbSystemAchievement at = _dbConnect.DbSystemAchievement.Where(a => a.Adate.Year == dtime.Year && a.Adate.Month == dtime.Month && a.Adate.Day == dtime.Day).FirstOrDefault();
                if (at != null)
                {
                    at.Tixianjine += Jine;
                }
                else
                {
                    at = new DbSystemAchievement
                    {
                        Tixianjine = Jine,
                        Adate = DateTime.Now
                    };
                    _dbConnect.DbSystemAchievement.Add(at);
                }

                _dbConnect.SaveChanges();

                res.Code = 100;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(res.Msg = "统计提现金额出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 全网已消耗的上架费总额；
        /// </summary>
        /// <param name="Jine">金额</param>
        public Result AddShangjiaZonge(decimal Jine)
        {
            Result res = new Result();
            try
            {
                DateTime dtime = DateTime.Now;
                DbSystemAchievement at = _dbConnect.DbSystemAchievement.Where(a => a.Adate.Year == dtime.Year && a.Adate.Month == dtime.Month && a.Adate.Day == dtime.Day).FirstOrDefault();
                if (at != null)
                {
                    at.Shangjiajine += Jine;
                }
                else
                {
                    at = new DbSystemAchievement
                    {
                        Shangjiajine = Jine,
                        Adate = DateTime.Now
                    };
                    _dbConnect.DbSystemAchievement.Add(at);
                }

                _dbConnect.SaveChanges();

                res.Code = 100;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(res.Msg = "统计已消耗的上架费出错", ex);
            }
            return res;
        }

        /// <summary>
        /// 全网信用值余额总额；
        /// </summary>
        /// <param name="Jine">金额</param>
        public Result AddXyzZonge(decimal Jine)
        {
            Result res = new Result();
            try
            {
                DateTime dtime = DateTime.Now;
                decimal zjine = _dbConnect.DbWallets.Where(c => c.Cid == 2).Sum(c => c.Jine);
                DbSystemAchievement at = _dbConnect.DbSystemAchievement.Where(a => a.Adate.Year == dtime.Year && a.Adate.Month == dtime.Month && a.Adate.Day == dtime.Day).FirstOrDefault();
                if (at != null)
                {
                    at.Xyzjine = zjine;
                }
                else
                {
                    at = new DbSystemAchievement
                    {
                        Xyzjine = zjine,
                        Adate = DateTime.Now
                    };
                    _dbConnect.DbSystemAchievement.Add(at);
                }

                _dbConnect.SaveChanges();

                res.Code = 100;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(res.Msg = "统计信用值余额出错", ex);
            }
            return res;
        }

        public Result AddZhuanhuan(decimal Jine)
        {
            Result res = new Result();
            try
            {
                DateTime dtime = DateTime.Now;
                DbSystemAchievement at = _dbConnect.DbSystemAchievement.Where(a => a.Adate.Year == dtime.Year && a.Adate.Month == dtime.Month && a.Adate.Day == dtime.Day).FirstOrDefault();
                if (at != null)
                {
                    at.Zhuanjine += Jine;
                }
                else
                {
                    at = new DbSystemAchievement
                    {
                        Zhuanjine = Jine,
                        Adate = DateTime.Now
                    };
                    _dbConnect.DbSystemAchievement.Add(at);
                }

                _dbConnect.SaveChanges();

                res.Code = 100;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(res.Msg = "统计消费券转换为信用值的总额出错", ex);
            }
            return res;
        }
    }
}
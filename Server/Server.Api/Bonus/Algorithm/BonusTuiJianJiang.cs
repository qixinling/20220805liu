using Microsoft.EntityFrameworkCore;
using Server.Api.Method;
using Server.Api.Utils;
using Server.Logs;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Api.Bonus.Algorithm
{
    /// <summary>
    /// 给Yid的推荐人发放奖金
    /// </summary>
    public class BonusTuiJianJiang : IBonus
    {
        public int BonusLx { get; }
        public string BonusName { get; }
        public bool IsDisplay { get; }
        public Result Res { get; set; } = new Result();

        public BonusTuiJianJiang(int lx)
        {
            BonusLx = lx;
        }

        public BonusTuiJianJiang(int lx, string name, bool display)
        {
            BonusLx = lx;
            BonusName = name;
            IsDisplay = display;
        }

        public Result Execute(int Yid, decimal Jine, DbConnect dbConnect = null, int Cid = 1)
        {
            try
            {
                dbConnect ??= DbConnectUtils.GetDbContext();
                DbUsers yus = dbConnect.DbUsers.FirstOrDefault(u => u.Id == Yid);
                if (yus == null) { return Res; }
                DbUsers reus = dbConnect.DbUsers.FirstOrDefault(u => u.Id == yus.Reid);
                if (reus == null) { return Res; }
                Dictionary<string, decimal> bonusDic = SystemSettingBonusUtils.GetBonusParameter(dbConnect);

                decimal amount = Jine * (decimal)0.5 / 100;
                if (amount > 0)
                {
                    
                        dbConnect.DbBonusSource.Add(new DbBonusSource
                        {
                            Yid = yus.Id,
                            Yuserid = yus.Userid,
                            Yusername = yus.Username,
                            Uid = reus.Id,
                            Userid = reus.Userid,
                            Username = reus.Username,
                            Lx = BonusLx,
                            Jine = amount,
                            Cid = Cid,
                            Sdate = DateTime.Now,
                            Bz = "-"
                        });
                    
                }
                dbConnect.SaveChanges();
                Res.Done(null, "结算完成");
            }
            catch (Exception ex)
            {
                Res.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "出错");
                NLogHelper._.Error(Res.Msg, ex);
            }
            return Res;
        }
    }
}

using Server.Api.Method;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Utils
{
    public static class SystemSettingBonusUtils
    {
        /// <summary>
        /// 把奖金参数集合转为dic方便调用
        /// </summary>
        /// <param name="dbConnect"></param>
        /// <returns></returns>
        public static Dictionary<string, decimal> GetBonusParameter(DbConnect dbConnect)
        {
            List<DbSystemSettingBonus> systemSettingBonusList = new SystemSettingBonusMethod(dbConnect).GetList();
            Dictionary<string, decimal> dic = new Dictionary<string, decimal>();
            foreach (DbSystemSettingBonus systemSettingBonus in systemSettingBonusList)
            {
                dic.Add(systemSettingBonus.Code, systemSettingBonus.Value);
            }
            return dic;
        }

    }
}

using Server.Models;
using Server.Models.DataBaseModels;
using System.Collections.Generic;

namespace Server.Bill.Utils
{
    public interface IBill
    {

        int BillLx { get; }
        string BillName { get; }

        /// <summary>
        /// 创建账单
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="cidAmount"></param>
        /// <param name="dbConnect"></param>
        /// <param name="bz"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Result Create(int uid, Dictionary<int,decimal> cidAmount, DbConnect dbConnect, string bz = null, int state = 1);
    }
}

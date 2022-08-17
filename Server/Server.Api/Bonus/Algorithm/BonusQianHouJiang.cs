using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Bonus.Algorithm
{
    /// <summary>
    /// 自动赠送前N位、后N位代理的见点奖收入的20%。不分区域，按激活时间为准
    /// </summary>
    public class BonusQianHouJiang : IBonus
    {
        public string BonusName => throw new NotImplementedException();

        public int BonusLx => throw new NotImplementedException();

        public bool IsDisplay => throw new NotImplementedException();

        public Result Res { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Result Execute(int Yid, decimal Jine, DbConnect dbConnect = null, int Cid = 1)
        {
            throw new NotImplementedException();
        }
    }
}

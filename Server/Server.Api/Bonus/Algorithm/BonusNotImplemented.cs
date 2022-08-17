using Server.Models;
using Server.Models.DataBaseModels;
using System;

namespace Server.Api.Bonus.Algorithm
{
    /// <summary>
    /// 此类不做任何功能实现,只作为占位用
    /// </summary>
    public class BonusNotImplemented : IBonus
    {
        public string BonusName { get; }

        public int BonusLx { get; }

        public bool IsDisplay { get; }

        public Result Res { get; set; } = new Result();

        public BonusNotImplemented(int lx, string name, bool display)
        {
            BonusLx = lx;
            BonusName = name;
            IsDisplay = display;
        }

        public Result Execute(int Yid, decimal Jine, DbConnect dbConnect = null, int Cid = 1)
        {
            //不要实现该方法
            throw new NotImplementedException();
        }
    }
}

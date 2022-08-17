using Server.Models;
using Server.Models.DataBaseModels;

namespace Server.Api.Bonus.Algorithm
{
    public interface IBonus
    {
        /// <summary>
        /// 奖项名称
        /// </summary>
        string BonusName { get; }
        /// <summary>
        /// 奖项标识
        /// </summary>
        int BonusLx { get; }
        /// <summary>
        /// 是否展示到前端
        /// </summary>
        bool IsDisplay { get; }
        /// <summary>
        /// 返回值,用于需要判断结算是否成功的场景
        /// </summary>
        Result Res { get; set; }

        /// <summary>
        /// 运行结算
        /// </summary>
        /// <param name="Yid">来源ID,无来源填0</param>
        /// <param name="Jine">结算基数,无基数填0</param>
        /// <param name="dbConnect">数据上下文,需要执行事务时传递</param>
        /// <param name="Cid">币种ID,通常情况下是1=奖金</param>
        /// <returns></returns>
        Result Execute(int Yid, decimal Jine, DbConnect dbConnect = null, int Cid = 1);
    }
}

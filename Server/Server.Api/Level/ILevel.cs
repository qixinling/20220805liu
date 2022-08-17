using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Level
{
    public interface ILevel
    {
        /// <summary>
        /// 类型,代表了用户级别,业绩级别,其他级别等
        /// </summary>
        int Lx { get; }
        /// <summary>
        /// 级别数,一级,二级,三级等
        /// </summary>
        int Level { get; set; }
        /// <summary>
        /// 级别名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 该级别所需要的金额/业绩等
        /// </summary>
        decimal Amount { get; set; }
        /// <summary>
        /// 是否显示在选项卡中
        /// </summary>
        bool IsOption { get; set; }
        /// <summary>
        /// 获取级别集合
        /// </summary>
        /// <param name="dbConnect">数据上下文</param>
        /// <returns>级别集合</returns>
        List<ILevel> GetLevels(DbConnect dbConnect = null);
        /// <summary>
        /// 升级方法
        /// </summary>
        /// <param name="dbConnect">数据上下文</param>
        /// <param name="Path">团队路径</param>
        void LevelUp(DbConnect dbConnect, string Path = null);
    }
}

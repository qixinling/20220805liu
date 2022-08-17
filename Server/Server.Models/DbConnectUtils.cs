using Server.Models.DataBaseModels;
using System;

namespace Server.Models
{
    public static class DbConnectUtils
    {
        /// <summary>
        /// 用于帮助静态类获取已注入的服务
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 获取数据库连接上下文
        /// </summary>
        /// <returns></returns>
        public static DbConnect GetDbContext()
        {
            return ServiceProvider.GetService(typeof(DbConnect)) as DbConnect;
        }
    }
}

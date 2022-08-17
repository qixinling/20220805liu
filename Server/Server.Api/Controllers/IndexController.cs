using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Configuration_Utils;
using System.Collections.Generic;
using System.Linq;

namespace Server.Api.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        /// <summary>
        /// 数据库上下文
        /// </summary>
        /// <param name="dbConnect">数据库上下文</param>
        /// <param name="res">接口返回模型</param>
        public IndexController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 显示数据库连接
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Index()
        {
            string res = "done";
#if DEBUG
            res = string.Format("Scaffold-DbContext \"Server = {0}; User Id = {1}; Password = {2}; Database = {3}; \" \"Pomelo.EntityFrameworkCore.MySql\" -Context DbConnect -OutputDir DataBaseModels -Force -NoPluralize"
                , ConfigUtils.Configuration["AppSettings:server_debug"]
                , ConfigUtils.Configuration["AppSettings:user"]
                , ConfigUtils.Configuration["AppSettings:pwd"]
                , ConfigUtils.Configuration["AppSettings:database"]);
#endif
            return res;
        }
    }
}
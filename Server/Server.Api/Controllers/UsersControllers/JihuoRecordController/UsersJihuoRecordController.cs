using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.UsersControllers.JihuoRecordController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersJihuoRecordController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public UsersJihuoRecordController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 激活记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result List(JObject data)
        {
            
            try
            {
                string userid = Convert.ToString(data["userid"]);
                int select_uid = Convert.ToInt32(data["select_uid"]);
                string query_userid = Convert.ToString(data["query_userid"]);
                DateTime query_date = Convert.ToDateTime(data["query_date"]);
                UsersJihuoRecordMethod ujrm = new UsersJihuoRecordMethod(_dbConnect);
                List<DbUsersJihuoRecord> fjrlist = ujrm.GetUidList(select_uid);
                if (PublicUtils.NotNull(query_userid))
                {
                    string Query_userid = query_userid;
                    fjrlist = fjrlist.Where(u => u.Userid.Equals(Query_userid)).ToList();
                }
                if (PublicUtils.NotNull(query_date))
                {
                    
                    fjrlist = fjrlist.Where(f => Convert.ToDateTime(f.Jdate).Year == query_date.Year && Convert.ToDateTime(f.Jdate).Month == query_date.Month && Convert.ToDateTime(f.Jdate).Day == query_date.Day).ToList();
                }
                _res.Done(fjrlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询服务中心激活记录异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }

}

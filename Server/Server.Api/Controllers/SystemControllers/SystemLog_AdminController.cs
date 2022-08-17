using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

namespace Server.Api.Controllers.SystemControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SystemLog_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public SystemLog_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 获取管理员登录记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Login_List(JObject data)
        {
            try
            {
                string userid_admin = Convert.ToString(data["userid_admin"]);

                SystemLogMethod slm = new SystemLogMethod(_dbConnect );
                List<DbSystemLog> systemLogList = new List<DbSystemLog>();
                if(userid_admin == "admin")
                {
                    systemLogList = slm.GetList().Where(l => l.Lx == 0 && l.IsDel == 0).OrderByDescending(l => l.Id).ToList();
                }
                else
                {
                    systemLogList = slm.GetList().Where(l => l.Lx == 0 && l.Userid != "admin" && l.IsDel == 0).OrderByDescending(l => l.Id).ToList();
                }
                
                _res.Done(systemLogList, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取登录日志异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///删除日志
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Operation_Delete(JObject data)
        {
            try
            {
                string delete_id = data["delete_id"].ToString();
                List<DbSystemLog> slist = _dbConnect.DbSystemLog.Where(l => EF.Functions.Like(delete_id, "%," + l.Id + ",%")).ToList();
                foreach (DbSystemLog log in slist)
                {
                    log.IsDel = 1;
                }
                _dbConnect.SaveChanges();
                _res.Done(null, "删除成功");

            }
            catch (Exception ex)
            {
                _res.Error("删除操作日志异常");
             
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询操作日志表所有内容
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Operation_List(JObject data)
        {
            try
            {
                SystemLogMethod slm = new SystemLogMethod(_dbConnect);
                string userid_admin = Convert.ToString(data["userid_admin"]);
                List<DbSystemLog> systemLogList = new List<DbSystemLog>();
                if (userid_admin == "admin")
                {
                    systemLogList = slm.GetList().Where(l => l.Lx > 0 && l.IsDel == 0).OrderByDescending(l => l.Id).Take(5).ToList();
                }
                else
                {
                    systemLogList = slm.GetList().Where(l => l.Lx > 0 && l.Userid != "admin" && l.IsDel == 0).OrderByDescending(l => l.Id).Take(5).ToList();
                }

                _res.Done(systemLogList, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询操作日志记录数据异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 用户资料修改日志
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Users_Update(JObject data)
        {
            try
            {
                int uid = Convert.ToInt32(data["select_id"]);
                SystemLogMethod slm = new SystemLogMethod(_dbConnect);
                List<DbSystemLog> systemLogList = slm.GetList().Where(l => l.Uid == uid && l.Lx == 1).OrderByDescending(l => l.Id).ToList();
                _res.Done(systemLogList, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询修改会员日志异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

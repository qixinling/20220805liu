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

using Server.Utils.Http_Utils;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.UsersControllers.JihuoRecordController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersJihuoRecord_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public UsersJihuoRecord_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 激活记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List()
        {
            

            try
            {
                UsersJihuoRecordMethod ujrm = new UsersJihuoRecordMethod(_dbConnect);
                var fjrlist = ujrm.GetList().OrderBy(m => m.Jdate).Select(f => new
                {
                    f.Id,
                    f.Uid,
                    f.Userid,
                    f.Username,
                    f.Hblx,
                    f.Jine,
                    f.Cid,
                    f.Codename,
                    f.Coinname,
                    f.Jid,
                    f.Jdate,
                    juserid=_dbConnect.DbUsers.FirstOrDefault(u=>u.Id==f.Jid).Userid,
                    jusername= _dbConnect.DbUsers.FirstOrDefault(u => u.Id == f.Jid).Username
                });
                _res.Done(fjrlist, "查询成功");

            }
            catch (Exception ex)
            {
                _res.Error("查询所有服务中心激活记录数据异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除激活记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Delete(JObject data)
        {
            
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                string delete_id = data["delete_id"].ToString();

                string Msg = "";
                string[] Dllist = delete_id.Split(',');
                List<string> Cname = new List<string>();

                UsersJihuoRecordMethod ujrm = new UsersJihuoRecordMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);

                    DbUsersJihuoRecord fj = ujrm.GetById(Id);
                    if (fj != null)
                    {
                        Cname.Add(fj.Juserid);
                        ujrm.Remove(Id);
                        Msg += fj.Userid + "删除成功. ";
                    }
                    else
                    {
                        Msg += "记录不存在,删除失败. ";
                    }
                }
                _dbConnect.SaveChanges();
                _res.Done(null, Msg);
               
                foreach (string name in Cname)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "删除服务中心激活记录:" + name);
                }
            }
            catch (Exception ex)
            {
                _res.Error("删除激活记录异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }

}

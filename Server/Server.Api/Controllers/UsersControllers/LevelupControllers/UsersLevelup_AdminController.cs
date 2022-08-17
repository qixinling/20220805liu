using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;

using Server.Utils.Http_Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Api.Level;

namespace Server.Api.Controllers.UsersControllers.LevelupControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersLevelup_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public UsersLevelup_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询用户升级记录
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
                UsersLevelupMethod ulm = new UsersLevelupMethod(_dbConnect);
                List<Ulevel> uLevelList = new Ulevel().GetLevels();
                var uulist = ulm.GetList().OrderBy(u => u.State).Select(u => new
                {
                    u.Id,
                    u.Userid,
                    u.Username,
                    u.Ylevel,
                    ylevelname = uLevelList[u.Ylevel].Name,
                    u.Level,
                    xulevelname=uLevelList[u.Level].Name,
                    u.Jine,
                    u.Sdate,
                    ispay=u.State
                });
                _res.Done(uulist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询用户升级记录数据异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除升级记录
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
                UsersLevelupMethod ulm = new UsersLevelupMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbUsersLevelup up = ulm.GetById(Id);
                    if (up != null)
                    {
                        Cname.Add(up.Userid);
                        ulm.Remove(Id);
                        Msg += up.Userid + "删除成功. ";
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
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "删除升级记录:" + name);
                }
            }
            catch (Exception ex)
            {
                _res.Error("删除升级记录异常");
           
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

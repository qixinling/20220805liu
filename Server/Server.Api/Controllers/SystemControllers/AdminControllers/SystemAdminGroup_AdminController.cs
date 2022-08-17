using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Http_Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Utils.Permission_Utils;

namespace Server.Api.Controllers.SystemControllers.AdminControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SystemAdminGroup_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public SystemAdminGroup_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result GetPermissionMod()
        {
            
           
            List<PermissionMod> pmlist = PermissionUtils.GetDefaultPermission();

            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "pmlist", JsonConvert.SerializeObject(pmlist) }
            };

            _res.Done(dic, "查询成功");

            return _res;
        }

        /// <summary>
        /// 添加权限组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Add(JObject data)
        {
            
            
            try
            {

                string userid_admin = data["userid_admin"].ToString();
               
                string add_groupname = data["add_groupname"].ToString();


                SystemAdminGroupMethod sagm = new SystemAdminGroupMethod(_dbConnect);
                DbSystemAdminGroup uag = sagm.GetByGroupname(add_groupname);
                if (uag == null)
                {
                    uag = new DbSystemAdminGroup
                    {
                        Groupname = add_groupname
                    };
                    sagm.Add(uag);
                    if (_dbConnect.SaveChanges() > 0)
                    {
                        _res.Done(null, "添加成功");
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 10, "添加职位:" + add_groupname);
                    }
                    else
                    {
                        _res.Fail("添加失败");
                    }
                }
                else
                {
                    _res.Fail("职位已存在");
                   
                }
            }
            catch (Exception ex)
            {
                _res.Error("添加职位异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除管理组
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
                int delete_id = Convert.ToInt32(data["delete_id"]);

                SystemAdminGroupMethod sagm = new SystemAdminGroupMethod(_dbConnect);
                DbSystemAdminGroup ug =sagm.GetById(delete_id);
                if (ug == null) { _res.Fail("职位不存在"); return _res; }

                _dbConnect.Database.ExecuteSqlRaw("delete from `db_system_admin_group` where id=" + delete_id + "");

                _res.Done(null, "删除成功");
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 10, "添加职位:" + ug.Groupname);
            }
            catch (Exception ex)
            {
                _res.Error("删除管理组异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改管理组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Update(JObject data)
        {
               
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                int id = Convert.ToInt32(data["id"]);
                string update_groupname = data["update_groupname"].ToString();
         
                string permission = data["permission"].ToString();
           


                if (update_groupname == " ")
                {
                    update_groupname = null;
                }
                SystemAdminGroupMethod sagm = new SystemAdminGroupMethod(_dbConnect);
                DbSystemAdminGroup uag =sagm.GetById(id);
                if (uag == null) { _res.Fail("职位不存在"); return _res; }

                if (!string.IsNullOrEmpty(update_groupname))
                {
                    uag.Groupname = update_groupname;
                }
                if (!string.IsNullOrEmpty(permission))
                {
                    uag.Permission = permission;
                }
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "修改成功");
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 10, "修改职位:" + update_groupname);
                }
                else
                {
                    _res.Fail("修改失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改职位异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取单个用户组信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {
            
            
            try
            {
                int select_id = Convert.ToInt32(data["select_id"]);
                SystemAdminGroupMethod sagm = new SystemAdminGroupMethod(_dbConnect);
                DbSystemAdminGroup ua =sagm.GetById(select_id);
                if (ua != null)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "id", ua.Id.ToString() },
                        { "groupname", ua.Groupname }
                    };
                    _res.Done(dic, "查询成功");
                }
                else
                {
                    _res.Fail("没有对应id");
             
                }
            }
            catch (Exception ex)
            {
                _res.Error("获取单个用户组信息异常");
             
                NLogHelper._.Error(_res.Msg, ex);
            }

            return _res;
        }

        /// <summary>
        /// 查询所有管理组
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
                List<DbSystemAdminGroup> uaglist = new SystemAdminGroupMethod(_dbConnect).GetList().OrderBy(g => g.Id).ToList();
                _res.Done(uaglist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询所有权限组数据异常");
             
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

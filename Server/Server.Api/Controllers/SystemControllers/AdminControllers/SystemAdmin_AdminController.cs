using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Crypto_Utils;
using Server.Utils.Http_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Utils.Token_Utils;

namespace Server.Api.Controllers.SystemControllers.AdminControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SystemAdmin_AdminController : ControllerBase
    {

        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public SystemAdmin_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 后台登录验证
        /// </summary>
        /// <param name="data">登录数据</param>
        /// <returns></returns>
        [HttpPost]
        [SignCheckFilters]
        public Result Login([FromBody] JObject data)
        {
            string userid = data["userid"].ToString();
            string password = data["password"].ToString();

            if (password != null)
            {
                //AES解密密码
                password = UsersUtils.Decode(password);

                string Passwordmd5 = MD5Utils.MD5Encrypt(password, 32);

                
                DbSystemAdmin admin = _dbConnect.DbSystemAdmin.FirstOrDefault(u => u.Userid.Equals(userid) && u.Password.Equals(Passwordmd5));
                if (admin != null)
                {
                    string Token_str = TokenUtils.Token_admin_add(admin.Id, admin.Userid, 1, HttpInfoUtils.GetIP(), HttpInfoUtils.GetOSVersion());

                    //登录日志
                    DbSystemLog systemLog = new DbSystemLog
                    {
                        Userid = userid,
                        Username = admin.Username,
                        Ip = HttpInfoUtils.GetIP(),
                        Lx = 0,
                        LxName = SystemLogMethod.GetLxName(0),
                        Ldate = DateTime.Now
                    };

                    SystemLogMethod slm = new SystemLogMethod(_dbConnect);
                    slm.Add(systemLog);
                    _dbConnect.SaveChanges();

                    _res.Done(null, "登录成功");

                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "token", Token_str },
                        { "id", admin.Id.ToString() },
                        { "userid", admin.Userid.ToString() }
                    };
                    if (admin.Id > 1)
                    {
                        SystemAdminGroupMethod sagm = new SystemAdminGroupMethod(_dbConnect);
                        DbSystemAdminGroup ug = sagm.GetById(Convert.ToInt32(admin.Gid));
                        if (ug == null) { _res.Fail("权限错误,请重新分配权限"); return _res; }
                        dic.Add("permission", ug.Permission);
                    }
                    _res.Done(dic, "");
                }
                else
                {
                    _res.Fail("账号或密码错误");

                }
            }
            else
            {
                _res.Fail("密码不能为空");

                return _res;
            }
            return _res;
        }

        /// <summary>
        /// 添加管理员
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
            
                string add_userid = data["add_userid"].ToString();
                string add_password = data["add_password"].ToString();
                string add_username = data["add_username"].ToString();

                int permission = Convert.ToInt32(data["permission"]);

                if (!PublicUtils.NotNull(add_userid)) { _res.Fail("请输入用户名"); return _res; }
                if (!PublicUtils.NotNull(add_password)) { _res.Fail("请输入密码");  return _res; }
                if (!PublicUtils.NotNull(add_username)) { _res.Fail("请输入姓名");  return _res; }

                SystemAdminGroupMethod sagm = new SystemAdminGroupMethod(_dbConnect);
                DbSystemAdminGroup ug = sagm.GetById(permission);
                if (ug != null)
                {
                    DbSystemAdmin ua = new DbSystemAdmin
                    {
                        Userid = add_userid
                    };

                    string Passwordmd5 = MD5Utils.MD5Encrypt(add_password, 32);

                    ua.Password = Passwordmd5;
                    ua.Username = add_username;
                    ua.Gid = permission;
                    ua.Permissionname = ug.Groupname;
                    ua.Adate = DateTime.Now;
                    ua.Logintime = DateTime.Now.ToString();

                    SystemAdminMethod sam = new SystemAdminMethod(_dbConnect);
                    sam.Add(ua);
                    if (_dbConnect.SaveChanges() > 0)
                    {
                        _res.Done(null, "添加成功");
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 10, "添加管理员:" + add_userid);
                    }
                    else
                    {
                        _res.Fail("添加失败");
                       
                    }
                }
                else
                {
                    _res.Fail("职位不存在"); return _res;
                }
            }
            catch (Exception ex)
            {
                _res.Error("添加管理员异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除管理员
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
                SystemAdminMethod sam = new SystemAdminMethod(_dbConnect);
                if (delete_id == "")
                {
                    _res.Fail("请勾选删除项");
                   
                }
                else
                {
                    string[] Dllist = delete_id.Split(',');
                    List<string> Cname = new List<string>();
                    foreach (string Dl in Dllist)
                    {
                        int Id = Convert.ToInt32(Dl);
                        if (Id > 1)
                        {
                            DbSystemAdmin ua =sam.GetById(Id);
                            if (ua != null)
                            {
                                Cname.Add(ua.Userid);
                               sam.Remove(Id);
                            }
                        }
                    }
                    if (_dbConnect.SaveChanges() != 0)
                    {
                        _res.Done(null, "删除成功");

                        foreach (string Name in Cname)
                        {
                            SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 10, "删除管理员:" + Name);
                        }
                    }
                    else
                    {
                        _res.Fail("删除失败");
                    }
                }
            }
            catch (Exception ex)
            {
                _res.Error("删除管理员异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询单个数据
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
                SystemAdminMethod sam = new SystemAdminMethod(_dbConnect);
                DbSystemAdmin ua = sam.GetById( select_id);
                if (ua != null)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "id", ua.Id.ToString() },
                        { "userid", ua.Userid },
                        { "username", ua.Username },
                        { "permission", ua.Gid.ToString() }
                    };
                    _res.Done(dic, "查询成功");
                }
                else
                {
                    _res.Fail("获取信息失败");
            
                }
            }
            catch (Exception ex)
            {
                _res.Error("查询单个数据异常");
            
                NLogHelper._.Error(_res.Msg, ex);
            }

            return _res;
        }

        /// <summary>
        /// 查询全部管理员
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
                List<DbSystemAdmin> admin_list = new SystemAdminMethod(_dbConnect).GetList().OrderBy(m => m.Id).ToList();
                _res.Done(admin_list, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询全部管理员异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改管理员
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
                int up_id = Convert.ToInt32(data["up_id"]);
                string username = data["username"].ToString();
                string password = data["password"].ToString();

                
                
                
                SystemAdminMethod sam = new SystemAdminMethod(_dbConnect);
                DbSystemAdmin uaup = sam.GetById(up_id);

                SystemAdminGroupMethod sagm = new SystemAdminGroupMethod(_dbConnect);
                if (uaup != null)
                {
                    uaup.Username = username;
                    if (password != "")
                    {
                        string Passwordmd5 = MD5Utils.MD5Encrypt(password, 32);
                        uaup.Password = Passwordmd5;
                    }
                    if (uaup.Id != 1)
                    {
                        int permission = Convert.ToInt32(data["permission"]);
                        if (uaup.Gid != permission)
                        {
                            DbSystemAdminGroup ug =sagm.GetById(permission);
                            if (ug != null)
                            {
                                uaup.Gid = permission;
                                uaup.Permissionname = ug.Groupname;
                            }
                            else
                            {
                                _res.Fail("职位不存在");  return _res;
                            }
                        }
                    }
                    if (_dbConnect.SaveChanges() > 0)
                    {
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 10, "修改管理员:" + username);

                        _res.Done(null, "修改成功");
                    }
                    /*else if (uaup.Gid == permission)
                    {
                        
                        _res.Fail("无修改"); return _res;
                    }*/
                }
                else
                {
                    _res.Fail("修改失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改管理员异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改管理员密码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Update_Password(JObject data)
        {
           

            try
            {
                string userid_admin = data["userid_admin"].ToString();
                string onenewpassword = data["onenewpassword"].ToString();
                string twonewpassword = data["twonewpassword"].ToString();
                string password = data["password"].ToString();

                
                if (onenewpassword.Equals(twonewpassword))
                {
                    string Passwordmd5 = MD5Utils.MD5Encrypt(password, 32);
                    DbSystemAdmin ua = _dbConnect.DbSystemAdmin.FirstOrDefault(u => u.Userid == userid_admin && u.Password == Passwordmd5);
                    if (ua != null)
                    {
                        string Passwordmd5_new = MD5Utils.MD5Encrypt(onenewpassword, 32);
                        ua.Password = Passwordmd5_new;
                        if (_dbConnect.SaveChanges() > 0)
                        {
                            _res.Done(null, "修改成功");
                            SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 10, "修改管理员密码:" + userid_admin);
                        }
                        else
                        {
                            _res.Fail("修改失败");
                        }
                    }
                    else
                    {
                        _res.Fail("原密码错误");
                       
                    }
                }
                else
                {
                    _res.Fail("两次密码输入不一致");
                   
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改管理员密码异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

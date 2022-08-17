using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Http_Utils;
using Server.Utils.Msg_Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Api.Level;

namespace Server.Api.Controllers.UsersControllers.FwzxController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersFwzxApply_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public UsersFwzxApply_AdminController(DbConnect dbConnect,Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        public string[] IsplayNameArr { get; set; } = new[] { "待审核", "已审核", "已撤销" };

        /// <summary>
        /// 服务中心审核
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]

        public Result Pass(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }

                string fapass_id = data["fapass_id"].ToString();
                int id = Convert.ToInt32(data["id"]);
                int lx = Convert.ToInt32(data["lx"]);

                string[] Dllist = fapass_id.Split(',');
                string Msg = "";
                List<string> Cname = new List<string>();
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    
                    DbUsersFwzxApply fa = _dbConnect.DbUsersFwzxApply.Include(c => c.UidNavigation).FirstOrDefault(c => c.Id == Id && c.Ispay == 0);
                    if (fa != null)
                    {
                        fa.Ispay = 1;
                        fa.Sdate = DateTime.Now;

                        fa.UidNavigation.Isbd = 1;
                        fa.UidNavigation.Bdlevel = fa.Bdlevel;
                        fa.UidNavigation.Bdsheng = fa.Bdsheng;
                        fa.UidNavigation.Bdshi = fa.Bdshi;
                        fa.UidNavigation.Bdxian = fa.Bdxian;
                        fa.UidNavigation.Bdaddress = fa.Bdaddress;
                        fa.UidNavigation.Bddate = DateTime.Now;
                        MsgUtils.Send(0, "服务中心", "您申请的服务中心资格已通过审核", 0, "系统消息", fa.Uid, fa.Userid);
                        Msg += fa.Userid + "审核成功. ";

                        UsersMethod um = new UsersMethod(_dbConnect);
                        DbUsers us = um.GetById( fa.Uid);

                        if (us != null)
                        {
                            Cname.Add(us.Userid);
                        }
                    }
                    else
                    {
                        Msg += "申请记录不存在或信息已改变,审核失败. ";
                    }
                }
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, Msg);
                 
                    foreach (string name in Cname)
                    {
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 2, "审核服务中心申请:" + name);
                    }
                }
                else
                {
                    _res.Fail(Msg);
                
                }
            }
            catch (Exception ex)
            {
                _res.Error("审核异常");
       
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        ///  撤销申请
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Revoke(JObject data)
        {
            
            
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }

                string fapass_id = data["fapass_id"].ToString();
                int id = Convert.ToInt32(data["id"]);
                int lx = Convert.ToInt32(data["lx"]);

                string[] Dllist = fapass_id.Split(',');
                string Msg = "";
                List<string> Cname = new List<string>();
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                   
                    DbUsersFwzxApply fa = _dbConnect.DbUsersFwzxApply.FirstOrDefault(c => c.Id == Id && c.Ispay == 0);
                    if (fa != null)
                    {
                        fa.Ispay = 2;
                        MsgUtils.Send(0, "服务中心", "您申请的服务中心资格撤销", 0, "系统消息", fa.Uid, fa.Userid);
                        Msg += fa.Userid + "撤销成功. ";
                        UsersMethod um = new UsersMethod(_dbConnect);
                        DbUsers us =um.GetById(fa.Uid);
                        if (us != null)
                        {
                            Cname.Add(us.Userid);
                        }
                    }
                    else
                    {
                        Msg += "申请记录不存在或信息已改变,撤销失败. ";
                    }
                }
                if (_dbConnect.SaveChanges() > 0)
                {

                    _res.Done(null, Msg);
                  
                    foreach (string name in Cname)
                    {
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "撤销服务中心申请:" + name);
                    }
                }
                else
                {
                    _res.Fail(Msg);
                    
                }
            }
            catch (Exception ex)
            {
                _res.Error("撤销异常");
          
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除服务中心记录
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
                if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }

                string delete_id = data["delete_id"].ToString();

                string[] Dllist = delete_id.Split(',');
                string Msg = "";
                List<string> Cname = new List<string>();
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    UsersFwzxApplyMethod ufm = new UsersFwzxApplyMethod(_dbConnect);
                    DbUsersFwzxApply fa = ufm.GetById(Id);
                    if (fa != null)
                    {
                       ufm.Remove(Id);
                        Msg += fa.Userid + "删除成功. ";
                    }
                    else
                    {
                        Msg += "数据不存在,删除失败. ";
                    }
                    UsersMethod um = new UsersMethod(_dbConnect);
                    DbUsers us = um.GetById( fa.Uid);
                    if (us != null)
                    {
                        Cname.Add(us.Userid);
                    }
                }
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "删除成功"); ;
                    foreach (string name in Cname)
                    {
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "删除申请记录:" + name);
                    }
                }
                else
                {
                    _res.Fail(Msg);
                   
                }
            }
            catch (Exception ex)
            {
                _res.Error("删除记录异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询所有服务中心申请列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List(JObject data)
        {
            try
            {
                int lx = Convert.ToInt32(data["lx"]);
                UsersFwzxApplyMethod ufm = new UsersFwzxApplyMethod(_dbConnect);
                
                List<ILevel> bdLevelList = new Bdlevel().GetLevels();
                var falist = ufm.GetList().Where(m => m.Lx == lx).OrderBy(m => m.Fdate).Select(m=>new{
                    m.Id,
                    m.Uid,
                    m.Userid,
                    m.Username,
                    m.Bdlevel,
                    bdlevelname= bdLevelList[m.Bdlevel].Name,
                    m.Jine,
                    m.Bdsheng,
                    m.Bdshi,
                    m.Bdxian,
                    m.Bdaddress,
                    m.Fdate,
                    m.Ispay,
                    m.Lx,
                    ispayname=IsplayNameArr[m.Ispay],
                    m.Sdate

                });
                _res.Done(falist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询服务中心申请异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


    }
}

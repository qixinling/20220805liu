using Server.Api.Method;
using Microsoft.AspNetCore.Mvc;


using Server.Models;
using Server.Utils.Msg_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using static Server.Api.Filters;
using Server.Models.DataBaseModels;
using Server.Logs;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Renzheng_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public Renzheng_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }
        /// <summary>
        /// 查询认证记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();

                List<DbRenzheng> ualist = _dbConnect.DbRenzheng.OrderBy(u => u.State).ToList();
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                foreach (DbRenzheng u in ualist)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("id", u.Id.ToString());
                    dic.Add("uid", u.Uid.ToString());
                    dic.Add("userid", u.Userid);
                    dic.Add("username", u.Username);
                    dic.Add("usercode", u.Usercode);
                    dic.Add("zimg", u.Zimg);
                    dic.Add("fimg", u.Fimg);
                    dic.Add("sdate", u.Sdate.ToString());
                    if (u.State == 0)
                    {
                        dic.Add("statename", "待审核");
                    }
                    else if (u.State == 1)
                    {
                        dic.Add("statename", "已审核");
                    }
                    else if (u.State == 2)
                    {
                        dic.Add("statename", "已撤销");
                    }
                    
                    dic.Add("state", u.State.ToString());
                    diclist.Add(dic);
                }
                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 审核会员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Shenhe(JObject data)
        {

            string userid_admin = data["userid_admin"].ToString();

            try
            {
                string ids = data["ids"].ToString();

                List<DbRenzheng> rlist = _dbConnect.DbRenzheng.Where(r => EF.Functions.Like(ids, "%," + r.Id + ",%")).ToList();
                string msg = "";
                foreach (DbRenzheng ua in rlist)
                {
                    DbUsers user = _dbConnect.DbUsers.FirstOrDefault(u => u.Id == ua.Uid);
                    if (user != null)
                    {
                        if (ua.State == 0)
                        {
                            if (user.Isrz == 0)
                            {
                                ua.State = 1;
                                user.Isrz = 1;
                                user.Usercode = ua.Usercode;
                                if (_dbConnect.SaveChanges() > 0)
                                {
                                    msg += ua.Userid + "认证成功. ";
                                    MsgUtils.Send(0, "认证成功", "恭喜，您的认证申请已通过审核", 0, "系统消息", user.Id, user.Userid);
                                }
                                else
                                {
                                    msg += ua.Userid + "认证失败. ";
                                }
                            }
                        }
                        else if (ua.State == 1)
                        {
                            msg += ua.Userid + "已经通过审核，无法再次审核. ";
                        }
                        else if (ua.State == 2)
                        {
                            msg += ua.Userid + "已撤销申请，无法再次审核. ";
                        }
                    }
                    else
                    {
                        msg += ua.Userid + "用户不存在,撤销失败. ";
                    }

                }
                    _res.Done(null, msg);

            }
            catch (Exception ex)
            {
                _res.Error("审核异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 撤销(审核不通过)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Nopass(JObject data)
        {
            
            string userid_admin = data["userid_admin"].ToString();
            
            try
            {
                string ids = data["ids"].ToString();
                string msg = "";
                List<DbRenzheng> rlist = _dbConnect.DbRenzheng.Where(r => EF.Functions.Like(ids, "%," + r.Id + ",%")).ToList();
                foreach (DbRenzheng ua in rlist)
                {
                   
                        if (ua.State == 0)
                        {
                            DbUsers user = _dbConnect.DbUsers.FirstOrDefault(u => u.Id == ua.Uid);
                            if (user != null)
                            {
                                ua.State = 2;//审核不通过
                                if (_dbConnect.SaveChanges() > 0)
                                {
                                    msg += ua.Userid + "撤销成功. ";
                                    MsgUtils.Send(0, "认证失败", "非常抱歉，您的认证申请不通过审核", 0, "系统消息", user.Id, user.Userid);
                                }
                                else
                                {
                                    msg += ua.Userid + "撤销失败. ";
                                }
                            }
                            else
                            {
                                msg += ua.Userid + "用户不存在,撤销失败. ";
                            }
                        }
                        else if (ua.State == 1)
                        {
                            msg += ua.Userid + "已经通过审核，无法再次审核. ";
                        }
                        else if (ua.State == 2)
                        {
                            msg += ua.Userid + "已撤销申请，无法再次审核. ";
                        }
                  
                }

                _res.Done(null, msg);

               
            }
            catch (Exception ex)
            {
                _res.Error("撤销异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Delete(JObject data)
        {

            string userid_admin =data["userid_admin"].ToString();

            try
            {
                string ids = data["ids"].ToString();
                string msg = "";
                List<DbRenzheng> rlist = _dbConnect.DbRenzheng.Where(r => EF.Functions.Like(ids, "%," + r.Id + ",%")).ToList();
                foreach (DbRenzheng ua in rlist)
                {

                    msg += ua.Userid + "删除成功. ";
                    _dbConnect.DbRenzheng.Remove(ua); 
                }
                _dbConnect.SaveChanges();
                _res.Done(null, msg);
            }
            catch (Exception ex)
            {
                _res.Error("删除异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Api.Method;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.MsgController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Msg_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public Msg_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 获取未读消息数量
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result GetNotReadMsgCount()
        {
            try
            {
               
                int count = _dbConnect.DbMsg.Where(m => m.Lx == 1 && m.Isread == 0 && m.Fid != 0).Count();
                _res.Done(count, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取未读消息数量异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  首次查询
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List_First(JObject data)
        {
                 
            try
            {
                string userid = data["userid"].ToString();

                
                List<DbMsg> Msg_list = _dbConnect.DbMsg.Where(m => m.Lx == 1 && (m.Fuserid.Equals(userid) || m.Suserid.Equals(userid) && m.Mdate > DateTime.Now.AddDays(-3))).ToList();
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();

                foreach (DbMsg msg in Msg_list)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "Id", msg.Id.ToString() },
                        { "Title", msg.Title },
                        { "Fid", msg.Fid.ToString() },
                        { "Sid", msg.Sid.ToString() },
                        { "Fuserid", msg.Fuserid },
                        { "Suserid", msg.Suserid },
                        { "Msgcontent", msg.Msgcontent },
                        { "Mdate", msg.Mdate.ToString() },
                        { "Lx", msg.Lx.ToString() },
                        { "Isread", msg.Isread.ToString() }
                    };
                    if (msg.Sid == 0 && msg.Isread == 0)
                    {
                        msg.Isread = 1;
                    }
                    diclist.Add(dic);
                }
                _dbConnect.SaveChanges();

                _res.Done(diclist, "查询成功");
            }

            catch (Exception ex)
            {
                _res.Error("查询消息异常");
            
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 监听未读消息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List_Watch(JObject data)
        {
                 

            try
            {
                string userid = data["userid"].ToString();

               
                List<DbMsg> Msg_list = _dbConnect.DbMsg.Where(m => m.Lx == 1 && m.Isread == 0 && m.Fuserid.Equals(userid)).ToList();

                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                foreach (DbMsg msg in Msg_list)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "Id", msg.Id.ToString() },
                        { "Title", msg.Title },
                        { "Fid", msg.Fid.ToString() },
                        { "Sid", msg.Sid.ToString() },
                        { "Fuserid", msg.Fuserid },
                        { "Suserid", msg.Suserid },
                        { "Msgcontent", msg.Msgcontent },
                        { "Mdate", msg.Mdate.ToString() },
                        { "Lx", msg.Lx.ToString() },
                        { "Isread", msg.Isread.ToString() }
                    };
                    if (msg.Sid == 0 && msg.Isread == 0)
                    {
                        msg.Isread = 1;
                    }
                    diclist.Add(dic);
                }
                _dbConnect.SaveChanges();

                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("监听未读消息异常");
           
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取聊天中的用户列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List_Chating()
        {
            
            
            try
            {
                
                UsersMethod um = new UsersMethod(_dbConnect);
                List<DbUsers> ulist = _dbConnect.DbUsers.Where(u => u.Msgdate != null).OrderByDescending(u => u.Msgdate).ToList();

                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                foreach (DbUsers us in ulist)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "Id", us.Id.ToString() },
                        { "Userid", us.Userid },
                        { "Username", us.Username },
                        { "Tx", us.Tx },
                        { "Msgdate", us.Msgdate.ToString() },
                        { "Noreadnum",_dbConnect.DbMsg.Where(m => m.Fid == us.Id && m.Lx == 1 && m.Isread == 0).ToList().Count.ToString() }
                    };
                    diclist.Add(dic);
                    
                }
                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取聊天中的用户列表异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List_User(JObject data)
        {
            
           
            try
            {
                string query_userid = data["query_userid"].ToString();
               
                List<DbUsers> ulist = _dbConnect.DbUsers.Where(u => u.Id > 0).ToList();
                if (query_userid != "")
                {
                    ulist = ulist.Where(u => u.Userid.Contains(query_userid) || u.Username.Contains(query_userid)).ToList();
                }
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                foreach (DbUsers us in ulist)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "Id", us.Id.ToString() },
                        { "Userid", us.Userid },
                        { "Username", us.Username },
                        { "Tx", us.Tx },
                        { "Usertel", us.Usertel.ToString() }
                    };
                    diclist.Add(dic);
                }
                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取用户列表异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

    }
}

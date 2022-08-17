using Microsoft.AspNetCore.Mvc;
using Server.Models;
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
    public class MsgController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public MsgController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 获取未读消息条数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result GetMsgCount(JObject data)
        {
            
            try
            {
                List<DbMsg> Msg_list = _dbConnect.DbMsg.Where(m => m.Suserid.Equals(data["userid"]) && m.Lx == Convert.ToInt32(data["lx"]) && m.Isread == 0 && m.Sisdelete == 0).ToList();
                _res.Done(Msg_list.Count.ToString(), "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }



        /// <summary>
        /// 首次打开消息面板
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result List_First(JObject data)
        {
            
            try
            {
                string userid = data["userid"].ToString();
                int lx = Convert.ToInt32(data["lx"]);  
                List<DbMsg> msglist = _dbConnect.DbMsg.Where(m => (m.Fuserid.Equals(userid) || m.Suserid.Equals(userid)) && m.Lx == lx && m.Sisdelete == 0).OrderByDescending(m => m.Mdate).ToList();
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                foreach (DbMsg msg in msglist)
                {
                    diclist.Add(MsgMethod.GetDic(msg));
                    //系统消息标为已读
                    if (msg.Fid == 0 && msg.Isread == 0)
                    {
                        msg.Isread = 1;
                    }
                }
                _dbConnect.SaveChanges();

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
        /// 监听未读消息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result List_Watch(JObject data)
        {
            
            try
            {
                List<DbMsg> Msg_list = _dbConnect.DbMsg.Where(m => m.Suserid.Equals(data["userid"]) && m.Isread == 0 && m.Sisdelete == 0).ToList();
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                foreach (DbMsg msg in Msg_list)
                {
                    diclist.Add(MsgMethod.GetDic(msg));
                    //客服消息标为已读
                    if (msg.Isread == 0 && msg.Lx == 1)
                    {
                        msg.Isread = 1;
                    }
                }
                _dbConnect.SaveChanges();
                _res.Done(diclist, "查询成功");

            }
            catch (Exception ex)
            {
                _res.Error("查询异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;


        }
    }
}

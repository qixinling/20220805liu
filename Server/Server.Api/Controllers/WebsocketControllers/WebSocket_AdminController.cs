using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Server.Utils.WebSocket_Utils;

namespace Server.Api.Controllers.WebsocketControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WebSocket_AdminController : ControllerBase
    {
        protected Rooms _rooms;
        private readonly Result _res;
        public WebSocket_AdminController(Rooms rooms,Result res)
        {
            _rooms = rooms;
            _res = res;
        }

        /// <summary>
        /// 获取端口号,服务还没开启时将启动
        /// </summary>
        /// <param name="userid_admin"></param>
        /// <param name="token_admin"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result GetPort()
        {
            
          
            try
            {
                WSMethod wsm = new WSMethod(_rooms);
                Dictionary<string, string> dic = new Dictionary<string, string>
                {
                    { "port", wsm.GetPort() }
                };
                _res.Done(dic, "查询成功");
                if (!wsm.GetLive()) { wsm.Start(); }
            }
            catch (Exception ex)
            {
                _res.Error("获取端口号异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}
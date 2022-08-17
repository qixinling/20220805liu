using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System;
using System.Collections.Generic;
using Server.Logs;
using Server.Utils.WebSocket_Utils;

namespace Server.Api.Controllers.WebsocketControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WebSocketController : ControllerBase
    {
        protected Rooms _rooms;
        private readonly Result _res;

        public WebSocketController(Rooms rooms,Result res)
        {
            _rooms = rooms;
            _res = res;
        }

        /// <summary>
        /// 获取端口号,服务还没开启时将启动
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result GetPort()
        {
            
            try
            {
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                WSMethod wsm = new WSMethod(_rooms);
                Dictionary<string, string> dic = new Dictionary<string, string>
                {
                    { "port", wsm.GetPort() }
                };
                diclist.Add(dic);
                _res.Done(diclist, "查询成功");
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
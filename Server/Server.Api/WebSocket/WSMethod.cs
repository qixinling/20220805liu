using Fleck;
using Newtonsoft.Json;
using Server.Models.DataBaseModels;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Server.Logs;
using Server.Utils.Configuration_Utils;

namespace Server.Utils.WebSocket_Utils
{
    public class WSMethod
    {
        protected Rooms _rooms;

        public WSMethod(Rooms rooms)
        {
            _rooms = rooms;
        }
        /// <summary>
        /// 获取服务存活状态
        /// </summary>
        /// <returns></returns>
        public bool GetLive()
        {
            return _rooms.GetLive();
        }

        /// <summary>
        /// 获取端口号
        /// </summary>
        /// <returns></returns>
        public string GetPort()
        {
            string Port = (Convert.ToInt32(ConfigUtils.Configuration["port"] + "1") - 11000).ToString();
            return Port;
        }

        /// <summary>
        /// 启动WebSocket服务
        /// </summary>
        public void Start()
        {
            try
            {
                List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();
                WebSocketServer server = new WebSocketServer("ws://0.0.0.0:" + GetPort())
                {
                    //出错后进行重启
                    RestartAfterListenError = true
                };

                server.Start(Socket =>
                {
                    //后端开启执行操作
                    Socket.OnOpen = () =>
                    {
                        Open(allSockets, Socket);
                    };
                    //接收前端发来的消息
                    Socket.OnMessage = DataMod =>
                    {
                        Chulixiaoxi(Socket, DataMod);

                    };
                    //后端关闭执行操作
                    Socket.OnClose = () =>
                    {
                        Close(allSockets, Socket);
                    };
                });
            }
            catch (Exception ex)
            {
                string Msg = "WebSocket启动错误";
                NLogHelper._.Error(Msg, ex);
            }
        }

        /// <summary>
        /// 对房间进行广播
        /// </summary>
        /// <param name="roomid">房间号</param>
        /// <param name="Jsonmsg">广播消息</param>
        public void Guangbo(string Roomid, string Jsonmsg)
        {
            try
            {
                Dictionary<string, List<IWebSocketConnection>> dicrooms = _rooms.GetRooms();
                if (dicrooms.ContainsKey(Roomid))
                {
                    List<IWebSocketConnection> Wslist = dicrooms[Roomid];
                    foreach (var Ws in Wslist)
                    {
                        Ws.Send(Jsonmsg);
                    }
                }
            }
            catch (Exception ex)
            {
                string Msg = "WebSocket广播出错";
                NLogHelper._.Error(Msg, ex);
            }
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="socket"></param>
        public void Chulixiaoxi(IWebSocketConnection socket, string DataMod)
        {
            try
            {
                WSMod mod = JsonConvert.DeserializeObject<WSMod>(DataMod);
                using DbConnect dbConnect = DbConnectUtils.GetDbContext();
                if (mod.Type == 0)//对话消息
                {
                    int[] ids = { mod.Fuid, mod.Suid };
                    foreach (int uid in ids)
                    {
                        DbUsers user = dbConnect.DbUsers.FirstOrDefault(u => u.Id == uid);
                        if (user != null) { user.Msgdate = DateTime.Now; }
                    }
                    dbConnect.DbMsg.Add(mod.MsgData);
                    if (dbConnect.SaveChanges() > 0)
                    {
                        Dictionary<string, List<IWebSocketConnection>> Dicrooms = _rooms.GetRooms();
                        List<IWebSocketConnection> Wslist = Dicrooms[mod.Rid];
                        IWebSocketConnection fus = socket;
                        IWebSocketConnection sus = null;
                        foreach (var Ws in Wslist)
                        {
                            NameValueCollection Values = GetParams(Ws.ConnectionInfo.Path);
                            string userid = Values[1];//用户名
                            if (userid.Equals(mod.Suserid))
                            {
                                sus = Ws;//找到接收人
                            }
                        }

                        //给发送人的反馈
                        string offMsg = "对方不在线,离线消息已发送";
                        if (sus != null)
                        {
                            if (sus.IsAvailable)
                            {
                                mod.Msg = "发送成功";
                            }
                            else
                            {
                                mod.Msg = offMsg;
                            }
                        }
                        else
                        {
                            mod.Msg = offMsg;
                        }
                        fus.Send(JsonConvert.SerializeObject(mod));

                        //接收人的反馈
                        if (sus != null)
                        {
                            mod.Msg = "成功接收" + mod.Fuserid + "的消息";
                            //#1
                            //后台客服当前存在一个状态IsAvailable为false的时候无法收到信息,所以需要去所有房间找到IsAvailable为true的客服连接发送
                            //干脆就判断是客服时暂时不发送消息，找到了IsAvailable为true的客服连接再发
                            if (!sus.ConnectionInfo.Path.Equals("/?roomid=1admin&userid=kefu"))
                            {
                                sus.Send(JsonConvert.SerializeObject(mod));
                            }
                        }

                        if (!socket.ConnectionInfo.Path.Equals("/?roomid=1admin&userid=kefu"))//发送人是客服时不用找
                        {
                            bool find = false;
                            //找出客服
                            foreach (var item in Dicrooms)
                            {
                                foreach (var us in item.Value)
                                {
                                    if (us.ConnectionInfo.Path.Equals("/?roomid=1admin&userid=kefu") && us.IsAvailable)
                                    {
                                        //#2
                                        //找到IsAvailable为true的客服发送消息
                                        us.Send(JsonConvert.SerializeObject(mod));
                                        //有消息时提醒后台的admin管理员
                                        mod.Type = 3;
                                        mod.Msg = "收到[" + mod.Fuserid + "]新信息";

                                        us.Send(JsonConvert.SerializeObject(mod));
                                        find = true;
                                    }
                                    if (find)
                                    {
                                        break;
                                    }
                                }
                                if (find)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (mod.Type == 1)//1心跳保持连接
                {
                    Dictionary<string, List<IWebSocketConnection>> Dicrooms = _rooms.GetRooms();
                    if (Dicrooms.ContainsKey(mod.Rid))
                    {
                        List<IWebSocketConnection> Wslist = Dicrooms[mod.Rid];
                        foreach (var Ws in Wslist)
                        {
                            mod.Msg = "成功续命";
                            Ws.Send(JsonConvert.SerializeObject(mod));
                        }
                    }
                }
                else if (mod.Type == 2)//切换房间
                {
                    string toRid = mod.Suid + mod.Suserid;//目标房间号
                    mod.Msg = _rooms.AddRoom(toRid, socket);
                    socket.Send(JsonConvert.SerializeObject(mod));
                }

            }
            catch (Exception ex)
            {
                string Msg = "处理WebSocket消息出错";
                NLogHelper._.Error(Msg, ex);
            }
        }

        /// <summary>
        /// 当有用户开启服务时
        /// </summary>
        /// <param name="allSockets"></param>
        /// <param name="socket"></param>
        public void Open(List<IWebSocketConnection> allSockets, IWebSocketConnection socket)
        {
            try
            {
                NameValueCollection Values = GetParams(socket.ConnectionInfo.Path);
                string Roomid = Values[0];
                _rooms.AddRoom(Roomid, socket);
                allSockets.Add(socket);
                WSMod mod = new WSMod
                {
                    Type = 1,
                    Msg = "开始心跳..."
                };
                socket.Send(JsonConvert.SerializeObject(mod));
            }
            catch (Exception ex)
            {
                string Msg = socket.ConnectionInfo.Path + "开启WebSocket服务出错";
                NLogHelper._.Error(Msg, ex);
            }
        }

        /// <summary>
        /// 当有用户关闭服务时
        /// </summary>
        /// <param name="allSockets"></param>
        /// <param name="socket"></param>
        public void Close(List<IWebSocketConnection> allSockets, IWebSocketConnection socket)
        {
            try
            {
                NameValueCollection values = GetParams(socket.ConnectionInfo.Path);
                string Roomid = values[0];
                _rooms.RemoveUser(Roomid, socket);
                allSockets.Remove(socket);
            }
            catch (Exception ex)
            {
                string Msg = socket.ConnectionInfo.Path + "关闭WebSocket服务出错";
                NLogHelper._.Error(Msg, ex);
            }
        }

        /// <summary>
        /// 获取房间数量
        /// </summary>
        /// <returns></returns>
        public int GetRoomNumber()
        {
            return _rooms.GetRoomNumber();
        }

        /// <summary>
        /// 提取参数
        /// </summary>
        /// <param name="url">包含参数的字符串</param>
        /// <returns></returns>
        public NameValueCollection GetParams(string Url)
        {
            int StartIndex = Url.IndexOf("?");
            NameValueCollection values = new NameValueCollection();
            if (StartIndex <= 0) { return values; }
            string[] NameValues = Url[(StartIndex + 1)..].Split('&');
            foreach (string s in NameValues)
            {
                string[] Pair = s.Split('=');
                string Name = Pair[0];
                string Value = string.Empty;
                if (Pair.Length > 1) { Value = Pair[1]; }
                values.Add(Name, Value);
            }
            return values;
        }
    }
}

using Fleck;
using System.Collections.Generic;

namespace Server.Utils.WebSocket_Utils
{
    public class Rooms
    {
        /// <summary>
        /// 服务存活状态
        /// </summary>
        bool Islive;

        /// <summary>
        /// 房间集合
        /// </summary>
        protected Dictionary<string, List<IWebSocketConnection>> roomdic;

        public Rooms()
        {
            //构造房间集合
            roomdic = new Dictionary<string, List<IWebSocketConnection>>();
            Islive = false;
        }

        /// <summary>
        /// 获取服务存活状态
        /// </summary>
        /// <returns></returns>
        public bool GetLive()
        {
            return Islive;
        }

        /// <summary>
        /// 添加房间
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="username"></param>
        public string AddRoom(string RoomId, IWebSocketConnection username)
        {
            string result = "进入房间失败";
            //判断该房号的房间是否存在
            if (this.roomdic.ContainsKey(RoomId))
            {
                //存在时,获得房间用户
                List<IWebSocketConnection> userlist = this.roomdic[RoomId];
                //把新用户添加进房间中
                foreach (var user in userlist)
                {
                    if (user.ConnectionInfo.Path.Equals(username.ConnectionInfo.Path))
                    {
                        result = "您已在" + RoomId + "房间中";
                        return result;
                    }
                }
                userlist.Add(username);
                this.roomdic[RoomId] = userlist;
                result = "成功进入" + RoomId;
            }
            else
            {
                //房间不存在时,创建房间
                List<IWebSocketConnection> userlist = new List<IWebSocketConnection>
                {
                    username
                };
                this.roomdic.Add(RoomId, userlist);
                result = "成功创建" + RoomId;
            }
            Islive = true;
            return result;
        }

        /// <summary>
        /// 获取所有房间
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<IWebSocketConnection>> GetRooms()
        {
            return this.roomdic;
        }

        /// <summary>
        /// 获取房间数量
        /// </summary>
        /// <returns></returns>
        public int GetRoomNumber()
        {
            return this.roomdic.Count;
        }

        /// <summary>
        /// 根据房号获得房间里的用户
        /// </summary>
        /// <param name="roomId">房号</param>
        /// <returns></returns>
        public List<IWebSocketConnection> GetUsers(string RoomId)
        {
            return this.roomdic[RoomId];
        }

        /// <summary>
        /// 移除房间里断开的用户
        /// </summary>
        /// <param name="roomId">房间号</param>
        /// <param name="username">用户</param>
        public void RemoveUser(string RoomId, IWebSocketConnection username)
        {
            //判断该房号的房间是否存在
            if (this.roomdic.ContainsKey(RoomId))
            {
                //存在时,获得房间用户
                List<IWebSocketConnection> userlist = this.roomdic[RoomId];
                //把用户从房间中移除
                userlist.Remove(username);
                if (userlist.Count > 0)
                {
                    //移除当前用户后,如果房间内还有人,更新房间
                    this.roomdic[RoomId] = userlist;
                }
                else
                {
                    //房间没人时,移除房间
                    this.RemoveRoom(RoomId);
                }
            }
        }

        /// <summary>
        /// 移除房间
        /// </summary>
        /// <param name="roomId">房间号</param>
        public void RemoveRoom(string RoomId)
        {
            this.roomdic.Remove(RoomId);
        }
    }
}

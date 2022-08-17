using Server.Models.DataBaseModels;

namespace Server.Utils.WebSocket_Utils
{
    public class WSMod
    {
        public WSMod()
        {
            ActiveTime = 60000;
            Type = 0;
            Msg = "消息发送失败";
        }
        public int Fuid { get; set; }//发送人uid
        public string Fuserid { get; set; }//发送人userid
        public int Suid { get; set; }//接收人uid
        public string Suserid { get; set; }//接收人userid
        public string Rid { get; set; }//房间号
        public int Type { get; set; }//类型：0对话消息 1心跳保持连接 2切换房间 3未读消息通知(客服)
        public int ActiveTime { get; set; }//活跃时间
        public string Msg { get; set; }//通知消息
        public DbMsg MsgData { get; set; }//对话消息
    }
}

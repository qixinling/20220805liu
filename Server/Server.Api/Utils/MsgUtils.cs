using Server.Models.DataBaseModels;
using Server.Models;
using System;
using System.Reflection;
using Server.Logs;

namespace Server.Utils.Msg_Utils
{
    public static class MsgUtils
    {
        /// <summary>
        /// 发送消息通用方法
        /// </summary>
        /// <param name="lx">类型</param>
        /// <param name="title">标题</param>
        /// <param name="msgcontent">内容</param>
        /// <param name="fid">发送消息的id</param>
        /// <param name="fuserid">发送消息的userid</param>
        /// <param name="sid">接收消息的id</param>
        /// <param name="suserid">接收消息的userid</param>
        /// <returns></returns>
        public static int Send(int Lx, string Title, string Msgcontent, int Fid, string Fuserid, int Sid, string Suserid)
        {
            int Res = 0;
            try
            {
                using DbConnect dbConnect = DbConnectUtils.GetDbContext();
                DbMsg msg = new DbMsg
                {
                    Fid = Fid,
                    Fuserid = Fuserid,
                    Lx = Lx,
                    Title = Title,
                    Msgcontent = Msgcontent,
                    Mdate = DateTime.Now,
                    Sid = Sid,
                    Suserid = Suserid,
                    Isread = 0,
                    Sisdelete = 0
                };
                dbConnect.DbMsg.Add(msg);
                Res = dbConnect.SaveChanges();
            }
            catch (Exception ex)
            {
                NLogHelper._.Error("发送消息出错", ex);
            }
            return Res;
        }
    }
}
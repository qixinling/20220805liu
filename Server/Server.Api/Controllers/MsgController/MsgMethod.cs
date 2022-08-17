using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Controllers.MsgController
{
    public static class MsgMethod
    {
        /// <summary>
        /// 创建键值对
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDic(DbMsg msg)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "Id", msg.Id.ToString() },
                { "Fid", msg.Fid.ToString() },
                { "Fuserid", msg.Fuserid },
                { "Title", msg.Title },
                { "Msgcontent", msg.Msgcontent },
                { "Sid", msg.Sid.ToString() },
                { "Suserid", msg.Suserid },
                { "Mdate", msg.Mdate.ToString() },
                { "Lx", msg.Lx.ToString() },
                { "Isread", msg.Isread.ToString() }
            };
            return dic;
        }
    }
}

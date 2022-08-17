using Server.Models.DataBaseModels;
using System.Collections.Generic;
using System.Web;

namespace Server.Api.Controllers.HelpController
{
    public static class HelpUtils
    {
        /// <summary>
        /// 创建键值对
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDic(DbHelp h)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "id", h.Id.ToString() },
                { "zan", h.Zan.ToString() },
                { "cai", h.Cai.ToString() },
                { "question", h.Question },
                { "answer", HttpUtility.HtmlDecode(h.Answer) },
                { "show", h.Show.ToString() },
                { "hpath", h.Hpath.ToString() },
                { "hlevel", h.Hlevel.ToString() },
                { "gpath", h.Gpath.ToString() },
                { "rank", h.Rank.ToString() }
            };
            return dic;
        }
    }
}

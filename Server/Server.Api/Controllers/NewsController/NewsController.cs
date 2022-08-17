using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Server.Logs;
using Newtonsoft.Json.Linq;
using Server.Api.Method;

namespace Server.Api.Controllers.NewsController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NewsController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public NewsController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询所有新闻
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result List()
        {
            try
            {
                var list = _dbConnect.DbNews.Where(n => n.Display == 1).OrderByDescending(m => m.Id).Select(n => new
                {
                    id=n.Id,
                    news_cover=n.Newscover,
                    news_title=n.Newstitle,
                    news_operator=n.Newsoperator,
                    news_content= HttpUtility.HtmlDecode(n.Newscontent),
                    news_time=n.Newstime,
                    clicks=n.Clicks
                });
                _res.Done(list, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询新闻异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询单个新闻
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Get(JObject data)
        {
            try
            {
                int select_id = Convert.ToInt32(data["select_id"]);
                NewsMethod met = new NewsMethod(_dbConnect);
                DbNews news = met.GetById(select_id);
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                if (news != null)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "id", news.Id.ToString() },
                        { "news_cover", news.Newscover },
                        { "news_title", news.Newstitle },
                        { "news_operator", news.Newsoperator },
                        { "news_content", HttpUtility.HtmlDecode(news.Newscontent) },
                        { "news_time", news.Newstime.ToString() },
                        { "clicks", news.Clicks.ToString() }
                    };
                    diclist.Add(dic);

                    news.Clicks += 1;
                    _dbConnect.SaveChanges();
                    _res.Done(diclist, "查询成功");

                }
                else
                {

                    _res.Fail("新闻获取失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("查询新闻异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result Gonggao()
        {
            try
            {
                _res.Done(new SystemSettingMethod(_dbConnect).Get().Marqueemsg, "查询成功");

            }
            catch (Exception ex)
            {
                _res.Error("查询公告异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;

        }

    }
}

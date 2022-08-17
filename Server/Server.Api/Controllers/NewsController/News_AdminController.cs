using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Http_Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.NewsController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class News_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public News_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        public string[] displayNameArr { get; set; } = new[] { "未发布", "已发布" };

        /// <summary>
        /// 修改新闻
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Update(JObject data)
        {
            try
            {
                string news_cover = data["news_cover"].ToString();
                string news_title = data["news_title"].ToString();
                string news_content = data["news_content"].ToString();
                string userid_admin = data["userid_admin"].ToString();
                int display = Convert.ToInt32(data["display"]);
                int id = Convert.ToInt32(data["id"]);
                NewsMethod nsm = new NewsMethod(_dbConnect);
                DbNews ns = nsm.GetById(id);
                if (ns != null)
                {
                    ns.Newscover = news_cover;
                    ns.Newstitle = news_title;
                    ns.Display = display;
                    ns.Newscontent = news_content;
                    if (_dbConnect.SaveChanges() > 0)
                    {
                        _res.Done(null, "修改成功");
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 11, "修改新闻:" + news_title);
                    }
                    else
                    {
                        _res.Fail("无修改");
                    }
                }
                else
                {
                    _res.Fail("不存在该新闻");
                    ;
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改新闻异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询所有新闻
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List()
        {
            
           
            try
            {
                NewsMethod nsm = new NewsMethod(_dbConnect);
                var newslist = nsm.GetList().OrderByDescending(n => n.Id).Select(n => new
                {
                    id=n.Id,
                    news_cover=n.Newscover,
                    news_title=n.Newstitle,
                    news_operator=n.Newsoperator,
                    news_content=n.Newscontent,
                    news_time=DateTime.Parse(n.Newstime.ToString()).ToString("yyyy-MM-dd"),
                    display=n.Display,
                    displayname= displayNameArr[Convert.ToInt32(n.Display)],
                });
                _res.Done(newslist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询所有新闻信息异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询单条新闻
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {
            
            
            try
            {
                int id = Convert.ToInt32(data["id"]);
                NewsMethod nsm = new NewsMethod(_dbConnect);
                DbNews ns =nsm.GetById(id);
                if (ns != null)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "id", ns.Id.ToString() },
                        { "news_cover", ns.Newscover },
                        { "news_title", ns.Newstitle },
                        { "news_content", HttpUtility.HtmlDecode(ns.Newscontent) },
                        { "display", ns.Display.ToString() }
                    };
                    _res.Done(dic, "查询成功");
                }
                else
                {
                    _res.Fail("不存在该新闻");
                    
                }
            }
            catch (Exception ex)
            {
                _res.Error("查询单条新闻异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  删除新闻
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Delete(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                string delete_id = data["delete_id"].ToString();

                string[] Dllist = delete_id.Split(',');
                List<string> Cname = new List<string>();

                NewsMethod nsm = new NewsMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbNews news =nsm.GetById (Id);
                    if (news != null)
                    {
                        Cname.Add(news.Newstitle);
                       nsm.Remove(Id);
                    }
                }
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "删除完成");
  
                    foreach (string name in Cname)
                    {
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 11, "删除新闻:" + name);
                    }
                }
                else
                {
                    _res.Fail("删除失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("删除新闻异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Add(JObject data)
        {
            
            
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                string news_cover = data["news_cover"].ToString();
                string news_title = data["news_title"].ToString();
                int display =Convert.ToInt32(data["display"]);
                string news_content = data["news_content"].ToString();

                NewsMethod nm = new NewsMethod(_dbConnect);
                nm.Add(new DbNews
                {
                    Newscover = news_cover,
                    Newstitle = news_title,
                    Display = display,
                    Newsoperator = userid_admin,
                    Newscontent = news_content,
                    Newstime = DateTime.Now,
                    Clicks = 0
                });
                
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "发布完成");
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 11, "发布新闻:" + news_title);
                }
                else
                {
                    _res.Fail("添加失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("添加新闻异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

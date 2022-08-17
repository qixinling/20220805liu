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
using System.Text.Json;
using Newtonsoft.Json;

namespace Server.Api.Controllers.Article_AdminController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Article_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public Article_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询文章
        /// </summary>
        /// <param name="data">文章id</param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {
            try
            {
                ArticleMethod am = new ArticleMethod(_dbConnect);
                DbArticle article = am.GetById(Convert.ToInt32(data["id"]));
                if (article == null) { _res.Fail("文章不存在"); }
                _res.Done(article, "");
            }
            catch (Exception ex)
            {
                _res.Error("查询文章异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 修改文章
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
                string useridAdmin = data["userid_admin"].ToString();

                string articleTitle = data["article_title"].ToString();
                string articleContent = data["article_content"].ToString();
                

                ArticleMethod am = new ArticleMethod(_dbConnect);
                DbArticle article = am.GetById(Convert.ToInt32(data["id"]));
                if (article == null) { _res.Fail("文章不存在"); }

                article.Articletitle = articleTitle;
                article.Articlecontent = articleContent;
                _dbConnect.SaveChanges();

                if (article == null) { _res.Fail("文章不存在"); }
                _res.Done(null, "修改成功");
                SystemLogMethod.Add(useridAdmin, HttpInfoUtils.GetIP(), 11, "修改文章:" + articleTitle);
            }
            catch (Exception ex)
            {
                _res.Error("修改文章异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

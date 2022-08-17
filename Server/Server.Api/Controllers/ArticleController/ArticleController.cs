using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using Server.Logs;
using Newtonsoft.Json.Linq;
using Server.Api.Method;

namespace Server.Api.Controllers.Article_AdminController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ArticleController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public ArticleController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询文章
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result Get(JObject data)
        {
          
            try
            {
                ArticleMethod am = new ArticleMethod(_dbConnect);
                DbArticle article = am.GetById(Convert.ToInt32(data["select_id"]));
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
    }
}

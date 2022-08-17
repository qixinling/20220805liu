using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Api.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.HelpController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HelpController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public HelpController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 获取自动问答
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result List(JObject data)
        {
            
            try
            {
                HelpMethod hm = new HelpMethod(_dbConnect);
                var hlist= hm.GetList().Where(h => h.Id > 0 && h.Show == 1).Select(h=>new 
                { 
                    id=h.Id,
                    zan=h.Zan,
                    cai=h.Cai,
                    question=h.Question,
                    answer= HttpUtility.HtmlDecode(h.Answer),
                    show=h.Show,
                    hpath=h.Hpath,
                    hlevel=h.Hlevel
                });

                _res.Done(hlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询bonus异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

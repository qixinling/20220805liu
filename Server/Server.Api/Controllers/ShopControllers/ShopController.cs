using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using Server.Api.Method;

namespace Server.Api.Controllers.ShopControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShopController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public ShopController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 商城首页展示图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result Img()
        {
            try
            {
                ShopImgMethod sim = new ShopImgMethod(_dbConnect);
                List<DbShopImg> silist = sim.GetList();
                _res.Done(silist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询商品首页展示图片异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;


        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.ShopControllers.GoodsController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShopGoodsController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public ShopGoodsController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 获取所有商品和各项字段信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result List(JObject data)
        {

            try
            {
                int goodstype = Convert.ToInt32(data["goodstype"]);

                List<DbShopGoods> gslist = _dbConnect.DbShopGoods.Where(g => g.Ispay == 1).OrderByDescending(m => m.Sort).ToList();
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                if (goodstype != 99)
                {
                    gslist = gslist.Where(g => g.Goodstype == goodstype).ToList();
                }
                foreach (DbShopGoods gs in gslist)
                {
                    Dictionary<string, string> dic = ShopGoodsMethod.GetGoodsInfo(gs);

                    diclist.Add(dic);
                }
                _res.Done(diclist, "查询成功");

            }
            catch (Exception ex)
            {
                _res.Error("查询异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取单个商品的各项字段信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Get(JObject data)
        {

            try
            {
                int id = Convert.ToInt32(data["id"]);
                DbShopGoods gs = _dbConnect.DbShopGoods.FirstOrDefault(g => g.Id == id && g.Ispay == 1);
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();

                if (gs == null) { _res.Fail("商品已下架"); return _res; }

                Dictionary<string, string> dic = ShopGoodsMethod.GetGoodsInfo(gs);
                diclist.Add(dic);


                _res.Done(diclist, "查询成功");

            }
            catch (Exception ex)
            {
                _res.Error("查询商品详细信息异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;

        }

        /// <summary>
        /// 获取大小类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result Daxiaolei_List()
        {
            try
            {
                List<DbShopGoodsSort> list = _dbConnect.DbShopGoodsSort.Include(s => s.DbShopGoodsSortChild).Where(m => m.Id != 1 && m.Putaway == 1).ToList();
                foreach (DbShopGoodsSort item in list)
                {
                    var temp = item.DbShopGoodsSortChild;
                    item.DbShopGoodsSortChild = temp.Where(t => t.Sid != 1 && t.Putaway == 1).ToList();
                }
                _res.Done(list, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;

        }


    }
}

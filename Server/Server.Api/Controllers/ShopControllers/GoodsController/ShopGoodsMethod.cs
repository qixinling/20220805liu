using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Server.Api.Controllers.ShopControllers.GoodsController
{
    public static class ShopGoodsMethod
    {
        /// <summary>
        /// 通用获取商品信息方法
        /// 注意:不是接口
        /// </summary>
        /// <param name="gs"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetGoodsInfo(DbShopGoods gs)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "id", gs.Id.ToString() },
                { "goodsname", gs.Goodsname },
                { "goodstype", gs.Goodstype.ToString() },
                { "goodspv", gs.Goodspv.ToString() },
                { "stock", gs.Stock.ToString() },
                { "sales", gs.Sales.ToString() },
                { "goodsimg", gs.Goodsimg },
                { "dlid", gs.Dlid.ToString() },
                { "dlname", gs.Dlname },
                { "xlid", gs.Xlid.ToString() },
                { "xlname", gs.Xlname },
                { "goodscontent", HttpUtility.HtmlDecode(gs.Goodscontent) },
                { "goodslabel", gs.Goodslabel },
                { "cost", gs.Cost.ToString() },
                { "prompt", gs.Prompt },
                { "sort", gs.Sort.ToString() },
                { "ispay", gs.Ispay.ToString() },
                { "ishome", gs.Ishome.ToString() },
                { "sjine", gs.Goodsprice.ToString() },
                { "yjine", gs.Goodsprice.ToString() }
            };

            return dic;
        }
    }
}

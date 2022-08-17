using Server.Models.DataBaseModels;
using System.Collections.Generic;
using System.Linq;

namespace Server.Api.Controllers.ShopControllers.GoodsController
{
    public static class ShopGoodsSortMethod
    {
        /// <summary>
        /// 获取大类下商品数量
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="_dbConnect"></param>
        /// <returns></returns>
        public static int GetGoodsNum(int Id,DbConnect _dbConnect)
        {
            int Num = 0;
            List<DbShopGoodsSortChild> sortChildList = _dbConnect.DbShopGoodsSortChild.Where(s => s.Sid == Id).ToList();
            foreach (DbShopGoodsSortChild child in sortChildList)
            {
                Num += _dbConnect.DbShopGoods.Where(g => g.Xlid == child.Id).Count();
            }
            return Num;
        }

        /// <summary>
        /// 获取显示类型
        /// </summary>
        /// <param name="Lx"></param>
        /// <returns></returns>
        public static string Getpagemarklx(int Lx)
        {
            string Lxname = "未知类型";
            if (Lx == 1)
            {
                Lxname = "显示图片";
            }
            else if (Lx == 2)
            {
                Lxname = "显示名称";

            }
            else if (Lx == 3)
            {
                Lxname = "只显示商品";
            }
            return Lxname;
        }
    }
}

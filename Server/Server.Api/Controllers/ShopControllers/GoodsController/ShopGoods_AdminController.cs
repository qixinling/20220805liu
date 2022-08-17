using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Http_Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Api.Utils.Public;

namespace Server.Api.Controllers.ShopControllers.GoodsControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShopGoods_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public ShopGoods_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 添加商品
        /// </summary>
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
                string goods = data["goods"].ToString();

                DbShopGoods shopgoods = JsonConvert.DeserializeObject<DbShopGoods>(goods);

                ShopGoodsSortChildMethod sgm = new ShopGoodsSortChildMethod(_dbConnect);
                DbShopGoodsSortChild xlinfo =sgm.GetById(shopgoods.Xlid);
                if (xlinfo == null) { _res.Fail("请选择分类"); return _res; }

                ShopGoodsSortMethod sgsm = new ShopGoodsSortMethod(_dbConnect);
                DbShopGoodsSort dalei = sgsm.GetById( Convert.ToInt32 (xlinfo.Sid));
                if (dalei == null) { _res.Fail("商品大类不存在"); return _res; }
                shopgoods.Dlid = dalei.Id;
                shopgoods.Dlname = dalei.Daleiname;
                shopgoods.Xlname = xlinfo.Xiaoleiname;

                ShopGoodsMethod sm = new ShopGoodsMethod(_dbConnect);
                sm.Add(shopgoods);

                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "添加成功");
                
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 7, "添加商品:" + shopgoods.Goodsname);
                }
                else
                {
                    _res.Fail("添加失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("添加商品异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改商品
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
                string userid_admin = data["userid_admin"].ToString();
                string goods = data["goods"].ToString();
                DbShopGoods shopgoods = JsonConvert.DeserializeObject<DbShopGoods>(goods);

                ShopGoodsMethod sm = new ShopGoodsMethod(_dbConnect);
                DbShopGoods inGoods =sm.GetById(shopgoods.Id);
                if (inGoods == null) { _res.Fail("商品不存在"); return _res; }

                ModUtils.ObjUpdateObj<DbShopGoods, DbShopGoods>(shopgoods, inGoods);

                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "修改成功");
                
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 7, "修改商品:" + inGoods.Goodsname);
                }
                else
                {
                    _res.Fail("修改失败");
              
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改商品异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除商品
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
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    ShopGoodsMethod sm = new ShopGoodsMethod(_dbConnect);
                    DbShopGoods gs =sm.GetById(Id);
                    if (gs != null)
                    {
                        string Goodsname = gs.Goodsname;
                        sm.Remove(Id);

                        string[] Imgs = gs.Goodsimg.Split(",");

                        if (_dbConnect.SaveChanges() > 0)
                        {
                            //删除商品时,连带删除商品图片
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload");
                            DirectoryInfo folder = new DirectoryInfo(path);
                            foreach (string Imgname in Imgs)
                            {
                                foreach (FileInfo fileinfo in folder.GetFiles(Imgname))
                                {
                                    fileinfo.Delete();
                                }
                            }
                            SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 7, "删除商品:" + Goodsname);
                        }
                    }
                }
                _res.Done(null, "删除成功");
      
            }
            catch (Exception ex)
            {
                _res.Error("删除商品异常");
           
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询单个商品信息
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
                int gid = Convert.ToInt32(data["gid"]);
                ShopGoodsMethod sm = new ShopGoodsMethod(_dbConnect);
                DbShopGoods goods = sm.GetById(gid);
                if (goods == null) { _res.Fail("商品信息出错"); return _res; }
                Dictionary<string, string> dic = new Dictionary<string, string>
                {
                    { "data", JsonConvert.SerializeObject(goods) }
                };
                _res.Done(dic, "查询成功");
            }

            catch (Exception ex)
            {
                _res.Error("查询单个商品信息异常");

                NLogHelper._.Error(_res.Msg, ex);
            }

            return _res;
        }

        /// <summary>
        /// 查询全部商品
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
                List<DbShopGoods> Goods_list = new ShopGoodsMethod(_dbConnect).GetList().OrderByDescending(m => m.Sort).ToList();
                _res.Done(Goods_list, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询全部商品异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

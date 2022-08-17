using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Http_Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.ShopControllers.GoodsControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShopGoodsSortChild_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public ShopGoodsSortChild_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 是否展示操作
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result ChangePagemark(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                int id = Convert.ToInt32(data["id"]);

                ShopGoodsSortChildMethod sgsm = new ShopGoodsSortChildMethod(_dbConnect);
                DbShopGoodsSortChild sort = sgsm.GetById(id);
                if (sort == null) { _res.Fail("数据不存在"); return _res; }
                string Name = "展示";
                if (sort.Pagemark == 0) { sort.Pagemark = 1; }
                else if (sort.Pagemark == 1) { sort.Pagemark = 0; Name = "不展示"; }
                if (_dbConnect.SaveChanges() > 0)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 7, "修改商品小类" + sort.Xiaoleiname + Name);
                    _res.Done(null, "修改已保存");
                    return _res;
                }
            }
            catch (Exception ex)
            {
                _res.Error("是否展示操作异常");
           
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 上下架操作
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result ChangePutaway(JObject data)
        {

            try
            {
                string userid_admin = data["userid_admin"].ToString();
                int id = Convert.ToInt32(data["id"]);
                ShopGoodsSortChildMethod sgsm = new ShopGoodsSortChildMethod(_dbConnect);
                DbShopGoodsSortChild sort =sgsm.GetById(id);
                if (sort == null) { _res.Fail("数据不存在"); return _res; }
                string Name = "上架";
                if (sort.Putaway == 0) { sort.Putaway = 1; }
                else if (sort.Putaway == 1) { sort.Putaway = 0; Name = "下架"; }
                if (_dbConnect.SaveChanges() > 0)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 7, "商品小类上下架:" + sort.Xiaoleiname + Name);
                    _res.Done(null, "修改已保存");
                  
                    return _res;
                }
            }
            catch (Exception ex)
            {
                _res.Error("上下架操作异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 添加小类
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
                string xiaoleiname = data["xiaoleiname"].ToString();
                string xiaoleiimg = data["xiaoleiimg"].ToString();
              
                int xiaoleiorder = Convert.ToInt32(data["xiaoleiorder"]);
                int pagemark = Convert.ToInt32(data["pagemark"]);
                int putaway = Convert.ToInt32(data["putaway"]);
                int id = Convert.ToInt32(data["id"]);

                DbShopGoodsSortChild sortChild = new DbShopGoodsSortChild
                {
                    Sid = id,
                    Xiaoleiname = xiaoleiname,
                    Xiaoleiorder = xiaoleiorder,
                    Pagemark = pagemark,
                    Xiaoleiimg = xiaoleiimg,
                    Adate = DateTime.Now,
                    Putaway = putaway
                };
                ShopGoodsSortChildMethod sgsm = new ShopGoodsSortChildMethod(_dbConnect);
                sgsm.Add(sortChild);

                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "添加成功");
                
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 7, "添加小类:" +xiaoleiname);
                }
                else
                {
                    _res.Fail("添加失败");
                    
                }
            }
            catch (Exception ex)
            {
                _res.Error("添加小类异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


      /// <summary>
      /// 
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
                string ids = data["ids"].ToString();
                string[] Idlist = ids.Split(',');
                List<string> Cname = new List<string>();
                ShopGoodsSortChildMethod sgsm = new ShopGoodsSortChildMethod(_dbConnect);
                ShopGoodsMethod sgm = new ShopGoodsMethod(_dbConnect);
                foreach (string Id in Idlist)
                {
                    int Gid = Convert.ToInt32(Id);
                    if (Gid == 1) { _res.Fail("不能删除未分类项"); return _res; }

                    DbShopGoodsSortChild sortChild =sgsm.GetById( Gid);
                    if (sortChild == null) { continue; }
                    Cname.Add(sortChild.Xiaoleiname);
                   sgsm.Remove(Gid);
                    List<DbShopGoods> goodslist =sgm.GetList().Where(n => n.Xlid == Gid).ToList();
                    foreach (DbShopGoods goods in goodslist)
                    {
                        goods.Dlid = 1;
                        goods.Xlid = 1;
                    }
                }
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "删除成功");
                   
                    foreach (string name in Cname)
                    {
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 7, "删除小类:" + name);
                    }
                }
                else
                {
                    _res.Fail("删除失败");
                  
                }
            }
            catch (Exception ex)
            {
                _res.Error("删除小类异常");
             
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询单个小类
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
                ShopGoodsSortChildMethod sgsm = new ShopGoodsSortChildMethod(_dbConnect);
                DbShopGoodsSortChild sortChild =sgsm.GetById(id);
                if (sortChild == null) { _res.Fail("数据不存在");  return _res; }

                Dictionary<string, string> dic = new Dictionary<string, string>
                {
                    { "id", sortChild.Id.ToString() },
                    { "sid", sortChild.Sid.ToString() },
                    { "xiaoleiname", sortChild.Xiaoleiname },
                    { "xiaoleiorder", sortChild.Xiaoleiorder.ToString() },
                    { "pagemark", sortChild.Pagemark.ToString() },
                    { "xiaoleiimg", sortChild.Xiaoleiimg },
                    { "adata", sortChild.Adate.ToString() },
                    { "putaway", sortChild.Putaway.ToString() }
                };
                _res.Done(dic, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询单个小类异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询全部小类
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
                List<DbShopGoodsSortChild> childList = _dbConnect.DbShopGoodsSortChild.OrderBy(m => m.Xiaoleiorder).ToList(); 
                _res.Done(childList, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询全部小类异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询所有小类名称
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [SignCheckFilters]
        public Result List_Name()
        {
            
            try
            {
                _res.Done(new ShopGoodsSortChildMethod(_dbConnect).GetList(), "查询成功");
        
            }
            catch (Exception ex)
            {
                _res.Error("查询所有小类名称异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改小类
        /// </summary>
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
                string xiaoleiname = data["xiaoleiname"].ToString();
                string xiaoleiimg = data["xiaoleiimg"].ToString();
                int sid = Convert.ToInt32(data["sid"]);
                int xiaoleiorder = Convert.ToInt32(data["xiaoleiorder"]);
                int pagemark = Convert.ToInt32(data["pagemark"]);
                int putaway = Convert.ToInt32(data["putaway"]);
                int id = Convert.ToInt32(data["id"]);
                ShopGoodsSortChildMethod sgsm = new ShopGoodsSortChildMethod(_dbConnect);
                DbShopGoodsSortChild sortChild =sgsm.GetById(id);
                if (sortChild == null) { _res.Fail("修改失败"); return _res; }

                sortChild.Sid = sid;
                sortChild.Xiaoleiname = xiaoleiname;
                sortChild.Xiaoleiorder = xiaoleiorder;
                sortChild.Pagemark = pagemark;
                sortChild.Xiaoleiimg = xiaoleiimg;
                sortChild.Putaway = putaway;

                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "修改成功");
                   
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 11, "修改小类:" + xiaoleiname);
                }
                else
                {
                    _res.Fail("修改失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改小类异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

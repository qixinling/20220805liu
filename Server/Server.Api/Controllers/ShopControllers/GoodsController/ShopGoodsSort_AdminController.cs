using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;

using Server.Utils.Http_Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.ShopControllers.GoodsController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShopGoodsSort_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public ShopGoodsSort_AdminController(DbConnect dbConnect, Result res)
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

                Server.Api.Method.ShopGoodsSortMethod sgsm = new Server.Api.Method.ShopGoodsSortMethod(_dbConnect);
                DbShopGoodsSort sort = sgsm.GetById(id);
                if (sort == null) { _res.Fail("数据不存在"); return _res; }
                string Name = "展示";
                if (sort.Pagemark == 0) { sort.Pagemark = 1; }
                else if (sort.Pagemark == 1) { sort.Pagemark = 0; Name = "不展示"; }
                if (_dbConnect.SaveChanges() > 0)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 7, "商品大类" + sort.Daleiname + Name);
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

                Server.Api.Method.ShopGoodsSortMethod sgsm = new Server.Api.Method.ShopGoodsSortMethod(_dbConnect);
                DbShopGoodsSort sort = sgsm.GetById(id);

                if (sort == null) { _res.Fail("数据不存在"); return _res; }
                string Name = "上架";
                if (sort.Putaway == 0) { sort.Putaway = 1; }
                else if (sort.Putaway == 1) { sort.Putaway = 0; Name = "下架"; }
                if (_dbConnect.SaveChanges() > 0)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 7, "商品上下架:" + sort.Daleiname + Name);

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
        /// 添加大类
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

                string daleiname = data["daleiname"].ToString();
                int daleiorder = Convert.ToInt32(data["daleiorder"]);
                string daleiimg = data["daleiimg"].ToString();
                int putaway = Convert.ToInt32(data["putaway"]);
                int pagemark = Convert.ToInt32(data["pagemark"]);
                int pagemarklx = Convert.ToInt32(data["pagemarklx"]);

                DbShopGoodsSort gs = new DbShopGoodsSort
                {
                    Daleiname = daleiname,
                    Daleiorder = daleiorder,
                    Putaway = putaway,
                    Pagemark = pagemark,
                    Pagemarklx = pagemarklx,
                    Daleiimg = daleiimg,
                    Adate = DateTime.Now
                };
                Server.Api.Method.ShopGoodsSortMethod sgsm = new Server.Api.Method.ShopGoodsSortMethod(_dbConnect);
                sgsm.Add(gs);

                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "添加成功");

                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 7, "添加大类:" + daleiname);
                }
                else
                {
                    _res.Fail("添加失败");

                }
            }
            catch (Exception ex)
            {
                _res.Error("添加商品大类异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  删除大类
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
                foreach (string Id in Idlist)
                {
                    int Gid = Convert.ToInt32(Id);
                    if (Gid == 1) { _res.Fail("不能删除未分类项"); return _res; }

                    Server.Api.Method.ShopGoodsSortMethod sgsm = new Server.Api.Method.ShopGoodsSortMethod(_dbConnect);
                    DbShopGoodsSort sort = sgsm.GetById(Gid);
                    if (sort == null) { continue; }
                    Cname.Add(sort.Daleiname);
                    sgsm.Remove(Gid);

                    ShopGoodsSortChildMethod sgscm = new ShopGoodsSortChildMethod(_dbConnect);
                    List<DbShopGoodsSortChild> sortChildList = sgscm.GetSidList(Gid);
                    foreach (DbShopGoodsSortChild sortChild in sortChildList)
                    {
                        sortChild.Sid = 1;
                    }
                }
                if (_dbConnect.SaveChanges() > 0)
                {

                    _res.Done(null, "删除完成");

                    foreach (string name in Cname)
                    {
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 7, "删除大类:" + name);
                    }
                }
                else
                {
                    _res.Fail("删除失败");

                }
            }
            catch (Exception ex)
            {
                _res.Error("删除商品大类异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询单个大类
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
                Server.Api.Method.ShopGoodsSortMethod sgsm = new Server.Api.Method.ShopGoodsSortMethod(_dbConnect);
                DbShopGoodsSort sort = sgsm.GetById(id);
                if (sort == null) { _res.Fail("数据不存在"); return _res; }

                Dictionary<string, string> dic = new Dictionary<string, string>
                {
                    { "id", sort.Id.ToString() },
                    { "daleiname", sort.Daleiname },
                    { "daleiorder", sort.Daleiorder.ToString() },
                    { "putaway", sort.Putaway.ToString() },
                    { "pagemark", sort.Pagemark.ToString() },
                    { "pagemarklx", sort.Pagemarklx.ToString() },
                    { "daleiimg", sort.Daleiimg },
                    { "adata", sort.Adate.ToString() }
                };
                _res.Done(dic, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询单个大类异常");

                NLogHelper._.Error(_res.Msg, ex);
            }

            return _res;
        }

       /// <summary>
        /// 查询全部大类
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List(JObject data)
        {

            try
            {
                int id = Convert.ToInt32(data["id"]);
                ShopGoodsSortChildMethod sgscm = new ShopGoodsSortChildMethod(_dbConnect);
               
                List<DbShopGoodsSort> sortList = _dbConnect.DbShopGoodsSort.OrderBy(m => m.Daleiorder).ToList();
                List<string> List = new List<string>();
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                foreach (DbShopGoodsSort sort in sortList)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        //key用于识别不重复数据
                        { "key", sort.Id.ToString() + "1" },
                        { "id", sort.Id.ToString() },
                        { "name", sort.Daleiname },
                        { "order", sort.Daleiorder.ToString() },
                        { "putaway", sort.Putaway.ToString() },
                        { "pagemark", sort.Pagemark.ToString() },
                        { "pagemarklx", sort.Pagemarklx.ToString() },
                        { "pagemarklxname", ShopGoodsSortMethod.Getpagemarklx((int)sort.Pagemarklx) },
                        { "img", sort.Daleiimg },
                        { "adata", sort.Adate.ToString() },

                        { "num", ShopGoodsSortMethod.GetGoodsNum(sort.Id,_dbConnect).ToString() },
                        { "ischild", "0" }
                    };
                    string HaveChild = "0";
                    if (sgscm.GetSidList(sort.Id).Count() > 0)
                    {
                        HaveChild = "1";
                        dic.Add("child", JsonConvert.SerializeObject(List));
                    }
                    dic.Add("haveChild", HaveChild);
                    diclist.Add(dic);
                }
                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询全部大类异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 获取小类
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [SignCheckFilters]

        public Result ChildList(JObject data)
        {

            try
            {
                int id = Convert.ToInt32(data["id"]);

                ShopGoodsSortChildMethod sgscm = new ShopGoodsSortChildMethod(_dbConnect);
                var sortChildList = sgscm.GetSidList(id).Select(s => new
                {
                    key = s.Id + 2,
                    id = s.Id,
                    name = s.Xiaoleiname,
                    order = s.Xiaoleiorder,
                    pagemark = s.Pagemark,
                    putaway = s.Putaway,
                    img = s.Xiaoleiimg,
                    adata = s.Adate,
                    num = _dbConnect.DbShopGoods.Where(g => g.Xlid == s.Id).Count().ToString(),
                    ischild = 1
                });
                _res.Done(sortChildList, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取小类异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询所有大类名称
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [SignCheckFilters]
        public Result List_Name()
        {

            try
            {
                List<DbShopGoodsSort> sortList = _dbConnect.DbShopGoodsSort.OrderBy(m => m.Daleiorder).ToList();
                _res.Done(sortList, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询所有大类名称异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  修改大类
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
                int id = Convert.ToInt32(data["id"]);
                string daleiname = data["daleiname"].ToString();
                int daleiorder = Convert.ToInt32(data["daleiorder"]);
                string daleiimg = data["daleiimg"].ToString();
                int putaway = Convert.ToInt32(data["putaway"]);
                int pagemark = Convert.ToInt32(data["pagemark"]);
                int pagemarklx = Convert.ToInt32(data["pagemarklx"]);

                Server.Api.Method.ShopGoodsSortMethod sgam = new Server.Api.Method.ShopGoodsSortMethod(_dbConnect);
                DbShopGoodsSort upsort = sgam.GetById(id);
                if (upsort == null) { _res.Fail("修改失败"); return _res; }

                upsort.Daleiname = daleiname;
                upsort.Daleiorder = daleiorder;
                upsort.Putaway = putaway;
                upsort.Pagemark = pagemark;
                upsort.Pagemarklx = pagemarklx;
                upsort.Daleiimg = daleiimg;

                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "修改成功");

                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 7, "修改大类:" + daleiname);
                }
                else { _res.Fail("修改失败"); }
            }
            catch (Exception ex)
            {
                _res.Error("修改大类异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

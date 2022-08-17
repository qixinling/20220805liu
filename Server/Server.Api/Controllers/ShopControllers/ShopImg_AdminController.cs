using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Api.Method;

namespace Server.Api.Controllers.ShopControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShopImg_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public ShopImg_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 后台添加商城图片
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
                int lx = Convert.ToInt32(data["lx"]);
                string imgurl = data["imgurl"].ToString();
                string bdlx = data["bdlx"].ToString();
                string gid = data["gid"].ToString();

                DbShopImg si = new DbShopImg
                {
                    Lx = lx,
                    Img = imgurl,
                    Bdlx = bdlx,
                    Gid = gid
                };
                ShopImgMethod sim = new ShopImgMethod(_dbConnect);
                sim.Add(si);
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "添加成功");
                
                }
                else
                {
                    _res.Fail("添加失败");
                   
                }
            }
            catch (Exception ex)
            {
                _res.Error("添加商城图片异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除商城图片
        /// </summary>
        /// <param name="userid_admin"></param>
        /// <param name="token_admin"></param>
        /// <param name="delete_id"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Delete(JObject data)
        {
            
            

            try
            {
                string delete_id = data["delete_id"].ToString();

                string[] Dllist = delete_id.Split(',');
                ShopImgMethod sim = new ShopImgMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbShopImg si = sim.GetById(Id);
                    if (si != null)
                    {
                        sim.Remove(Id);
                    }
                }
                if (_dbConnect.SaveChanges() != 0)
                {
                    _res.Done(null, "删除成功");
                }
                else
                {
                    _res.Fail("删除失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("删除商城图片异常");
            
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询单条商城图片
        /// </summary>
        /// <param name="userid_admin"></param>
        /// <param name="token_admin"></param>
        /// <param name="select_id"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {
            
            try
            {
                int select_id = Convert.ToInt32(data["select_id"]);
                ShopImgMethod sim = new ShopImgMethod(_dbConnect);
                DbShopImg si =sim.GetById(select_id);
                if (si != null)
                {
                   
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "id", si.Id.ToString() },
                        { "lx", si.Lx.ToString() },
                        { "img", si.Img },
                        { "bdlx", si.Bdlx },
                        { "gid", si.Gid }
                    };
                    _res.Done(dic, "查询成功");
                }
                else
                {
                    _res.Fail("没有对应id");
                   
                }
            }
            catch (Exception ex)
            {
                _res.Error("查询单条商城图片异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }

            return _res;
        }

        /// <summary>
        /// 查询全部商城图片
        /// </summary>
        /// <param name="userid_admin"></param>
        /// <param name="token_admin"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List()
        {
            
            
            try
            {

                ShopImgMethod sim = new ShopImgMethod(_dbConnect);
                List<DbShopImg> silist = sim.GetList().OrderBy(m => m.Id).ToList();
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                foreach (DbShopImg si in silist)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "id", si.Id.ToString() },
                        { "lx", si.Lx.ToString() },
                        { "img", si.Img },
                        { "bdlx", si.Bdlx },
                        { "gid", si.Gid }
                    };
                    diclist.Add(dic);

                }
                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询全部商城图片异常");
            
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改商城图片
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
                int select_id = Convert.ToInt32(data["select_id"]);
                int lx = Convert.ToInt32(data["lx"]);
                string imgurl = data["imgurl"].ToString();
                string bdlx = data["bdlx"].ToString();
                string gid = data["gid"].ToString();

                ShopImgMethod sim = new ShopImgMethod(_dbConnect);
                DbShopImg si =sim.GetById(select_id);//原来图片的数据
                if (si == null) { _res.Fail("图片信息错误"); return _res; }
                si.Lx = lx;
                si.Img = imgurl;
                si.Bdlx = bdlx;
                si.Gid = gid;

                _dbConnect.SaveChanges();
                _res.Done(null, "修改成功");
            }
            catch (Exception ex)
            {
                _res.Error("修改商城商品图片异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

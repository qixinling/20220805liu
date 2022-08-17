using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ShopCollectController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public ShopCollectController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 商品收藏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Shoucang(JObject data)
        {
            List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
            try
            {

                string userid = Convert.ToString(data["userid"]);
                int uid = Convert.ToInt32(data["uid"]);
                int gid = Convert.ToInt32(data["gid"]);
                ShopCollectionMethod scm = new ShopCollectionMethod(_dbConnect);
                DbShopCollection co = scm.GetById(uid);
                if (co != null)
                {
                    string Goodsid = "," + gid + ",";
                    DbShopCollection cot =scm.GetList().Where(c => c.Uid == uid && EF.Functions.Like(c.Spath, "%," + gid + ",%")).FirstOrDefault();
                    if (cot != null)
                    {
                        string Qx = gid + ",";
                        cot.Spath = cot.Spath.Replace(Qx, "");
                        if (_dbConnect.SaveChanges() > 0)
                        {
                            Dictionary<string, string> dic = new Dictionary<string, string>
                            {
                                { "isshoucang", "0" }
                            };
                            diclist.Add(dic);
                            _res.Done(diclist, "取消成功");
                            return _res;
                        }
                        else
                        {
                            _res.Fail("取消失败");
                            return _res;
                        }
                    }
                    else
                    {
                        co.Spath = co.Spath + gid + ",";
                    }
                }
                else
                {
                    DbShopCollection coll = new DbShopCollection
                    {
                        Uid = uid,
                        Spath = "," + gid + ","
                    };
                   scm.Add(coll);
                }

                if (_dbConnect.SaveChanges() > 0)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "isshoucang", "1" }
                    };
                    diclist.Add(dic);

                    _res.Done(diclist, "收藏成功");
                }
                else
                {
                    _res.Fail("收藏失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("收藏异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取所有收藏商品信息
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

                string userid = Convert.ToString(data["userid"]);
                int uid = Convert.ToInt32(data["uid"]);
                ShopCollectionMethod scm = new ShopCollectionMethod(_dbConnect);
                DbShopCollection coll =scm.GetById(uid);
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                if (coll != null)
                {
                    string[] Spath = coll.Spath.Split(',');
                    Spath = Spath.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                    foreach (string Spa in Spath)
                    {
                        int Gid = Convert.ToInt32(Spa);
                        DbShopGoods goods = _dbConnect.DbShopGoods.Where(g => g.Id == Gid && g.Ispay > 0).FirstOrDefault();
                        if (goods != null)
                        {
                            Dictionary<string, string> dic = new Dictionary<string, string>
                            {
                                { "id", goods.Id.ToString() },
                                { "goodimg", goods.Goodsimg },
                                { "goodsname", goods.Goodsname },
                                { "goodsprict", goods.Goodsprice.ToString() }
                            };
                            diclist.Add(dic);
                        }
                    }
                    _res.Done(diclist, "查询成功");
                }
            }
            catch (Exception ex)
            {
                _res.Error("获取收藏商品异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取单个收藏商品是否收藏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {
            
            try
            {

                string userid = Convert.ToString(data["userid"]);
                int uid = Convert.ToInt32(data["uid"]);
                int gid = Convert.ToInt32(data["gid"]);
                ShopCollectionMethod scm = new ShopCollectionMethod( _dbConnect);
                DbShopCollection coll = scm.GetByUid(uid);
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                if (coll != null)
                {
                    string[] Spath = coll.Spath.Split(',');
                    Spath = Spath.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                    if (Spath.Length > 0)
                    {
                        foreach (string Spa in Spath)
                        {
                            int Spaid = Convert.ToInt32(Spa);
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            if (Spaid == gid)
                            {
                                dic.Add("isshoucang", "1");
                                diclist.Add(dic);
                                _res.Done(null, "已收藏");
                                return _res;
                            }
                            else
                            {
                                dic.Add("isshoucang", "0");
                                diclist.Add(dic);
                                _res.Done(null, "未收藏");
                            }
                        }
                    }
                    else
                    {
                        _res.Fail("无收藏");
                    }
                }
            }
            catch (Exception ex)
            {
                _res.Error("获取商品异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Cancel(JObject data)
        {
            
            try
            {

                string userid = Convert.ToString(data["userid"]);
                int uid = Convert.ToInt32(data["uid"]);
                int gid = Convert.ToInt32(data["gid"]);
                ShopCollectionMethod scm = new ShopCollectionMethod(_dbConnect);
                DbShopCollection coll =scm.GetByUid(uid);
                if (coll != null)
                {
                    string Goodsid = "," + gid + ",";
                    DbShopCollection cot = scm.GetList().Where(c => c.Uid == uid && EF.Functions.Like(c.Spath, "%," + gid + ",%")).FirstOrDefault();
                    if (cot != null)
                    {
                        string Qx = gid + ",";
                        cot.Spath = cot.Spath.Replace(Qx, "");
                        if (_dbConnect.SaveChanges() > 0)
                        {
                            _res.Done(null, "取消成功");
                        }
                        else
                        {
                            _res.Fail("取消失败");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _res.Error("取消收藏商品异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;

        }
    }
}

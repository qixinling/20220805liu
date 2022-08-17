using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Server.Api.Utils.Public;
using StackExchange.Redis;
using Server.Api.Utils;

namespace Server.Api.Controllers.ShopControllers.SiteController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Site_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        private readonly IDatabase _redis;
        public Site_AdminController(DbConnect dbConnect, Result res, RedisUtils redisUtils)
        {
            _dbConnect = dbConnect;
            _res = res;
            _redis = redisUtils.GetDatabase();
        }


        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Add(JObject data)
        {

            try
            {
                string userid_admin = data["userid_admin"].ToString();
                string site = data["site"].ToString();

                DbSite newsite = JsonConvert.DeserializeObject<DbSite>(site);
                _dbConnect.DbSite.Add(newsite);


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
                _res.Error("添加场次异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Delete(JObject data)
        {
            try
            {
                string ids = data["ids"].ToString();

                List<DbSite> sites = _dbConnect.DbSite.Where(c => EF.Functions.Like(ids, "%," + c.Id + ",%")).ToList();
                foreach (DbSite site in sites)
                {
                    if (!_redis.StringGet($"site{site.Id}").IsNull)
                    {
                        _redis.KeyDelete($"site{site.Id}");
                    }
                    _dbConnect.DbSite.Remove(site);
                }

                if (_dbConnect.SaveChanges() > 0)
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
                _res.Error("删除场次异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }



        /// <summary>
        /// 查询全部
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
                List<DbSite> sites = _dbConnect.DbSite.ToList();

                _res.Done(sites, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询全部场次异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改
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
                string site = data["site"].ToString();
                DbSite newsite = JsonConvert.DeserializeObject<DbSite>(site);
                DbSite oldsite = _dbConnect.DbSite.FirstOrDefault(c => c.Id == newsite.Id);
                ModUtils.ObjUpdateObj<DbSite, DbSite>(newsite, oldsite);

                _redis.StringSet($"site{oldsite.Id}", JsonConvert.SerializeObject(oldsite));

                _dbConnect.SaveChanges();
                _res.Done(null, "修改成功");
            }
            catch (Exception ex)
            {
                _res.Error("修改场次异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

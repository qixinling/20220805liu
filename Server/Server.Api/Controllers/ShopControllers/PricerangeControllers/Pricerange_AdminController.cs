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
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Server.Api.Utils.Public;
using StackExchange.Redis;
using Server.Api.Utils;

namespace Server.Api.Controllers.ShopControllers.PricerangeControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Pricerange_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        private readonly IDatabase _redis;
        public Pricerange_AdminController(DbConnect dbConnect, Result res, RedisUtils redisUtils)
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
                string price = data["price"].ToString();

                DbPricerange newsite = JsonConvert.DeserializeObject<DbPricerange>(price);
                _dbConnect.DbPricerange.Add(newsite);

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

                List<DbPricerange> plist = _dbConnect.DbPricerange.Where(c => EF.Functions.Like(ids, "%," + c.Id + ",%")).ToList();
                foreach (DbPricerange prices in plist)
                {
                    if (!_redis.StringGet($"prices{prices.Id}").IsNull)
                    {
                        _redis.KeyDelete($"prices{prices.Id}");
                    }

                    _dbConnect.DbPricerange.Remove(prices);
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
                _res.Error("删除区间异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List()
        {


            try
            {
                List<DbPricerange> sites = _dbConnect.DbPricerange.ToList();

                _res.Done(sites, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询全部区间异常");

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
                string price = data["price"].ToString();
                DbPricerange newprice = JsonConvert.DeserializeObject<DbPricerange>(price);
                DbPricerange oldprice = _dbConnect.DbPricerange.FirstOrDefault(c => c.Id == newprice.Id);

                ModUtils.ObjUpdateObj<DbPricerange, DbPricerange>(newprice, oldprice);
                _dbConnect.SaveChanges();

                _redis.StringSet($"prices{oldprice.Id}", JsonConvert.SerializeObject(oldprice));

                _res.Done(null, "修改成功");
            }
            catch (Exception ex)
            {
                _res.Error("修改区间异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

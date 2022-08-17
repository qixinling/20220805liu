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

namespace Server.Api.Controllers.ShopControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Jewellery_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public Jewellery_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
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
                string jewellery = data["jewellery"].ToString();

                DbJewellery newjewellery = JsonConvert.DeserializeObject<DbJewellery>(jewellery);
                _dbConnect.DbJewellery.Add(newjewellery);

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
                _res.Error("添加珠宝异常");
                
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

                List<DbJewellery> jewes = _dbConnect.DbJewellery.Where(c => EF.Functions.Like(ids, "%," + c.Id + ",%")).ToList();
                foreach (DbJewellery site in jewes)
                {
                    _dbConnect.DbJewellery.Remove(site);
                }

                if (_dbConnect.SaveChanges()>0)
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
                _res.Error("删除珠宝异常");
            
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
                List<DbJewellery> jewellery = _dbConnect.DbJewellery.ToList();

                _res.Done(jewellery, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询全部珠宝异常");
            
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {


            try
            {
                int jid =Convert.ToInt32(data["jid"]);
               
                DbJewellery jewellery = _dbConnect.DbJewellery.FirstOrDefault(c => c.Id == jid);


                _res.Done(jewellery, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询珠宝异常");

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
                string jewellery = data["jewellery"].ToString();
                DbJewellery newjewellery = JsonConvert.DeserializeObject<DbJewellery>(jewellery);
                DbJewellery oldjewellery = _dbConnect.DbJewellery.FirstOrDefault(c => c.Id == newjewellery.Id);

                 ModUtils.ObjUpdateObj<DbJewellery, DbJewellery>(newjewellery, oldjewellery);
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

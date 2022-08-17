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
using Server.Api.Utils.Public;

namespace Server.Api.Controllers.SystemControllers.AdminControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SystemSettingBonus_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public SystemSettingBonus_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 添加奖金参数
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

                DbSystemSettingBonus systemSettingBonus = JsonConvert.DeserializeObject<DbSystemSettingBonus>(data["systemSettingBonus"].ToString());

                SystemSettingBonusMethod ssbm = new SystemSettingBonusMethod(_dbConnect);

                if (_dbConnect.DbSystemSettingBonus.FirstOrDefault(b => b.Code == systemSettingBonus.Code) != null) { return _res.Fail("code重复"); }

                DbSystemSettingBonus lastSystemSettingBonus = ssbm.GetList().OrderByDescending(b => b.Index).FirstOrDefault();
                if (lastSystemSettingBonus == null)
                {
                    systemSettingBonus.Index = 1;
                }
                else
                {
                    systemSettingBonus.Index = lastSystemSettingBonus.Index + 1;
                }

                ssbm.Add(systemSettingBonus);

                _dbConnect.SaveChanges();

                _res.Done(ssbm.GetList(), "添加成功");

                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 9, "添加奖金参数");
            }
            catch (Exception ex)
            {
                _res.Error("添加奖金参数异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除奖金参数
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

                DbSystemSettingBonus systemSettingBonus = JsonConvert.DeserializeObject<DbSystemSettingBonus>(data["systemSettingBonus"].ToString());

                SystemSettingBonusMethod ssbm = new SystemSettingBonusMethod(_dbConnect);

                List<DbSystemSettingBonus> systemSettingBonusList = ssbm.GetList();
                foreach (DbSystemSettingBonus ssb in systemSettingBonusList)
                {
                    if (ssb.Index > systemSettingBonus.Index)
                    {
                        ssb.Index--;
                    }
                }

                ssbm.Remove(systemSettingBonus.Id);

                _dbConnect.SaveChanges();

                _res.Done(ssbm.GetList(), "删除成功");

                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 9, "删除奖金参数");
            }
            catch (Exception ex)
            {
                _res.Error("删除奖金参数异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 清空奖金参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result DeleteAll(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();

                _dbConnect.DbSystemSettingBonus.RemoveRange(_dbConnect.DbSystemSettingBonus.ToList());

                _dbConnect.SaveChanges();

                _res.Done(null, "清空成功");

                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 9, "清空奖金参数");
            }
            catch (Exception ex)
            {
                _res.Error("删除奖金参数异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 奖金参数集合
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
                string userid_admin = data["userid_admin"].ToString();

                SystemSettingBonusMethod ssbm = new SystemSettingBonusMethod(_dbConnect);

                List<DbSystemSettingBonus> systemSettingBonusList = ssbm.GetList();

                _res.Done(systemSettingBonusList, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询奖金参数异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改奖金参数
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

                DbSystemSettingBonus systemSettingBonus = JsonConvert.DeserializeObject<DbSystemSettingBonus>(data["systemSettingBonus"].ToString());

                int newIndex = Convert.ToInt32(data["index"]);
                int oldIndex = systemSettingBonus.Index;

                SystemSettingBonusMethod ssbm = new SystemSettingBonusMethod(_dbConnect);

                DbSystemSettingBonus systemSettingBonus1 = ssbm.GetById(systemSettingBonus.Id);
                DbSystemSettingBonus systemSettingBonus2 = ssbm.GetByIndex(newIndex);
                systemSettingBonus1.Index = newIndex;
                systemSettingBonus2.Index = oldIndex;

                _dbConnect.SaveChanges();

                _res.Done(ssbm.GetList(), "修改成功");
            }
            catch (Exception ex)
            {
                _res.Error("修改奖金参数异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改全部奖金参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result UpdataAll(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();

                List<DbSystemSettingBonus> systemSettingBonusList = JsonConvert.DeserializeObject<List<DbSystemSettingBonus>>(data["systemSettingBonusList"].ToString());

                SystemSettingBonusMethod ssbm = new SystemSettingBonusMethod(_dbConnect);

                foreach(DbSystemSettingBonus systemSettingBonus2 in systemSettingBonusList)
                {
                    DbSystemSettingBonus oldSystemSettingBonus2 = ssbm.GetById(systemSettingBonus2.Id);

                    ModUtils.ObjUpdateObj<DbSystemSettingBonus, DbSystemSettingBonus>(systemSettingBonus2, oldSystemSettingBonus2);

                    if (oldSystemSettingBonus2.Code == "") { return _res.Fail(oldSystemSettingBonus2.Name + "Code为空"); }
                }

                _dbConnect.SaveChanges();

                _res.Done(null, "修改成功");

                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 9, "修改奖金参数");
            }
            catch (Exception ex)
            {
                _res.Error("修改奖金参数异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

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
    public class SystemSetting_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public SystemSetting_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Get()
        {
            try
            {
                SystemSettingMethod ssm = new SystemSettingMethod(_dbConnect);
                DbSystemSetting ss = ssm.Get();
                if (ss == null) { _res.Fail("没有找到奖金参数数据"); return _res; }
                _res.Done(ss, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取奖金参数异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改系统参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Updata(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();

                DbSystemSetting ss = JsonConvert.DeserializeObject<DbSystemSetting>(data["ss"].ToString());
                SystemSettingMethod ssm = new SystemSettingMethod(_dbConnect);
                DbSystemSetting inss = ssm.GetById(ss.Id);
                if (inss == null) { _res.Fail("系统参数不存在"); return _res; }

                ModUtils.ObjUpdateObj<DbSystemSetting, DbSystemSetting>(ss, inss);
                if (_dbConnect.SaveChanges() > 0)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 9, "修改系统参数");
                    _res.Done(null, "修改成功");
                }
                else
                {
                    _res.Fail("修改失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改系统参数异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

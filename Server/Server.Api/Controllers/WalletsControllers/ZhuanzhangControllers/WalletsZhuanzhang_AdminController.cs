using Microsoft.AspNetCore.Mvc;
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

namespace Server.Api.Controllers.WalletsControllers.ZhuanzhangControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsZhuanzhang_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsZhuanzhang_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询所有转账数据
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
                List<DbWalletsZhuanzhang> zlist =_dbConnect.DbWalletsZhuanzhang.Where(b => b.Isdelete == 0).OrderByDescending(m => m.Zdate).ToList();
                _res.Done(zlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询转账信息异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除转帐记录
        /// </summary>
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
                if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }

                string delete_id = data["delete_id"].ToString();

                List<string> Zname = new List<string>();
                List<decimal> Zjine = new List<decimal>();

                string[] Del_list = delete_id.Split(',');
                string Msg = "";
                
                foreach (string Del in Del_list)
                {
                    int Id = Convert.ToInt32(Del);
                    DbWalletsZhuanzhang z = _dbConnect.DbWalletsZhuanzhang.FirstOrDefault(b => b.Id == Id && b.Isdelete == 0);
                    if (z != null)
                    {
                        z.Isdelete = 1;

                        Zname.Add(z.Username);
                        Zjine.Add(z.Jine);

                        if (_dbConnect.SaveChanges() > 0)
                        {
                            Msg += z.Userid + "删除成功. ";
                            SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 4, "删除转账记录:" + z.Username);
                        }
                        else
                        {
                            Msg += z.Userid + "删除失败. ";
                        }
                    }
                    else
                    {
                        Msg += Id + "记录不存在. ";
                    }
                }
                _res.Done(null, Msg);

                int i = 0;
                foreach (string name in Zname)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 4, "删除转账记录:" + name);
                    i++;
                }
            }
            catch (Exception ex)
            {
                _res.Error("删除转账记录异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

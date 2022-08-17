using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Server.Models;
using Server.Models.DataBaseModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Server.Api.Method;

namespace Server.Api.Controllers.SystemControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SystemAchievement_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public SystemAchievement_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询业绩全部信息
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
                SystemAchievementMethod sam = new SystemAchievementMethod(_dbConnect); 
                List<DbSystemAchievement> alist = sam.GetList().OrderByDescending(m => m.Id).ToList();

                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();

                Dictionary<string, string> dic = new Dictionary<string, string>();

                //解决转JSON后日期带T的问题
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter
                {
                    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                };

                dic.Add("alist", JsonConvert.SerializeObject(alist, timeFormat));
                //_res.Done(dic, "");
                diclist.Add(dic);

                int Zuserscount = 0;
                decimal Zusersjine = 0;
                decimal Zbonus = 0;
                int Zgouwucount = 0;
                decimal Zgouwujine = 0;
                decimal ZWalletsChongzhijine = 0;
                decimal Ztixianjine = 0;

                decimal Shangjiajine = 0;
                decimal Xyzjine = 0;
                decimal Zhuanjine = 0;
                if (alist.Count > 0)
                {
                    Zuserscount = alist.Sum(a => a.Userscount);
                    Zusersjine = alist.Sum(a => a.Usersjine);
                    Zbonus = alist.Sum(a => a.Bonus);
                    Zgouwucount = alist.Sum(a => a.Gouwucount);
                    Zgouwujine = alist.Sum(a => a.Gouwujine);
                    ZWalletsChongzhijine = alist.Sum(a => a.Chongzhijine);
                    Ztixianjine = alist.Sum(a => a.Tixianjine);
                    Shangjiajine = alist.Sum(a => a.Shangjiajine);
                    Xyzjine = alist.Sum(a => a.Xyzjine);
                    Zhuanjine = alist.Sum(a => (decimal)a.Zhuanjine);
                }
                dic = new Dictionary<string, string>
                {
                    { "userscount", Zuserscount.ToString() },
                    { "usersjine", Zusersjine.ToString() },
                    { "bonus", Zbonus.ToString() },
                    { "gouwucount", Zgouwucount.ToString() },
                    { "gouwujine", Zgouwujine.ToString() },
                    { "WalletsChongzhijine", ZWalletsChongzhijine.ToString() },
                    { "tixianjine", Ztixianjine.ToString() },
                    { "shangjiajine", Shangjiajine.ToString() },
                    { "xyzjine", Xyzjine.ToString() },
                    { "zhuanjine", Zhuanjine.ToString() }
                };
                diclist.Add(dic);
                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("业绩统计异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

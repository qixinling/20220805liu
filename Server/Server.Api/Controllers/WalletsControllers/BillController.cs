using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Server.Api.Method;

namespace Server.Api.Controllers.WalletsControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BillController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public BillController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Shouru(JObject data)
        {

            try
            {
                int uid = Convert.ToInt32(data["id"]);

                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                decimal Jinri = 0;
                decimal Zuori = 0;
                DateTime Dtime = DateTime.Now;
                DateTime Ztime = DateTime.Now.AddDays(-1);

                BillMethod bm = new BillMethod(_dbConnect);

                Jinri = bm.GetListByUid(uid).Where(b=>b.Blx == 0 && b.Isdel == 0 && b.Bdate.Year == Dtime.Year && b.Bdate.Month == Dtime.Month && b.Bdate.Day == Dtime.Day).Sum(b=>b.DbBillAmount.Sum(s=>s.Amount));

                Zuori = bm.GetListByUid(uid).Where(b =>b.Blx == 0 && b.Isdel == 0 && b.Bdate.Year == Ztime.Year && b.Bdate.Month == Ztime.Month && b.Bdate.Day == Ztime.Day).Sum(b => b.DbBillAmount.Sum(s => s.Amount));

                Dictionary<string, string> dic = new Dictionary<string, string>
                {
                    { "jinri", Jinri.ToString() },
                    { "zuori", Zuori.ToString() }
                };
                diclist.Add(dic);

                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取账单异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取账单
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
                int uid = Convert.ToInt32(data["id"]);
                int year = Convert.ToInt32(data["year"]);
                int month = Convert.ToInt32(data["month"]);

                BillMethod bm = new BillMethod(_dbConnect);

                var clist = bm.GetListByUidYearMonth(uid, year, month);

                _res.Done(clist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取账单异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

    }
}

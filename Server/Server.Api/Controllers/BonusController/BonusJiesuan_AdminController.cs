using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.BonusController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BonusJiesuan_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public BonusJiesuan_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [SignCheckFilters]
        [PermissionCheckFilters]
        public Result Jiesuan(JObject data)
        {
            string userid_admin = (string)data["userid_admin"];
            int lx = Convert.ToInt32(data["lx"]);
            BonusJiesuanMethod bjm = new BonusJiesuanMethod(_dbConnect);
            try
            {
                DbBonusJiesuan jr = new DbBonusJiesuan
                {
                    Userid = userid_admin,
                    Lx = lx,
                    Jdate = DateTime.Now
                };
                
                bjm.Add(jr);
                _dbConnect.SaveChanges();



                int jid = jr.Id;
                if (lx == 1)
                {
                    Task.Run(() =>
                    {
                        using DbConnect dbConnect = DbConnectUtils.GetDbContext();
                        DbBonusJiesuan ljr = bjm.GetById(jid);                
                        ljr.Wdate = DateTime.Now;
                        dbConnect.SaveChanges();
                    });
                }
                else if (lx == 2)
                {
                }

                _res.Done(null, "正在结算...");
            }
            catch (Exception ex)
            {
                _res.Error("结算" + lx + "异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询结算记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [SignCheckFilters]
        [PermissionCheckFilters]
        public Result List()
        {
            
            try
            {
                BonusJiesuanMethod bjm = new BonusJiesuanMethod(_dbConnect);
                List<DbBonusJiesuan> list =bjm.GetList().OrderByDescending(j => j.Id).ToList();
                List <Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
               
                foreach (DbBonusJiesuan jr in list)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "userid", jr.Userid },
                        { "lx", jr.Lx.ToString() }
                    };
                    string Lxname = "";
                    if (jr.Lx == 1)
                    {
                        Lxname = "月分红";
                    }
                    else if (jr.Lx == 2)
                    {
                        Lxname = "月分红";
                    }
                    else if (jr.Lx == 3)
                    {
                        Lxname = "";
                    }
                    dic.Add("lxname", Lxname);
                    dic.Add("jdate", jr.Jdate.ToString());
                    dic.Add("wdate", jr.Wdate.ToString());
                    dic.Add("state", jr.State.ToString());
                    switch (jr.State)
                    {
                        case 0:
                            dic.Add("statename", "正在结算...");
                            break;
                        case 1:
                            dic.Add("statename", "结算完成");
                            break;
                        case 2:
                            dic.Add("statename", "结算异常");
                            break;
                    }

                    diclist.Add(dic);
                }
                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询结算记录异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

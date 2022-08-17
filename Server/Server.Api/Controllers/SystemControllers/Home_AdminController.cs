using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.SystemControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Home_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public Home_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        [HttpPost]
        [TokenAdminCheckFilters]
        [SignCheckFilters]
        public Result Home_admin(JObject data)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();

                SystemAchievementMethod sam = new SystemAchievementMethod(_dbConnect);
                List<DbSystemAchievement> alist =sam.GetList();

                DateTime zdate = DateTime.Now.AddDays(-1);
                DateTime jdate = DateTime.Now;

                int Userscount = alist.Sum(a => a.Userscount);
                int Zuouserscount = alist.Where(a => a.Adate.Year == zdate.Year && a.Adate.Month == zdate.Month && a.Adate.Day == zdate.Day).Sum(a => a.Userscount);//昨日数据
                int Jinusercount = alist.Where(a => a.Adate.Year == jdate.Year && a.Adate.Month == jdate.Month && a.Adate.Day == jdate.Day).Sum(a => a.Userscount);//今日数据
                dic.Add("userscount", Userscount.ToString());
                dic.Add("zuouserscount", Zuouserscount.ToString());
                dic.Add("jinusercount", Jinusercount.ToString());

                decimal Usersjine = alist.Sum(a => a.Usersjine);
                decimal Zuousersjine = alist.Where(a => a.Adate.Year == zdate.Year && a.Adate.Month == zdate.Month && a.Adate.Day == zdate.Day).Sum(a => a.Usersjine);//昨日数据
                decimal Jinuserjine = alist.Where(a => a.Adate.Year == jdate.Year && a.Adate.Month == jdate.Month && a.Adate.Day == jdate.Day).Sum(a => a.Usersjine); ;//今日数据
                dic.Add("usersjine", Usersjine.ToString());
                dic.Add("zuousersjine", Zuousersjine.ToString());
                dic.Add("jinuserjine", Jinuserjine.ToString());


                decimal Bonus = alist.Sum(a => a.Bonus);
                decimal Zuobonus = alist.Where(a => a.Adate.Year == zdate.Year && a.Adate.Month == zdate.Month && a.Adate.Day == zdate.Day).Sum(a => a.Bonus);//昨日数据
                decimal Jinbonus = alist.Where(a => a.Adate.Year == jdate.Year && a.Adate.Month == jdate.Month && a.Adate.Day == jdate.Day).Sum(a => a.Bonus); ;//今日数据
                dic.Add("bonus", Bonus.ToString());
                dic.Add("zuobonus", Zuobonus.ToString());
                dic.Add("jinbonus", Jinbonus.ToString());

                int Ordercount = alist.Sum(a => a.Ordercount);
                ShopOrderMethod som = new ShopOrderMethod(_dbConnect);
                int Dfordercount = som.GetList().Where(o => o.Orderstate == 1).ToList().Count;
                int Yfordercount = som.GetList().Where(o => o.Orderstate == 2).ToList().Count;
                dic.Add("ordercount", Ordercount.ToString());
                dic.Add("dfordercount", Dfordercount.ToString());
                dic.Add("yfordercount", Yfordercount.ToString());


                WalletsChongzhiMethod wcm = new WalletsChongzhiMethod(_dbConnect);
                List<DbWalletsChongzhi> clist = wcm.GetList().Where(c => c.Isdelete == 0).ToList();
                decimal WalletsChongzhijine = clist.Sum(c => c.Jine);
                decimal Dsczjine = clist.Where(c => c.Ispay == 0).ToList().Count;
                decimal Ysczjine = clist.Where(c => c.Ispay == 1).ToList().Count;
                dic.Add("WalletsChongzhijine", WalletsChongzhijine.ToString());
                dic.Add("dsczjine", Dsczjine.ToString());
                dic.Add("ysczjine", Ysczjine.ToString());

                WalletsTixianMethod wtm = new WalletsTixianMethod(_dbConnect);
                List<DbWalletsTixian> tlist= wtm.GetList().Where(t => t.Isdelete == 0).ToList();
                decimal Tlistjine = tlist.Sum(c => c.Jine);
                decimal Dtxzjine = tlist.Where(c => c.Ispay == 0).ToList().Count;
                decimal Ytxzjine = tlist.Where(c => c.Ispay == 1).ToList().Count;
                dic.Add("tlistjine", Tlistjine.ToString());
                dic.Add("dtxzjine", Dtxzjine.ToString());
                dic.Add("ytxzjine", Ytxzjine.ToString());


                #region 柱状图
                List<int> Usercountlist = new List<int>();
                List<decimal> Userlsklist = new List<decimal>();
                List<decimal> Bonuslist = new List<decimal>();
                List<decimal> WalletsChongzhilist = new List<decimal>();
                List<decimal> Tixianlist = new List<decimal>();
                List<int> Orderlist = new List<int>();

                List<DbSystemAchievement> alist2;
                int i = 0;
                while (i < 12)
                {
                    DateTime dt = DateTime.Now.AddMonths(-i);

                    alist2 = alist.Where(a => a.Adate.Year == dt.Year && a.Adate.Month == dt.Month).ToList();
                    Usercountlist.Add(alist2.Sum(a => a.Userscount));
                    Userlsklist.Add(alist2.Sum(a => a.Usersjine));
                    Bonuslist.Add(alist2.Sum(a => a.Bonus));
                    WalletsChongzhilist.Add(alist2.Sum(a => a.Chongzhijine));
                    Tixianlist.Add(alist2.Sum(a => a.Tixianjine));
                    Orderlist.Add(alist2.Sum(a => a.Ordercount));
                    i++;
                }


                dic.Add("usercountlist", JsonConvert.SerializeObject(Usercountlist));
                dic.Add("userlsklist", JsonConvert.SerializeObject(Userlsklist));
                dic.Add("bonuslist", JsonConvert.SerializeObject(Bonuslist));
                dic.Add("WalletsChongzhilist", JsonConvert.SerializeObject(WalletsChongzhilist));
                dic.Add("tixianlist", JsonConvert.SerializeObject(Tixianlist));
                dic.Add("orderlist", JsonConvert.SerializeObject(Orderlist));
                #endregion


                #region 日志
                string userid_admin = Convert.ToString(data["userid_admin"]);

                //登录日志
                SystemLogMethod slm = new SystemLogMethod(_dbConnect);
                List<DbSystemLog> systemLoglistLogin = new List<DbSystemLog>();
                if (userid_admin == "admin")
                {
                    systemLoglistLogin = slm.GetList().Where(l => l.Lx == 0 && l.IsDel == 0).OrderByDescending(l => l.Id).Take(9).ToList();
                }
                else
                {
                    systemLoglistLogin = slm.GetList().Where(l => l.Lx == 0 && l.Userid != "admin" && l.IsDel == 0).OrderByDescending(l => l.Id).Take(9).ToList();
                } 

                
                List<DbSystemLog> systemLogList = new List<DbSystemLog>();
                if (userid_admin == "admin")
                {
                    systemLogList = slm.GetList().Where(l => l.Lx > 0 && l.IsDel == 0).OrderByDescending(l => l.Id).Take(5).ToList();
                }
                else
                {
                    systemLogList = slm.GetList().Where(l => l.Lx > 0 && l.Userid != "admin" && l.IsDel == 0).OrderByDescending(l => l.Id).Take(5).ToList();
                }

                //操作日志
                List<object> systemLoglist = new List<object>();
                foreach (DbSystemLog systemLog in systemLogList)
                {
                    var ln = new
                    {
                        userid = systemLog.Userid,
                        zdate = systemLog.Ldate,
                        lx = systemLog.Lx,
                        lxname = SystemLogMethod.GetLxName(systemLog.Lx),
                        bz = systemLog.Bz
                    };
                    systemLoglist.Add(ln);
                }

                dic.Add("loglist", JsonConvert.SerializeObject(systemLoglistLogin));
                dic.Add("logplist", JsonConvert.SerializeObject(systemLoglist));

                #endregion


                _res.Done(dic, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询首页信息异常");
             
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

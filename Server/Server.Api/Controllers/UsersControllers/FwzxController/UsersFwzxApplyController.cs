using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Api.Level;

namespace Server.Api.Controllers.UsersControllers.FwzxController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersFwzxApplyController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public UsersFwzxApplyController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        public string[] IsplayNameArr { get; set; } = new[] { "待审核", "已审核", "已撤销" };
        /// <summary>
        /// 服务中心申请
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Add(JObject data)
        {
            
            try
            {

                string userid = Convert.ToString(data["userid"]);
                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }
                int uid = Convert.ToInt32(data["uid"]);
                int lx= Convert.ToInt32(data["lx"]);
                int bdlevel = Convert.ToInt32(data["bdlevel"]);
                string add_sheng = Convert.ToString(data["add_sheng"]);
                string add_shi = Convert.ToString(data["add_shi"]);
                string add_xian = Convert.ToString(data["add_xian"]);
                string beizhu = Convert.ToString(data["beizhu"]);
                string add_address = Convert.ToString(data["add_address"]);
               
                if (bdlevel == 0) { _res.Fail("请选择正确的级别");  return _res; }
                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers us =um.GetByUsersid(userid);
                if (us != null)
                {
                    if (us.Isbd == 1 && us.Bdlevel >= bdlevel)
                    {
                        _res.Fail("您已成为服务中心,请勿重复申请");
                      
                    }
                    else
                    {
                        UsersFwzxApplyMethod ufam = new UsersFwzxApplyMethod(_dbConnect );
                        List<DbUsersFwzxApply> falist = ufam.GetList().Where(c => c.Uid == us.Id && c.Ispay == 0).ToList();
                        if (falist.Count == 0)
                        {
                            DbUsersFwzxApply fa = new DbUsersFwzxApply
                            {
                                Uid = us.Id,
                                Userid = userid,
                                Username = us.Username,
                                Bdlevel = bdlevel,
                                Jine = 0,
                                Bdsheng = add_sheng,
                                Bdshi = add_shi,
                                Bdxian = add_xian,
                                Bdaddress = add_address,
                                Ispay = 0,
                                Fdate = DateTime.Now,
                                Sdate = DateTime.Now,
                                Bz = beizhu,
                                Lx = lx
                            };

                            ufam.Add(fa);
                            _dbConnect.SaveChanges();
                            _res.Done(null, "申请成功,请等待审核");
                            
                        }
                        else
                        {
                            _res.Fail("有服务申请正在审核中,请勿重复提交");
                          
                        }
                    }
                }
                else
                {
                    _res.Fail("用户不存在");
                   
                }
            }
            catch (Exception ex)
            {
                _res.Error("申请服务中心异常");
          
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询用户服务中心最近申请记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {
            
            try
            {
                string userid = Convert.ToString(data["userid"]);

                UsersFwzxApplyMethod ufam = new UsersFwzxApplyMethod(_dbConnect);
                List<DbUsersFwzxApply> list =ufam.GetList().Where(c => c.Userid == userid).OrderBy(c => c.Sdate).ToList();
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                if (list.Count > 0)
                {
                    List<ILevel> bdLevelList = new Bdlevel().GetLevels();
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "userid", list[0].Userid },
                        { "username", list[0].Username },
                        { "bdlevel", list[0].Bdlevel.ToString() },
                        { "bdlevelname", bdLevelList[list[0].Bdlevel].Name },
                        { "bdsheng", list[0].Bdsheng },
                        { "bdshi", list[0].Bdshi },
                        { "bdxian", list[0].Bdxian },
                        { "bdaddress", list[0].Bdaddress },
                        { "bz", list[0].Bz },
                        { "fdate", list[0].Fdate.ToString() },
                        { "ispay", list[0].Ispay.ToString() }
                    };
                    if (list[0].Ispay == 0)
                    {
                        dic.Add("ispayname", "待审核");
                    }
                    else if (list[0].Ispay == 1)
                    {
                        dic.Add("ispayname", "已审核");
                    }
                    else if (list[0].Ispay == 2)
                    {
                        dic.Add("ispayname", "已撤销");
                    }
                    diclist.Add(dic);
                }

                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询服务最近申请记录异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询用户服务中心申请记录
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
                string userid = Convert.ToString(data["userid"]);
                int select_uid = Convert.ToInt32(data["select_uid"]);
                int lx = Convert.ToInt32(data["lx"]);
                DateTime query_date= Convert.ToDateTime(data["query_date"]);
                UsersFwzxApplyMethod ufam = new UsersFwzxApplyMethod(_dbConnect);
                List<DbUsersFwzxApply> list =ufam.GetList().Where(c => c.Uid == select_uid && c.Lx == lx).OrderByDescending(c => c.Id).ToList();
               
                if (PublicUtils.NotNull(query_date))
                {
                    
                    list = ufam.GetList().Where(m => m.Uid == select_uid && m.Lx == lx && m.Fdate.Year == query_date.Year && m.Fdate.Month == query_date.Month && m.Fdate.Day == query_date.Day).ToList();
                }

                List<ILevel> bdLevelList = new Bdlevel().GetLevels();

                var flist = list.Select(f => new
                {
                    f.Userid,
                    f.Username,
                    f.Bdlevel,
                    bdlevelname= bdLevelList[f.Bdlevel].Name,
                    f.Bdsheng,
                    f.Bdshi,
                    f.Bdxian,
                    f.Bdaddress,
                    f.Fdate,
                    f.Bz,
                    f.Ispay,
                    ispayname=IsplayNameArr[f.Ispay],
                });
                _res.Done(flist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询服务记录异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

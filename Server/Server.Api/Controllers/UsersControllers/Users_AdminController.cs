using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Crypto_Utils;
using Server.Utils.Http_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Server.Utils.Token_Utils;
using Server.Utils.Msg_Utils;
using Server.Api.Bonus.Algorithm;
using Server.Bonus;
using Server.Api.Level;

namespace Server.Api.Controllers.UsersControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Users_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public Users_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 获取用户奖金来源数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result GetUserBonusSourceData(JObject data)
        {
            try
            {
                string uid = data["uid"].ToString();
                List<IBonus> bonusList = BonusUtils.BonusList;
                BonusSourceMethod bsm = new BonusSourceMethod(_dbConnect);
                var list =bsm.GetList().Where(d => d.Uid == Convert.ToInt32(uid)).Select(d => new
                {
                    d.Id,
                    d.Uid,
                    d.Userid,
                    d.Yuserid,
                    bonusList[d.Lx].BonusName,
                    d.Jine,
                    d.Bz,
                    d.Sdate
                }).ToList();
                _res.Done(list, "获取成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取用户奖金来源数据异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取用户账单数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result GetUserBillData(JObject data)
        {
            try
            {
                int uid = Convert.ToInt32(data["uid"]);
                BillMethod bm = new BillMethod(_dbConnect);
                List<DbBill> list = bm.GetListByUid(uid);
                _res.Done(list, "获取成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取用户账单数据异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  撤销服务中心资格
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result RevokeFwzx(JObject data)
        {


            try
            {
                string userid_admin = data["userid_admin"].ToString();
                string delete_id = data["delete_id"].ToString();

                string[] Dllist = delete_id.Split(',');

                List<string> Cname = new List<string>();
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                   
                    DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Id == Id && c.Isbd == 1);
                    if (us != null)
                    {
                        us.Isbd = 0;
                        us.Bdsheng = "";
                        us.Bdshi = "";
                        us.Bdxian = "";
                        us.Bdaddress = "";

                        MsgUtils.Send(0, "服务中心", "您的服务中心资格已被撤销", 0, "系统消息", us.Id, us.Userid);
                    }

                    Cname.Add(us.Userid);
                }
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "撤销资格成功");

                    foreach (string name in Cname)
                    {
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "撤销服务中心:" + name);
                    }
                }
                else
                {
                    _res.Fail("撤销资格失败");

                }
            }
            catch (Exception ex)
            {
                _res.Error("撤销资格异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  服务中心
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result ListFwzx()
        {


            try
            {
                UsersMethod um = new UsersMethod(_dbConnect);
                List<ILevel> bdLevelList = new Bdlevel().GetLevels();
                var userslist = um.GetList().Where(m => m.Isbd == 1).OrderByDescending(m => m.Id).Select(m => new
                {
                    m.Id,
                    m.Userid,
                    m.Username,
                    m.Usertel,
                    m.Bdsheng,
                    m.Bdshi,
                    m.Bdxian,
                    m.Bdaddress,
                    m.Bdlevel,
                    m.Bddate,
                    bdlevelname = bdLevelList[m.Bdlevel].Name
                });
                _res.Done(userslist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询服务中心信息异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 网络图谱
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Network(JObject data)
        {

            try
            {
                int areacount = Convert.ToInt32(data["areacount"]);

                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<NetworkMod> rtlist = new List<NetworkMod>();

                DbSystemSetting ss = new SystemSettingMethod(_dbConnect).GetById(1);
                if (ss.Switchwlt == 0) { _res.Fail("暂未开放"); return _res; }

                UsersFteamMethod ufm = new UsersFteamMethod(_dbConnect);
                DbUsersFteam uf =ufm.GetById(1);
                if (uf != null)
                {
                    NetworkMod rt = new NetworkMod
                    {
                        Id = uf.Id,
                        Label = uf.Userid,
                        Treeplace = uf.Ftreeplace
                    };
                    if (rt.Treeplace == 1)
                    {
                        rt.Area = "A区";
                    }
                    else if (rt.Treeplace == 2)
                    {
                        rt.Area = "B区";
                    }
                    else if (rt.Treeplace == 3)
                    {
                        rt.Area = "C区";
                    }
                    else if (rt.Treeplace == 4)
                    {
                        rt.Area = "D区";
                    }
                    else if (rt.Treeplace == 5)
                    {
                        rt.Area = "E区";
                    }
                    rt.Teamcount = uf.Teamcount;
                    rt.Teamyeji = uf.Teamyeji;
                    rt.Area1 = uf.Area1;
                    rt.Area2 = uf.Area2;
                    rt.Area3 = uf.Area3;
                    rt.Area4 = uf.Area4;
                    rt.Area5 = uf.Area5;

                    rt = UsersUtils.Network(_dbConnect, uf, rt, areacount);

                    rtlist.Add(rt);

                    string rtjson = JsonConvert.SerializeObject(rtlist);

                    dic.Add("network", rtjson);
                    dic.Add("wlt", ss.Switchwlt.ToString());

                    _res.Done(dic, "查询成功");
                }
                else
                {
                    _res.Fail("未找到该会员");

                }

            }
            catch (Exception ex)
            {
                _res.Error("查询异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 推荐图谱
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Tree()
        {

            try
            {


                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<TreeMod> rtlist = new List<TreeMod>();
                DbSystemSetting ss = new SystemSettingMethod(_dbConnect).GetById(1);
                if (ss.Switchtjt == 0) { _res.Fail("暂未开放"); return _res; }

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers us =um.GetById(1);
                if (us == null) { _res.Fail("未找到该会员"); return _res; }

                List<Ulevel> uLevelList = new Ulevel().GetLevels();
                List<ILevel> xLevelList = new Xlevel().GetLevels();

                string uxName = " [";
             
             //  if (us.Ulevel > 0)
              //  {
                    uxName += "  " + uLevelList[us.Ulevel].Name;
                //  }

                uxName += "  " + us.Teamcount;
                uxName += "  " + us.Lsk;

                uxName += "  " + us.Riteamyeji + "]";
                TreeMod rt = new TreeMod
                {
                    Id = us.Id,
                    Label = us.Usertel + uxName,
                    Icon = "el-icon-s-custom ispay" + us.Ispay,
                    Teamcount = us.Teamcount,
                    Teamyeji = us.Teamyeji,
                    Riteamyeji = us.Riteamyeji
                };
                rt = UsersUtils.Tree(_dbConnect, us, rt);

                rtlist.Add(rt);

                string Rtjson = JsonConvert.SerializeObject(rtlist);
                dic.Add("tree", Rtjson);
                dic.Add("tjt", ss.Switchtjt.ToString());

                _res.Done(dic, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 查询用户伞下业绩
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result QueryPerformance(JObject data)
        {


            try
            {
                string userid_admin = data["userid_admin"].ToString();
                int uid = Convert.ToInt32(data["uid"]);
                string sdate = data["sdate"].ToString();
                string edate = data["edate"].ToString();

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers us =um.GetById(uid);
                if (us == null) { _res.Fail("用户信息错误"); return _res; }



                DateTime sdate1 = sdate.Equals("-") ? us.Rdt : Convert.ToDateTime(sdate);
                DateTime edate1 = edate.Equals("-") ? DateTime.Now : Convert.ToDateTime(edate);

                List<DbUsers> uslist = um.GetList().Where(u => u.Repath.Contains("," + us.Id + ",") && u.Pdt >= sdate1 && u.Pdt <= edate1 && u.Ispay > 0).ToList();
                int count = uslist.Count();
                decimal performance = uslist.Sum(u => u.Lsk);

                Dictionary<string, string> dic = new Dictionary<string, string>
                {
                    { "sdate", sdate1.ToString() },
                    { "edate", edate1.ToString() },
                    { "count", count.ToString() },
                    { "performance", performance.ToString() }
                };
                _res.Done(dic, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询用户伞下业绩异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除会员
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
                string delete_id = data["delete_id"].ToString();

                string Msg = "";
                string[] Dllist = delete_id.Split(',');
                int i = 0;
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    if (i > 0) { Msg += ","; }
                    Msg += UsersUtils.Delete(Id, 0, userid_admin, 0);
                }

                _res.Done(null, Msg);
            }
            catch (Exception ex)
            {
                _res.Error("删除用户异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  激活会员
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Jihuo(JObject data)
        {


            try
            {
                string userid_admin = data["userid_admin"].ToString();
                string ids = data["ids"].ToString();

                List<DbUsers> ulist = _dbConnect.DbUsers.Where(c => EF.Functions.Like(ids, ",%" + c.Id + "%,")).ToList();
                foreach (DbUsers item in ulist)
                {
                    item.Ispay = 1;
                }


                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "激活成功");
                }
                else
                {
                    _res.Fail("激活失败");
                }
              
            }
            catch (Exception ex)
            {
                _res.Error("激活异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  锁定会员
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Suoding(JObject data)
        {


            try
            {
                string userid_admin = data["userid_admin"].ToString();
                string s_id = data["s_id"].ToString();

                string Msg = "";
                string[] Dllist = s_id.Split(',');
                List<string> Cname = new List<string>();
                UsersMethod um = new UsersMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbUsers users =um.GetById(Id);
                    if (users != null)
                    {
                        if (users.Islock == 0)
                        {
                            users.Islock = 1;
                            _dbConnect.SaveChanges();
                            Msg += users.Userid + "锁定成功. ";
                            Cname.Add(users.Userid);
                        }
                        else
                        {
                            Msg += users.Userid + "状态错误,锁定失败. ";
                        }
                    }
                    else
                    {
                        Msg += "用户不存在. ";
                    }
                }

                _res.Done(null, Msg);


                foreach (string Name in Cname)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "锁定会员:" + Name);
                }
            }
            catch (Exception ex)
            {
                _res.Error("锁定用户异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 解锁会员
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Jiesuo(JObject data)
        {


            try
            {
                string userid_admin = data["userid_admin"].ToString();
                string j_id = data["j_id"].ToString();

                string Msg = "";
                string[] Dllist = j_id.Split(',');
                List<string> Cname = new List<string>();
                UsersMethod um = new UsersMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbUsers users =um.GetById(Id);
                    if (users != null)
                    {
                        if (users.Islock == 1)
                        {
                           /* List<DbHold> hlist = _dbConnect.DbHold.Where(c=>c.Uid == users.Id && c.Siscf == 1 && c.State == 2).ToList();
                            foreach (DbHold hold in hlist)
                            {
                                hold.Pdate = DateTime.Now.AddMinutes(150);
                                hold.Siscf = 0;
                            } 
                            List<DbHold> hlist2 = _dbConnect.DbHold.Where(c => c.Buid == users.Id && c.Biscf == 1 && c.State == 2).ToList();
                            foreach (DbHold hold2 in hlist2)
                            {
                                hold2.Pdate = DateTime.Now.AddMinutes(150);
                                hold2.Biscf = 0;
                            }*/

                            users.Islock = 0;
                            _dbConnect.SaveChanges();
                            Msg += users.Userid + "解锁成功. ";
                            Cname.Add(users.Userid);
                        }
                        else
                        {
                            Msg += users.Userid + "状态错误,锁定失败. ";
                        }
                    }
                    else
                    {
                        Msg += "用户不存在. ";
                    }
                }

                _res.Done(null, Msg);


                foreach (string Name in Cname)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "解锁会员:" + Name);
                }
            }
            catch (Exception ex)
            {
                _res.Error("解锁用户异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 空单会员
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Kongdan(JObject data)
        {


            try
            {
                string k_id = data["k_id"].ToString();
                string userid_admin = data["userid_admin"].ToString();

                string Msg = "";
                string[] Dllist = k_id.Split(',');
                List<string> Cname = new List<string>();
                UsersMethod um = new UsersMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbUsers users = um.GetById(Id);
                    if (users != null)
                    {
                        if (users.Ispay <= 1)
                        {
                            users.Ispay = 2;
                            _dbConnect.SaveChanges();
                            Msg += users.Userid + "操作成功. ";

                            Cname.Add(users.Userid);
                        }
                        else
                        {
                            Msg += users.Userid + "状态错误,操作失败. ";
                        }
                    }
                    else
                    {
                        Msg += "用户不存在. ";
                    }
                }

                _res.Done(null, Msg);


                foreach (string name in Cname)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "空单会员:" + name);
                }
            }
            catch (Exception ex)
            {
                _res.Error("空单会员异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取单个会员信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {


            try
            {
                int id = Convert.ToInt32(data["id"]);

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers us = um.GetById(id);
                if (us != null)
                {

                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "id", us.Id.ToString() },
                        { "userid", us.Userid },
                        { "username", us.Username },
                        { "usercode", us.Usercode },
                        { "usertel", us.Usertel },
                        { "rdt", us.Rdt.ToString() },
                        { "pdt", us.Pdt.ToString() },
                        { "ulevel", us.Ulevel.ToString() },
                        { "ylevel", us.Ylevel.ToString() },
                        { "xlevel", us.Xlevel.ToString() },
                        { "lsk", us.Lsk.ToString() },
                        { "ylsk", us.Ylsk.ToString() },
                        { "ispay", us.Ispay.ToString() },
                        { "ispayname", UsersUtils.Getuserispayname(us.Ispay) },
                        { "islock", us.Islock.ToString() },
                        { "islockname", UsersUtils.Getuserispayname(us.Islock) },
                        { "rename", us.Rename },
                        { "recount", us.Recount.ToString() },
                        { "reyeji", us.Reyeji.ToString() },
                        { "teamcount", us.Teamcount.ToString() },
                        { "teamyeji", us.Teamyeji.ToString() },
                         { "zzxz", us.Zzxz.ToString() },
                         { "iswn", us.Iswn.ToString() },
                         { "riteamyeji", us.Riteamyeji.ToString() },
                         { "djjine", us.Djjine.ToString() },
                         { "istq", us.Istq.ToString() },
                         { "studioname", us.StudioName },
                         { "studiocard", us.StudioCard },

                    };

                    //用户收款账户
                    UsersBankMethod ubm = new UsersBankMethod(_dbConnect);
                    List<Dictionary<string, string>> ubdiclist = new List<Dictionary<string, string>>();
                    List<DbUsersBank> ublist = ubm.GetList().Where(u => u.Uid == us.Id).ToList();
                    foreach (DbUsersBank ub in ublist)
                    {
                        Dictionary<string, string> ubdic = new Dictionary<string, string>
                        {
                            { "id", ub.Id.ToString() },
                            { "uid", ub.Uid.ToString() },
                            { "userid", ub.Userid },
                            { "username", ub.Username },
                            { "usertel", ub.Usertel },
                            { "bankname", ub.Bankname },
                            { "bankcard", ub.Bankcard },
                            { "isdefault", ub.Isdefault.ToString() },
                            { "bankimg", ub.Bankimg }
                        };
                        ubdiclist.Add(ubdic);
                    }
                    dic.Add("ubanklist", JsonConvert.SerializeObject(ubdiclist));

                    //用户地址
                    UsersAddressMethod uam = new UsersAddressMethod(_dbConnect);
                    List<Dictionary<string, string>> uadiclist = new List<Dictionary<string, string>>();
                    List<DbUsersAddress> ualist =uam.GetList().Where(u => u.Uid == us.Id).ToList();
                    foreach (DbUsersAddress ua in ualist)
                    {
                        Dictionary<string, string> uadic = new Dictionary<string, string>
                        {
                            { "id", ua.Id.ToString() },
                            { "uid", ua.Uid.ToString() },
                            { "userid", ua.Userid },
                            { "username", ua.Username },
                            { "usertel", ua.Usertel },
                            { "sheng", ua.Sheng },
                            { "shi", ua.Shi },
                            { "xian", ua.Xian },
                            { "address", ua.Address },
                            { "areaCode", ua.AreaCode },
                            { "isdefault", ua.Isdefault.ToString() }
                        };
                        uadiclist.Add(uadic);
                    }
                    dic.Add("uaddresslist", JsonConvert.SerializeObject(uadiclist));

                    _res.Done(dic, "查询成功");
                }
                else
                {
                    _res.Fail("没有对应id");

                }
            }
            catch (Exception ex)
            {
                _res.Error("查询用户信息异常");

                NLogHelper._.Error(_res.Msg, ex);
            }

            return _res;
        }

        /// <summary>
        /// 获取会员集合
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
                int ispay = Convert.ToInt32(data["ispay"]);
                UsersMethod um = new UsersMethod(_dbConnect);
                List<DbUsers> userslist = new List<DbUsers>();
                if (ispay == 0)
                {
                    userslist = um.GetList().Where(m => m.Ispay == ispay).ToList();
                }
                else if (ispay == 1)
                {
                    userslist = um.GetList().Where(m => m.Ispay >= ispay).OrderByDescending(u => u.Pdt).ToList();
                }
                else if(ispay == 99)
                {
                    userslist = um.GetList().Where(m => m.Iswn == 1).OrderByDescending(u => u.Pdt).ToList();
                }

                if (ispay >= 1)
                {
                    userslist = userslist.OrderByDescending(u => u.Pdt).ToList();
                }

                List<Ulevel> uLevelList = new Ulevel().GetLevels();
                List<ILevel> xLevelList = new Xlevel().GetLevels();
                var list = userslist.Select(u => new
                {
                    u.Id,
                    u.Userid,
                    u.Username,
                    u.Usercode,
                    u.Usertel,
                    u.Rename,
                    u.Rdt,
                    u.Pdt,
                    u.Ulevel,
                    ulevelname = uLevelList[u.Ulevel].Name,
                    u.Ylevel,
                    u.Xlevel,
                    xlevelname = xLevelList[u.Xlevel].Name,
                    u.Lsk,
                    u.Pv,
                    u.Ylsk,
                    u.Recount,
                    u.Teamcount,
                    u.Teamyeji,
                    u.Riteamyeji,
                    u.Zzxz,
                    u.Ispay,
                    spayname = UsersUtils.Getuserispayname(u.Ispay),
                    u.Islock,
                    islockname= UsersUtils.Getuserislockname(u.Islock),
                    u.Mystudioname,
                    
                });
                _res.Done(list, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取会员集合异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取画室长
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result HsList(JObject data)
        {
            try
            {
                
                UsersMethod um = new UsersMethod(_dbConnect);
                List<Ulevel> uLevelList = new Ulevel().GetLevels();

                List<DbUsers> userslist = um.GetList().Where(m => m.Ulevel == 1).OrderByDescending(u => u.Pdt).ToList(); 
                var list = userslist.Select(u => new
                {
                    u.Id,
                    u.Userid,
                    u.Username,
                    u.Usercode,
                    u.Usertel,
                    u.Rename,
                    u.Rdt,
                    u.Pdt,
                    u.Ulevel,
                    ulevelname = uLevelList[u.Ulevel].Name,
                    u.StudioName,
                    u.StudioCard
                });
                _res.Done(list, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取画室长异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        ///// <summary>
        ///// 激活会员
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[TokenAdminCheckFilters]
        //[PermissionCheckFilters]
        //[SignCheckFilters]
        //public Result Jihuo(JObject data)
        //{
        //    try
        //    {
        //        string userid_admin = data["userid_admin"].ToString();
        //        if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }

        //        string ids = data["ids"].ToString();

        //        string[] Cid = ids.Split(',');
        //        string Msg = "";
        //        for (int i = 0; i < Cid.Length; i++)
        //        {
        //            if (Cid[i] != "")
        //            {
        //                Msg += UsersUtils.Jihuo(0, Convert.ToInt32(Cid[i]), 1, userid_admin);
        //                if (i > 0)
        //                {
        //                    Msg += ",";
        //                }
        //            }
        //        }
        //        _res.Done(null, Msg);

        //    }
        //    catch (Exception ex)
        //    {
        //        _res.Error("激活会员异常");

        //        NLogHelper._.Error(_res.Msg, ex);
        //    }
        //    return _res;
        //}

        /// <summary>
        /// 登录前台指定会员
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Login(JObject data)
        {
            string userid_admin = data["userid_admin"].ToString();
            string userid = data["userid"].ToString();

            SystemSettingMethod ssm = new SystemSettingMethod(_dbConnect);
            DbSystemSetting ss = ssm.Get();
            if (ss.Switchsystem == 0) { _res.Fail(ss.Systemclosemsg); return _res; }
            if (string.IsNullOrWhiteSpace(userid)) { _res.Fail("用户信息错误"); return _res; }

            UsersMethod um = new UsersMethod(_dbConnect);
            DbUsers users =um.GetByUsersid(userid);
            if (users == null) { _res.Fail("用户信息错误"); return _res; }
            if (users.Islock == 1) { _res.Fail("该会员账户已被冻结"); return _res; }
            string Token = TokenUtils.Token_add(users.Id, users.Userid, 0, HttpInfoUtils.GetIP(), HttpInfoUtils.GetOSVersion());
            if (Token != null && !Token.Equals(""))
            {
                _res.Done(new
                {
                    token = Token,
                    id = users.Id,
                    userid = users.Userid,
                    username = users.Username,
                    usertel = users.Usertel
                }, "登录成功");
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "登录前台指定会员:" + users.Userid, null, null, users.Id);
            }
            else
            {
                _res.Fail("登录失败");
            }
            return _res;
        }

        /// <summary>
        /// 修改会员信息
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
                string users_json = data["users_json"].ToString();
                string ubanklistjson = data["ubanklistjson"].ToString();
                string uaddresslistjson = data["uaddresslistjson"].ToString();
                string beizhu = data["beizhu"].ToString();

                JObject jobj = (JObject)JsonConvert.DeserializeObject(users_json);
                DbUsers users = new DbUsers();
                UsersMethod um = new UsersMethod(_dbConnect);

                foreach (var item in jobj)
                {
                    //Console.WriteLine(item.Key + " " + item.Value.ToString());
                    if (item.Key.Equals("id"))
                    {
                        users = um.GetById ((int)item.Value);
                    }
                    else if (item.Key.Equals("username"))
                    {
                        users.Username = item.Value.ToString();
                    }
                    else if (item.Key.Equals("usertel"))
                    {
                        users.Usertel = item.Value.ToString();
                    }
                    else if (item.Key.Equals("usercode"))
                    {
                        users.Usercode = item.Value.ToString();
                    }
                    else if (item.Key.Equals("ulevel"))
                    {
                        users.Ulevel = (int)item.Value;
                    }
                    else if (item.Key.Equals("xlevel"))
                    {
                        users.Xlevel = (int)item.Value;
                    }
                    else if (item.Key.Equals("recount"))
                    {
                        users.Recount = (int)item.Value;
                    }
                    else if (item.Key.Equals("reyeji"))
                    {
                        users.Reyeji = (decimal)item.Value;
                    }
                    else if (item.Key.Equals("teamcount"))
                    {
                        users.Teamcount = (int)item.Value;
                    }
                    else if (item.Key.Equals("teamyeji"))
                    {
                        users.Teamyeji = (decimal)item.Value;
                    }
                    else if (item.Key.Equals("zzxz"))
                    {
                        users.Zzxz = (int)item.Value;
                    }
                    else if (item.Key.Equals("studioname"))
                    {
                        if (item.Value.ToString()!= "")
                        {
                            DbUsers dbUsers = _dbConnect.DbUsers.FirstOrDefault(c => c.StudioName == item.Value.ToString());
                            if (dbUsers != null) { return _res.Fail("画室名称已存在"); }
                            users.StudioName = item.Value.ToString();
                        }
                       
                    }
                    else if (item.Key.Equals("studiocard"))
                    {
                        if (item.Value.ToString() != "")
                        {
                            DbUsers dbUsers = _dbConnect.DbUsers.FirstOrDefault(c => c.StudioName == item.Value.ToString());
                            if (dbUsers != null) { return _res.Fail("画室编号已存在"); }
                            users.StudioCard = item.Value.ToString();
                        }
                    }
                }
                List<DbUsersBank> uabnklist = JsonConvert.DeserializeObject<List<DbUsersBank>>(ubanklistjson);
                UsersBankMethod ubm = new UsersBankMethod(_dbConnect);
                foreach (DbUsersBank item in uabnklist)
                {
                    DbUsersBank ub = ubm.GetById(item.Id);
                    if (ub == null) { continue; }
                    ub.Bankname = item.Bankname;
                    ub.Bankcard = item.Bankcard;
                    ub.Username = item.Username;
                    ub.Usertel = item.Usertel;
                }
                List<DbUsersAddress> uaddresslist = JsonConvert.DeserializeObject<List<DbUsersAddress>>(uaddresslistjson);
                UsersAddressMethod uam = new UsersAddressMethod(_dbConnect);
                foreach (DbUsersAddress item in uaddresslist)
                {
                    DbUsersAddress ua =uam.GetById(item.Id);
                    if (ua == null) { continue; }
                    ua.Username = item.Username;
                    ua.Usertel = item.Usertel;
                    ua.Sheng = item.Sheng;
                    ua.Shi = item.Shi;
                    ua.Xian = item.Xian;
                    ua.Address = item.Address;
                }
                if (users.Id > 0)
                {
                    if (_dbConnect.SaveChanges() > 0)
                    {
                        _res.Done(null, "修改成功");
                        SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "修改会员资料:" + users.Userid, null, null, users.Id);
                    }
                    else
                    {
                        _res.Fail("无修改");

                    }
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改会员资料异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Update_Password(JObject data)
        {


            try
            {
                string userid_admin = data["userid_admin"].ToString();
                int select_id = Convert.ToInt32(data["select_id"]);
                string password = data["password"].ToString();
                int lx = Convert.ToInt32(data["lx"]);

                if (password.Equals("") || password.Length < 6) { _res.Fail("密码格式不正确,请至少输入6位"); return _res; }

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers us = um.GetById(select_id);
                if (us == null) { _res.Fail("用户信息错误"); return _res; }

                string Passwordmd5 = MD5Utils.MD5Encrypt(password, 32);
                if (lx == 1)
                {
                    us.Password = Passwordmd5;
                }
                else if (lx == 2)
                {
                    us.Password2 = Passwordmd5;
                }

                _dbConnect.SaveChanges();
                _res.Done(null, "修改成功");
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "修改会员密码:" + us.Userid, null, null, us.Id);
            }
            catch (Exception ex)
            {
                _res.Error("修改密码异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  修改推荐人
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Update_Rename(JObject data)
        {


            try
            {
                string userid_admin = data["userid_admin"].ToString();
                int select_id = Convert.ToInt32(data["select_id"]);
                string updaterename = data["updaterename"].ToString();

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers us = um.GetById (select_id);
                if (us == null) { _res.Fail("用户信息错误"); return _res; }

                string Yrename = us.Rename;
                if (updaterename.Equals(us.Userid)) { _res.Fail("推荐人不能是自己"); return _res; }

                //判断推荐人是否相同
                if (updaterename.Equals(Yrename)) { _res.Fail("请勿修改相同的推荐人"); return _res; }

                DbUsers reur = um.GetByUsersid(updaterename);
                if (reur == null) { _res.Fail("新推荐人不存在"); return _res; }
                //判断新推荐人是否在会员的团队中
                string pdid = "," + us.Id + ",";
                if (reur.Repath.IndexOf(pdid) >= 0)
                {
                    _res.Fail("新推荐人不能是该会员的团队成员");

                    return _res;
                }

                us.Reid = reur.Id;
                us.Rename = reur.Userid;
                us.Relevel = reur.Relevel + 1;
                us.Repath = reur.Repath + reur.Id + ",";

                List<DbUsers> turlist = _dbConnect.DbUsers.Where(r => EF.Functions.Like(r.Repath, "%," + us.Id + ",%")).OrderBy(r => r.Relevel).ToList();
                foreach (DbUsers tur in turlist)
                {
                    DbUsers treur =um.GetById (tur.Reid);
                    tur.Relevel = treur.Relevel + 1;
                    tur.Repath = treur.Repath + treur.Id + ",";
                }

                _dbConnect.SaveChanges();
                _res.Done(null, "修改成功");
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "修改会员推荐人:" + us.Userid + " " + " 原推荐人:" + Yrename + " 新推荐人:" + updaterename, null, null, us.Id);
            }
            catch (Exception ex)
            {
                _res.Error("修改推荐人异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result UpdateHuashi(JObject data)
        {


            try
            {
                string userid_admin = data["userid_admin"].ToString();
                int select_id = Convert.ToInt32(data["select_id"]);
                int ul = Convert.ToInt32(data["ul"]);

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers us = um.GetById(select_id);
                if (us == null) { _res.Fail("用户信息错误"); return _res; }

                if(ul == 1)
                {
                    us.Mystudioid = us.Id;
                    us.Mystudioname = us.Userid;

                    List<DbUsers> ulist = _dbConnect.DbUsers.Where(c => EF.Functions.Like(c.Repath, "%," + us.Id + ",%")).ToList();
                    foreach(DbUsers u in ulist)
                    {
                        u.Mystudioid = us.Id;
                        u.Mystudioname = us.Userid;
                    }
                }else if(ul == 0 && us.Ulevel == 1)
                {
                   DbUsers jus = _dbConnect.DbUsers.FirstOrDefault(c => EF.Functions.Like(us.Repath, "%," + c.Id + ",%") && c.Ulevel == 1);
                    List<DbUsers> ulist = _dbConnect.DbUsers.Where(c => EF.Functions.Like(c.Repath, "%," + us.Id + ",%")).ToList();
                    foreach (DbUsers u in ulist)
                    {
                        u.Mystudioid = jus.Id;
                        u.Mystudioname = jus.Userid;
                    }
                    us.Mystudioid = jus.Id;
                    us.Mystudioname = jus.Userid;
                    us.StudioName = null;
                    us.StudioCard = null;

                }
                us.Ulevel = ul;
                _dbConnect.SaveChanges();
                _res.Done(null, "修改成功");
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 1, "修改会员画室:" + us.Userid, null, null, us.Id);
            }
            catch (Exception ex)
            {
                _res.Error("修改推荐人异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

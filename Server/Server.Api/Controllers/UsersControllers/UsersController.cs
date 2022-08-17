using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Crypto_Utils;
using Server.Utils.Http_Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static Server.Api.Filters;
using Server.Logs;
using Newtonsoft.Json.Linq;
using Server.Utils.Token_Utils;
using Server.Wallet.Utils;
using Server.Bill.Utils;
using Server.Api.Level;
using Server.Bonus.Utils;
using Server.Utils.Sms_Utils;
using Server.Api.Utils;

namespace Server.Api.Controllers.UsersControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private Result _res;
        public UsersController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 预登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result LoginBeforehand(JObject data)
        {
            string userid = Convert.ToString(data["userid"]);
            string token = Convert.ToString(data["token"]);

            if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(userid))
            {
                SystemSettingMethod ssm = new SystemSettingMethod(_dbConnect);
                DbSystemSetting ss = ssm.Get();
                if (ss.Switchsystem == 0)
                {
                    _res.Fail(ss.Systemclosemsg);
                    return _res;
                }
                _res = TokenUtils.Token_check(token, userid, HttpInfoUtils.GetIP(), HttpInfoUtils.GetOSVersion());
            }
            else
            {
                _res.Fail();
            }
            return _res;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result Login(JObject data)
        {


            /* if (!SignHelper.Check(Request.Form))
             {
                 _res.Fail("签名验证失败");

                 return _res;
             }*/

            //AES解密密码
            string password = Convert.ToString(data["password"]);
            string userid = Convert.ToString(data["userid"]);
            string studiocard = Convert.ToString(data["card"]);
            password = UsersUtils.Decode(password);

            SystemSettingMethod ssm = new SystemSettingMethod(_dbConnect);
            DbSystemSetting ss = ssm.Get();
            if (ss.Switchsystem == 0)
            {
                _res.Fail(ss.Systemclosemsg);
                return _res;
            }
            if (!string.IsNullOrWhiteSpace(userid) && !string.IsNullOrWhiteSpace(password))
            {
                string Passwordmd5 = MD5Utils.MD5Encrypt(password, 32);
                
                DbUsers users = _dbConnect.DbUsers.FirstOrDefault(u => u.Userid.Equals(userid) && u.Password.Equals(Passwordmd5));

                if (users != null)
                {
                    if(users.Islock == 1) { _res.Fail("账户已冻结，请联系管理员");return _res; }
                    if(users.Id > 1)
                    {
                        if(users.Ulevel == 0)
                        {
                            DbUsers reus = _dbConnect.DbUsers.FirstOrDefault(c => c.Id == users.Mystudioid);
                            if (!reus.StudioCard.Equals(studiocard)) { _res.Fail("所属画室编号错误"); return _res; }

                        }
                    }
                    

                    string Token = TokenUtils.Token_add(users.Id, users.Userid, 0, HttpInfoUtils.GetIP(), HttpInfoUtils.GetOSVersion());
                    if (Token != null && !Token.Equals(""))
                    {

                        List<Ulevel> uLevelList = new Ulevel().GetLevels();
                        List<ILevel> xLevelList = new Xlevel().GetLevels();

                        Dictionary<string, string> dic = new Dictionary<string, string>
                        {
                            { "token", Token },
                            { "id", users.Id.ToString() },
                            { "userid", users.Userid },
                            { "recode", users.Recode },
                            { "username", users.Username },
                            { "usertel", users.Usertel },
                            { "rdt", users.Rdt.ToString() },
                            { "pdt", users.Pdt.ToString() },
                            { "ulevel", users.Ulevel.ToString() },
                            { "ulevelname", uLevelList[users.Ulevel].Name },
                            { "ylevel", users.Ylevel.ToString() },
                            { "xlevel", users.Xlevel.ToString() },
                            { "xlevelname", xLevelList[users.Xlevel].Name },
                            { "lsk", users.Lsk.ToString() },
                            { "ylsk", users.Ylsk.ToString() },
                            { "ispay", users.Ispay.ToString() },
                            { "ispayname", UsersUtils.Getuserispayname(users.Ispay) },
                            { "islockname", UsersUtils.Getuserislockname(users.Islock) },
                            { "wxnickname", users.Wxnickname },
                            { "wxheadimgurl", users.Wxheadimgurl },
                            { "tx", users.Tx },
                            { "tjt", ss.Switchtjt.ToString() },
                            { "wlt", ss.Switchwlt.ToString() },
                            { "iswn", users.Iswn.ToString() }
                        };



                        _res.Done(dic, "查询成功");

                    }
                    else
                    {
                        _res.Fail("登录失败");

                    }
                }
                else
                {
                    _res.Fail("账号或密码错误");

                }
            }
            else
            {
                _res.Fail("用户名和密码不能留空");

            }

            return _res;
        }

        [HttpPost]
        public Result GetSystemSetting()
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
        /// 画室会员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result HsUser(JObject data)
        {
            string userid = Convert.ToString(data["userid"]);
            int uid = Convert.ToInt32(data["uid"]);
            try
            {
                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Id == uid && c.Ulevel >= 1);
                if (us == null) { _res.Fail("用户信息出错"); return _res; }
                List<DbUsers> ulist = _dbConnect.DbUsers.Where(c => c.Mystudioid == us.Id).ToList();
                _res.Done(ulist, "查询成功");
            }
            catch (Exception ex)
            {

                _res.Error("查询异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改预约
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Updateyy(JObject data)
        {
            string userid = Convert.ToString(data["userid"]);
            int uid = Convert.ToInt32(data["uid"]);
            try
            {
                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid) && c.Ulevel == 1);
                if (us == null) { _res.Fail("用户信息出错"); return _res; }
               
                if(us.Iswn == 1)
                {
                    us.Iswn = 0;
                }
                else
                {
                    us.Iswn = 1;
                }
                _dbConnect.SaveChanges();
                _res.Done(null, "修改成功");
            }
            catch (Exception ex)
            {

                _res.Error("修改预约异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Glpanel(JObject data)
        {
            string userid = Convert.ToString(data["userid"]);
            int uid = Convert.ToInt32(data["uid"]);
            try
            {
                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Userid.Equals(userid) && c.Ulevel == 1);
                if (us == null) { _res.Fail("用户信息出错"); return _res; }

                List<DbHold> hlist = _dbConnect.DbHold.Where(c => c.Hsuid == us.Id && c.State == 0 && c.Isdelete == 0 && c.Isfc == 0).ToList();
                List<DbHold> ylist = _dbConnect.DbHold.Where(c => c.Hsuid == us.Id && c.Qgdate.Date == DateTime.Now.Date && c.Isyy == 1&&c.Isdelete == 0).ToList();


                _res.Done(new
                {
                    isyy = us.Iswn,
                    dscount = hlist.Count,
                    yycount = ylist.Count

                }, "查询成功");
            }
            catch (Exception ex)
            {

                _res.Error("查询异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }


        /// <summary>
        /// 根据推广码查询服务中心和接点人,接点区域
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public Result Leader(JObject data)
        {

            try
            {
                string recode = Convert.ToString(data["recode"]);
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(u => u.Recode.Equals(recode));
                if (us == null) { _res.Fail("推荐人信息错误"); return _res; }

                Dictionary<string, string> dic = new Dictionary<string, string>();

                if (us.Isbd == 1)
                {
                    dic.Add("bdname", us.Userid);
                }
                else
                {
                    dic.Add("bdname", us.Bduserid);
                }

                DbUsersFteam uf = new DbUsersFteam();
                int Uid = us.Id;
                while (true)
                {
                    UsersFteamMethod ufm = new UsersFteamMethod(_dbConnect);
                    uf = ufm.GetByUid(Uid);
                    if (uf != null)
                    {
                        if (uf.Ch1 == 0)
                        {
                            dic.Add("fname", uf.Userid);
                            break;
                        }
                        else
                        {
                            Uid = uf.Ch1;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                diclist.Add(dic);
                _res.Done(diclist, "查询成功");

            }
            catch (Exception)
            {
                _res.Error("查询异常");

            }
            return _res;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result Register(JObject data)
        {

            try
            {
                string cacheToken = Convert.ToString(data["cacheToken"]);
                string recode = Convert.ToString(data["recode"]);
                string password = Convert.ToString(data["password"]);
                string password2 = Convert.ToString(data["password2"]);
                string usertel = Convert.ToString(data["usertel"]);
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();

                if (cacheToken == null) { _res.Fail("参数错误"); return _res; }
                if (RepeatedCheckUtils.Rc(cacheToken, 2)) { _res.Fail("请勿重复提交"); return _res; }
                if (string.IsNullOrWhiteSpace(recode)) { _res.Fail("请输入推广码"); return _res; }
                // if (string.IsNullOrWhiteSpace(userid)) { _res.Fail("请输入用户名"); return _res; }
                if (string.IsNullOrWhiteSpace(password)) { _res.Fail("请输入密码"); return _res; }
                if (string.IsNullOrWhiteSpace(password2)) { _res.Fail("请输入二级密码"); return _res; }
               // if (string.IsNullOrWhiteSpace(username)) { _res.Fail("请输入姓名"); return _res; }
                if (string.IsNullOrWhiteSpace(usertel)) { _res.Fail("请输入手机号码"); return _res; }
                


                Regex reg = new Regex(@"^[A-Za-z0-9]+$");
               // if (!reg.IsMatch(userid)) { _res.Fail("会员编号请输入英文+数字"); return _res; }
               // if (!reg.IsMatch(password)) { _res.Fail("密码请输入英文+数字"); return _res; }
                if (password.Length < 6 || password.Length > 12) { _res.Fail("登录密码请输入6-12位"); return _res; }
                if (password2.Length < 6 || password2.Length > 12) { _res.Fail("支付密码请输入6-12位"); return _res; }

                SystemSettingMethod ssm = new SystemSettingMethod(_dbConnect);
                DbSystemSetting ss = ssm.Get();
                //if(ss.Czbs == 1)
                //{
                //    string code = data["code"].ToString();
                //    if (string.IsNullOrWhiteSpace(code)) { _res.Code = 0; _res.Msg = "请输入验证码"; return _res; }
                //    if (!SmsUtils.Code(usertel, code)) { _res.Code = 0; _res.Msg = "验证码错误"; return _res; }
                //}

                DbUsers us = new DbUsers();
               
                UsersMethod um = new UsersMethod(_dbConnect);
                if (_dbConnect.DbUsers.Where(u => u.Userid.Equals(recode) || u.Recode.Equals(recode) || u.Usertel.Equals(recode)).Count() == 0) { _res.Fail("推荐人不存在"); return _res; }

                if (_dbConnect.DbUsers.Where(u => u.Usertel.Equals(usertel) ).Count() >= 1) { _res.Fail("该用户已存在"); return _res; }

                us.Userid = usertel;

                us.Recode = RandomUtils.StrRandom(6);
                while (um.GetList().FirstOrDefault(u => u.Userid.Equals(us.Recode) || u.Recode.Equals(us.Recode)) != null)
                {
                    us.Recode = RandomUtils.StrRandom(6);
                }

                string Passwordmd5 = MD5Utils.MD5Encrypt(password, 32);
                us.Password = Passwordmd5;

                string Password2md5 = MD5Utils.MD5Encrypt(password2, 32);
                us.Password2 = Password2md5;

                us.Tx = "tx.png";

                us.Username = "-";
                us.Usertel = usertel;

                us.Rdt = DateTime.Now;
                us.Pdt = DateTime.Now;

                us.Ulevel = 0;
                us.Ylevel = us.Ulevel;
                us.Xlevel = 0;

                us.Isbd = 0;
                us.Bdlevel = 0;
                us.Bddate = DateTime.Now;

                List<Ulevel> uLevelList = new Ulevel().GetLevels(_dbConnect);

                us.Lsk =0;
                us.Ylsk = us.Lsk;

                us.Dan = 0;
                us.Tdan = 0;
                us.Pv = 0;
                us.Ispay = 1;//激活状态
                us.Islock = 0;

                DbUsers reur = um.GetList().FirstOrDefault(r => r.Recode.Equals(recode) || r.Userid.Equals(recode));
                if (reur != null)
                {
                   // if (reur.Ispay > 0)
                   // {
                        us.Reid = reur.Id;
                        us.Rename = reur.Userid;
                        us.Reusername = reur.Username;
                        us.Repath = reur.Repath + reur.Id + ",";
                        us.Relevel = reur.Relevel + 1;
                        us.Recount = 0;
                        us.Teamcount = 0;
                        us.Reyeji = 0;
                        us.Teamyeji = 0;
                    //}
                    //else
                    //{
                    //    _res.Fail("推荐人未激活,无法推荐");

                    //    return _res;
                    //}
                }
                else
                {
                    _res.Fail("推荐人不存在");

                    return _res;
                }

                us.Daquyeji = 0;
                us.Xiaoquyeji = 0;

                us.Bdid = 1;
                us.Bduserid = "admin";
                us.Bdusername = "管理员";

                us.Iswx = 0;
                us.Wxtoken = "";

                us.Msgdate = DateTime.Parse("1970-01-01");
                UsersFteamMethod ufm = new UsersFteamMethod(_dbConnect);

            
                WalletsCoinMethod wcm = new WalletsCoinMethod(_dbConnect);
                List<DbWalletsCoin> clist = _dbConnect.DbWalletsCoin.Where(c=>c.State == 1).ToList();
                foreach (DbWalletsCoin c in clist)
                {
                    DbWallets uw = new DbWallets
                    {
                        Uid = us.Id,
                        Userid = us.Userid,
                        Cid = c.Id,
                        Cname = c.Codename,
                        CnameZh = c.Coinname,
                        Jine = 0,
                        Wdate = DateTime.Now
                    };

                    us.DbWallets.Add(uw);
                }

                um.Add(us);

               List<DbUsers> hus = _dbConnect.DbUsers.Where(c => EF.Functions.Like(us.Repath, "%," + c.Id + ",%") && c.Ulevel == 1).OrderByDescending(c=>c.Relevel).ToList();
                if(hus.Count != 0)
                {
                    us.Mystudioid = hus[0].Id;
                    us.Mystudioname = hus[0].Userid;
                }

                Dictionary<string, decimal> bonusDic = SystemSettingBonusUtils.GetBonusParameter(_dbConnect);

                if (_dbConnect.SaveChanges() > 0)
                {
                    YejiUtils ym = new YejiUtils(_dbConnect);
                    ym.AddUser(1, 0);
                    //尾部参数0为首次激活,升级页面,购物页面传1
                    UsersUtils.AddReteamYeji(us.Reid, us.Repath, 0, 0, 0, _dbConnect);

                   // string repath = "," + us.Id + us.Repath;
                   // new Xlevel().LevelUp(_dbConnect, repath);

                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "userid", us.Userid },
                        { "username", us.Username },
                        { "usertel", us.Usertel }
                    };
                    diclist.Add(dic);
                    _res.Done(diclist, "注册成功");
                }
                else
                {
                    _res.Fail("会员信息写入失败");

                }

            }
            catch (Exception ex)
            {
                _res.Error("添加用户异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 激活会员
        /// </summary>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Jihuo(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                int cid = Convert.ToInt32(data["cid"]);


                if (RepeatedCheckUtils.Rc(userid, 2)) { _res.Fail("请勿重复提交"); return _res; }

                using var transaction = _dbConnect.Database.BeginTransaction();

                WalletsMethod wm = new WalletsMethod(_dbConnect);
                DbWallets uw = wm.GetByUserid(userid);
                if (uw == null) { _res.Fail("用户信息错误"); return _res; }

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers cur = _dbConnect.DbUsers.FirstOrDefault(u => u.Id == cid && (u.Reid == uw.Uid || u.Bdid == uw.Uid));//被激活会员
                if (cur == null) { _res.Fail("会员不存在"); return _res; }

                if (cur.Ispay == 1) { _res.Fail("该会员已激活,请勿重复激活"); return _res; }

                Result res = WalletsUtils.PayBalance(uw.Uid, uw.Cid, cur.Lsk, _dbConnect);
                if (res.Code == 0) { return res; }

                //创建账单
                IBill bill = new BillPay();
                bill.Create(uw.Uid, new Dictionary<int, decimal>
                    {
                        {uw.Cid,cur.Lsk}
                    }, _dbConnect, "升级");

                //写入激活记录
                DbUsersJihuoRecord fjr = new DbUsersJihuoRecord
                {
                    Uid = uw.Uid,
                    Userid = uw.Userid,
                    Username = uw.UidNavigation.Username,
                    Hblx = 2,
                    Jine = cur.Lsk,
                    Cid = uw.Cid,
                    Codename = uw.Cname,
                    Coinname = uw.CnameZh,
                    Jid = cur.Id,
                    Juserid = cur.Userid,
                    Jusername = cur.Username,
                    Jdate = DateTime.Now
                };
                UsersJihuoRecordMethod ujrm = new UsersJihuoRecordMethod(_dbConnect);
                ujrm.Add(fjr);

                cur.Jid = uw.Uid;
                cur.Juserid = uw.Userid;
                cur.Jusername = uw.UidNavigation.Username;

                _res.Done(null, UsersUtils.Jihuo(uw.Cid, cur.Id, 0, uw.Userid));
                _dbConnect.SaveChanges();
                transaction.Commit();

            }
            catch (Exception ex)
            {
                _res.Error("激活会员异常");
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
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Delete(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                string delete_id = Convert.ToString(data["delete_id"]);
                int id = Convert.ToInt32(data["id"]);
                string Msg = "";
                string[] Dllist = delete_id.Split(',');
                int I = 0;
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    if (I > 0) { Msg += ","; }
                    Msg += UsersUtils.Delete(Id, id, userid, 1);
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
        /// 修改用户数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Update(JObject data)
        {

            try
            {
                string update_username = Convert.ToString(data["update_username"]);
               // string update_usertel = Convert.ToString(data["update_usertel"]);
                string userid = Convert.ToString(data["userid"]);
                UsersMethod um = new UsersMethod(_dbConnect);
                if (!update_username.Equals(""))
                {
                    DbUsers users = um.GetByUsersid(userid);
                    users.Username = update_username;
                   // users.Usertel = update_usertel;

                    _dbConnect.SaveChanges();
                    _res.Done(null, "修改成功1");
                }
                else
                {
                    _res.Fail("用户信息不能为空");

                }
            }

            catch (Exception ex)
            {
                _res.Error("修改用户数据异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public Result RetrievePassword(JObject data)
        {


            try
            {
                string userid = Convert.ToString(data["userid"]);
                string password = Convert.ToString(data["password"]);
                string usertel = Convert.ToString(data["usertel"]);
                string yanzhengma = Convert.ToString(data["yanzhengma"]);

                int lx = Convert.ToInt32(data["lx"]);

               // if (string.IsNullOrWhiteSpace(userid)) { _res.Fail("请输入用户名"); return _res; }
                Regex reg = new Regex(@"^[A-Za-z0-9]+$");
               // if (!reg.IsMatch(password)) { _res.Fail("密码请输入英文+数字"); return _res; }
                if (password.Length < 6 || password.Length > 12) { _res.Fail("密码请输入6-12位"); return _res; }
                if (string.IsNullOrWhiteSpace(usertel)) { _res.Fail("请输入手机号码"); return _res; }
                if (string.IsNullOrWhiteSpace(yanzhengma)) { _res.Fail("请输入验证码"); return _res; }

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(u => u.Usertel == usertel);
                if (us == null) { _res.Fail("用户不存在"); return _res; }

                DbCheckcode vc = _dbConnect.DbCheckcode.FirstOrDefault(v => v.Usertel.Equals(usertel) && v.Code.Equals(yanzhengma));
                if (vc == null) { _res.Code = 0; _res.Msg = "验证码错误"; return _res; }
                //验证通过后移除记录
                _dbConnect.DbCheckcode.Remove(vc);

                string Passwordmd5_new = MD5Utils.MD5Encrypt(password, 32);
                if(lx == 1)
                {
                    us.Password = Passwordmd5_new;
                }else if(lx == 2)
                {
                    us.Password2 = Passwordmd5_new;

                }
                else
                {
                    return _res.Fail("未知类型"); 
                }
               


                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "修改成功");

                }
            }
            catch (Exception ex)
            {
                _res.Error("找回密码异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改密码s
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Update_Password(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                string password_old = Convert.ToString(data["password_old"]);
                string password_new = Convert.ToString(data["password_new"]);
                UsersMethod um = new UsersMethod(_dbConnect);
                if (PublicUtils.NotNull(password_old) && PublicUtils.NotNull(password_new))
                {
                    DbUsers users = um.GetByUsersid(userid);
                    if (users == null) { _res.Fail("用户信息丢失,请重试"); return _res; }
                    string Passwordmd5_old = MD5Utils.MD5Encrypt(password_old, 32);
                    if (users.Password.Equals(Passwordmd5_old))
                    {
                        string Passwordmd5_new = MD5Utils.MD5Encrypt(password_new, 32);
                        users.Password = Passwordmd5_new;
                        if (_dbConnect.SaveChanges() > 0)
                        {
                            _res.Done(null, "修改成功");

                        }
                        else
                        {
                            _res.Fail("修改失败");

                        }
                    }
                    else
                    {
                        _res.Fail("原密码错误");

                    }
                }
                else
                {
                    _res.Fail("密码不能为空");

                }
            }
            catch (Exception ex)
            {
                _res.Error("修改用户密码异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改二级密码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Update_Password2(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                string password2_old = Convert.ToString(data["password2_old"]);
                string password2_new = Convert.ToString(data["password2_new"]);
                UsersMethod um = new UsersMethod(_dbConnect);
                if (PublicUtils.NotNull(password2_old) && PublicUtils.NotNull(password2_new))
                {
                    DbUsers users = um.GetByUsersid(userid);
                    if (users == null) { _res.Fail("用户信息丢失,请重试"); return _res; }
                    string Passwordmd5_old = MD5Utils.MD5Encrypt(password2_old, 32);
                    if (users.Password2.Equals(Passwordmd5_old))
                    {
                        string Passwordmd5_new = MD5Utils.MD5Encrypt(password2_new, 32);
                        users.Password2 = Passwordmd5_new;
                        if (_dbConnect.SaveChanges() > 0)
                        {
                            _res.Done(null, "修改成功");
                        }
                        else
                        {
                            _res.Fail("修改失败");

                        }
                    }
                    else
                    {
                        _res.Fail("原密码错误");

                    }
                }
                else
                {
                    _res.Fail("密码不能为空");

                }
            }
            catch (Exception ex)
            {
                _res.Error("修改支付密码异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Update_Tx(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                string tx = Convert.ToString(data["tx"]);
                UsersMethod um = new UsersMethod(_dbConnect);
                if (!string.IsNullOrWhiteSpace(tx))
                {

                    DbUsers users = um.GetByUsersid(userid);
                    if (users == null) { _res.Fail("用户信息出错"); return _res; }

                    string Ytx = users.Tx;

                    users.Tx = tx;
                    if (_dbConnect.SaveChanges() > 0)
                    {
                        //修改成功时删除原头像文件
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload");
                        DirectoryInfo folder = new DirectoryInfo(path);
                        foreach (FileInfo fileinfo in folder.GetFiles(Ytx))
                        {
                            fileinfo.Delete();
                        }
                    }
                    _res.Done(null, "修改成功");

                }
                else
                {
                    _res.Fail("头像不能为空");

                }
            }
            catch (Exception ex)
            {
                _res.Error("修改用户数据异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取所有会员与各字段信息
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

                int jhispay = Convert.ToInt32(data["jhispay"]);
               
                List<DbUsers> urlist = new List<DbUsers>();
                UsersMethod um = new UsersMethod(_dbConnect);
                if (jhispay == 1)
                {
                    urlist = um.GetList().Where(u => u.Ispay == 0 && (u.Userid.Equals(userid) || u.Rename.Equals(userid) || u.Bduserid.Equals(userid))).ToList();
                }
                else if (jhispay == 2)
                {
                    urlist = um.GetList().Where(u => u.Ispay == 1 && (u.Userid.Equals(userid) || u.Rename.Equals(userid) || u.Bduserid.Equals(userid))).ToList();
                }
                else if (jhispay == 3)
                {
                    urlist = um.GetList().Where(u => u.Ispay == 1 && u.Juserid!= null && u.Juserid.Equals(userid)).ToList();
                 }

                UsersJihuoRecordMethod ujrm = new UsersJihuoRecordMethod(_dbConnect);
                List<Ulevel> uLevelList = new Ulevel().GetLevels();
                var list = urlist.Select(u => new
                {
                    id = u.Id,
                    userid = u.Userid,
                    username = u.Username,
                    usertel = u.Usertel,
                    rdt = u.Rdt,
                    pdt = u.Pdt,
                    ulevelus = u.Ulevel,
                    ulevelname = uLevelList[u.Ulevel].Name,
                    rename = u.Rename,
                    lsk = u.Lsk,
                    ylsk = u.Ylsk,
                    ispay = u.Ispay,
                    ispayname = UsersUtils.Getuserispayname(u.Ispay),
                    islock = u.Islock,
                    islockname = UsersUtils.Getuserislockname(u.Islock),
                    jine= ujrm.GetByJid(u.Id) == null ? "-" : ujrm.GetByJid(u.Id).Jine.ToString()
                });
                _res.Done(list, "查询成功");

            }
            catch (Exception ex)
            {
                _res.Error("查询用户信息异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询用户个人信息
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
                Dictionary<string, string> dic = new Dictionary<string, string>();
                SystemSettingMethod ssm = new SystemSettingMethod(_dbConnect);
                DbSystemSetting ss = ssm.Get();

                //获取开关信息
                dic.Add("tjt", ss.Switchtjt.ToString());
                dic.Add("wlt", ss.Switchwlt.ToString());
                dic.Add("marqueemsg", ss.Marqueemsg.ToString());

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers us = um.GetByUsersid(userid);  //获取会员信息
                if (us != null)
                {
                    //获取个人信息
                    dic.Add("id", us.Id.ToString());
                    dic.Add("userid", us.Userid);
                    dic.Add("recode", us.Recode);
                    dic.Add("username", us.Username);
                    dic.Add("usertel", us.Usertel);
                    dic.Add("rdt", us.Rdt.ToString());
                    dic.Add("pdt", us.Pdt.ToString());
                    dic.Add("wxnickname", us.Wxnickname);
                    dic.Add("wxheadimgurl", us.Wxheadimgurl);
                    dic.Add("tx", us.Tx);
                    dic.Add("ylevel", us.Ylevel.ToString());
                    dic.Add("ylsk", us.Ylsk.ToString());
                    dic.Add("lsk", us.Lsk.ToString());
                    dic.Add("rename", us.Rename);
                    //dic.Add("maxmey", us.Userswallet.FirstOrDefault().Maxmey.ToString());
                    dic.Add("isrz", us.Isrz.ToString());
                    dic.Add("ispay", us.Ispay.ToString());
                    dic.Add("ispayname", UsersUtils.Getuserispayname(us.Ispay));
                    dic.Add("islockname", UsersUtils.Getuserislockname(us.Islock));
                    dic.Add("shouru", us.Shouru.ToString());
                    dic.Add("studioname", us.StudioName);
                    dic.Add("studiocard", us.StudioCard);

                    if(us.Mystudioid != 0)
                    {
                        DbUsers hus = _dbConnect.DbUsers.FirstOrDefault(c => c.Id == us.Mystudioid);
                        if(hus != null)
                        {
                            dic.Add("mystudiocard", hus.StudioCard);
                            dic.Add("mystudioname", hus.StudioName);
                        }
                    }
                   

                    //获取级别信息
                    List<Ulevel> uLevelList = new Ulevel().GetLevels();
                    List<ILevel> xLevelList = new Xlevel().GetLevels();
                    List<ILevel> bdLevelList = new Bdlevel().GetLevels();
                    dic.Add("ulevel", us.Ulevel.ToString());
                    dic.Add("ulevelname", uLevelList[us.Ulevel].Name);
                    dic.Add("xlevel", us.Xlevel.ToString());
                    dic.Add("xlevelname", xLevelList[us.Xlevel].Name);
                    dic.Add("isbd", us.Isbd.ToString());
                    dic.Add("bdlevel", us.Bdlevel.ToString());
                    dic.Add("bdlevelname", bdLevelList[us.Bdlevel].Name);

                    MsgMethod mm = new MsgMethod(_dbConnect);
                    List<DbMsg> msglist = mm.GetList().Where(m => m.Sid == us.Id && m.Isread == 0 && m.Sisdelete == 0).ToList();
                    dic.Add("msgcount", msglist.Count.ToString());

                }

                _res.Done(dic, "查询成功");

            }
            catch (Exception ex)
            {
                _res.Error("查询Home信息异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 推荐图谱
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Tree(JObject data)
        {

            try
            {
                string userid = Convert.ToString(data["userid"]);
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<TreeMod> rtlist = new List<TreeMod>();

                SystemSettingMethod ssm = new SystemSettingMethod(_dbConnect);
                DbSystemSetting ss = ssm.GetById(1);
                if (ss.Switchtjt == 0) { _res.Fail("暂未开放"); return _res; }

                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers us = um.GetByUsersid(userid);
                if (us == null) { _res.Fail("未找到该会员"); return _res; }

                List<Ulevel> uLevelList = new Ulevel().GetLevels();
                List<ILevel> xLevelList = new Xlevel().GetLevels();

                string uxName = " [";
                
                //if (us.Ulevel > 0)
               // {
                    uxName += "  " + uLevelList[us.Ulevel].Name;
                // }
                //if (us.Xlevel > 0)
                //{
                //    uxName += "  " + xLevelList[us.Xlevel].Name;
                //}
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
                    Riteamyeji = us.Riteamyeji,
                    Lsk = us.Lsk,
                    
                };
                rt = UsersUtils.Tree(_dbConnect, us, rt);

                rtlist.Add(rt);

                string Rtjson = JsonConvert.SerializeObject(rtlist);
                dic.Add("tree", Rtjson);
                dic.Add("tjt", ss.Switchtjt.ToString());

                diclist.Add(dic);

                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 网络图谱
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Network(JObject data)
        {

            try
            {

                string userid = Convert.ToString(data["userid"]);
                int areacount = Convert.ToInt32(data["areacount"]);
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<NetworkMod> rtlist = new List<NetworkMod>();

                SystemSettingMethod ssm = new SystemSettingMethod(_dbConnect);
                DbSystemSetting ss = ssm.GetById(1);
                if (ss.Switchwlt == 0) { _res.Fail("暂未开放"); return _res; }

                UsersFteamMethod ufm = new UsersFteamMethod(_dbConnect);
                DbUsersFteam uf = ufm.GetByUserid(userid);
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

                    diclist.Add(dic);

                    _res.Done(diclist, "查询成功");
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
        /// 设置画室
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result UpdateStudio(JObject data)
        {

            try
            {
                string name = Convert.ToString(data["name"]);
                string card = Convert.ToString(data["card"]);
                int uid = Convert.ToInt32(data["uid"]);

                DbUsers us = _dbConnect.DbUsers.FirstOrDefault(c => c.Id == uid);
                if(us.Ulevel < 1) { return _res.Fail("暂时没有资格设置"); }
                DbUsers usname = _dbConnect.DbUsers.FirstOrDefault(c=>c.Ulevel == 1 && c.Id != uid && c.StudioName.Equals(name));
                if (usname != null) { return _res.Fail("画室名已存在"); }
                DbUsers uscard = _dbConnect.DbUsers.FirstOrDefault(c => c.Ulevel == 1&& c.Id != uid && c.StudioCard.Equals(card));
                if (uscard != null) { return _res.Fail("画室编号已存在"); }

                us.StudioCard = card;
                us.StudioName = name;
                us.Mystudioid = uid;
                us.Mystudioname = us.Userid;
                _dbConnect.SaveChanges();
                _res.Done(null, "设置成功");

            }

            catch (Exception ex)
            {
                _res.Error("修改用户数据异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

    }
}

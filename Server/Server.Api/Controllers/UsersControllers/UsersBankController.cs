using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.UsersControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersBankController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public UsersBankController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 添加银行账户
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
                string add_bankname = Convert.ToString(data["add_bankname"]);
                string add_bankcard = Convert.ToString(data["add_bankcard"]);
                string add_username = Convert.ToString(data["add_username"]);
                string add_usertel = Convert.ToString(data["add_usertel"]);
                string userid = Convert.ToString(data["userid"]);
                string add_bankimg = Convert.ToString(data["add_bankimg"]);
              
                if (!(PublicUtils.NotNull(add_bankname) && PublicUtils.NotNull(add_bankcard) && PublicUtils.NotNull(add_username) && PublicUtils.NotNull(add_usertel)))
                {
                    _res.Fail("请将账户信息填写完整");
                   
                    return _res;
                }
                UsersMethod um = new UsersMethod(_dbConnect);
                DbUsers users = um.GetByUsersid(userid);     //查询用户是否存在
                if (users == null) { _res.Fail("用户不存在");  return _res; }

                UsersBankMethod ubm = new UsersBankMethod(_dbConnect);
                int Ubcount = ubm.GetUseridList(userid).Count();
                if (Ubcount >= 6) { _res.Fail("账户数量已达上限");  return _res; }

                List<DbUsersBank> ubk =ubm.GetUidList(users.Id).ToList();

                DbUsersBank ub = new DbUsersBank
                {
                    Uid = users.Id,
                    Userid = userid,
                    Username = add_username,
                    Usertel = add_usertel,
                    Bankname =add_bankname,
                    Bankcard = add_bankcard,
                    Bankaddress = "",
                    Bdate = DateTime.Now,
                    Bankimg = add_bankimg,

                    Isdefault = 0
                };//添加账户
                if (ubk.Count == 0)
                {
                    ub.Isdefault = 1;
                }

               ubm.Add(ub);
                _dbConnect.SaveChanges();
                _res.Done(null, "账户添加成功");
            }
            catch (Exception ex)
            {
                _res.Error("添加账户异常");
             
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除银行账户
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
                int delete_id = Convert.ToInt32(data["delete_id"]);
                int delete_uid = Convert.ToInt32(data["delete_uid"]);
                UsersBankMethod ubm = new UsersBankMethod(_dbConnect);
                DbUsersBank ub = _dbConnect.DbUsersBank.FirstOrDefault(n => n.Id == delete_id && n.Uid == delete_uid);
                if (ub != null) //判断条数
                {
                   ubm.Remove(ub.Id);//删除
                    if (_dbConnect.SaveChanges() > 0)
                    {
                        _res.Done(null, "删除成功");
                    }
                    else
                    {
                        _res.Fail("删除失败");
                    }
                }
                else
                {
                    _res.Fail("删除失败");
                }
            }
            catch (Exception ex)
            {
                _res.Error("删除数据异常");
             
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改银行账户
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Update(JObject data)
        {
            
            try
            {
                string userid = Convert.ToString(data["userid"]);
                string up_bankname = Convert.ToString(data["up_bankname"]);
                string up_bankcard = Convert.ToString(data["up_bankcard"]);
                string up_username = Convert.ToString(data["up_username"]);
                string up_usertel = Convert.ToString(data["up_usertel"]);
                string up_bankimg = Convert.ToString(data["up_bankimg"]);
                int up_id = Convert.ToInt32(data["up_id"]);
                int up_uid = Convert.ToInt32(data["up_uid"]);
               
                
                if (PublicUtils.NotNull(up_bankname) && PublicUtils.NotNull(up_bankcard) && PublicUtils.NotNull(up_username) && PublicUtils.NotNull(up_usertel))
                {
                  
                    DbUsersBank ub = _dbConnect.DbUsersBank.FirstOrDefault(u => u.Id ==up_id && u.Uid == up_uid);
                    if (ub != null)
                    {
                        ub.Username = up_username;
                        ub.Usertel = up_usertel;
                        ub.Bankname = up_bankname;
                        ub.Bankcard = up_bankcard;
                        ub.Bankimg = up_bankimg;
                        _dbConnect.SaveChanges();//保存结果

                        _res.Done(null, "修改成功");
                    }
                    else
                    {
                        _res.Fail("账户信息错误");
                        
                    }
                }
                else
                {
                    _res.Fail("请将账户内容填写完整");
                    
                }
            }
            catch (Exception ex)
            {
                _res.Error("修改数据异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 设置默认账户
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Default(JObject data)
        {
            
            try
            {

                string userid = Convert.ToString(data["userid"]);
                int set_uid = Convert.ToInt32(data["set_uid"]);
                int set_id = Convert.ToInt32(data["set_id"]);
                UsersBankMethod ubm = new UsersBankMethod(_dbConnect);
                List<DbUsersBank> ublist = ubm.GetUidList(set_uid);
                foreach (DbUsersBank ub in ublist)
                {
                    if (ub.Id == set_id)
                    {
                        ub.Isdefault = 1;
                    }
                    else
                    {
                        ub.Isdefault = 0;
                    }
                }
                _dbConnect.SaveChanges();
                _res.Done(null, "设置默认账户成功");
                
            }
            catch (Exception ex)
            {
                _res.Error("设置默认账户异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询用户账户集
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
                UsersBankMethod ubm = new UsersBankMethod(_dbConnect);
                List<DbUsersBank> ublist =ubm.GetList().OrderByDescending(m => m.Isdefault).ThenByDescending(m => m.Id).Where(m => m.Uid == select_uid).ToList();
                _res.Done(ublist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询数据异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询单条账户信息
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
                int select_id = Convert.ToInt32(data["select_id"]);
                int select_uid = Convert.ToInt32(data["select_uid"]);
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                Dictionary<string, string> dic = new Dictionary<string, string>();
              
                DbUsersBank ub = _dbConnect.DbUsersBank.FirstOrDefault(u => u.Id == select_id && u.Uid == select_uid);
                if (ub != null)
                {
                    _res.Done(null, "查询成功");
                    dic.Add("id", ub.Id.ToString());
                    dic.Add("uid", ub.Uid.ToString());
                    dic.Add("userid", ub.Userid);
                    dic.Add("username", ub.Username);
                    dic.Add("usertel", ub.Usertel);
                    dic.Add("bankname", ub.Bankname);
                    dic.Add("bankcard", ub.Bankcard);
                    dic.Add("bankimg", ub.Bankimg);
                    diclist.Add(dic);
                    _res.Done(diclist, "查询成功");
                }
                else
                {
                    _res.Fail("查询无数据");
                   
                }
            }
            catch (Exception ex)
            {
                _res.Error("查询账户异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取会员可选账户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string[] Select()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] bank = { };
            try
            {
                SystemSettingMethod ssm = new SystemSettingMethod(_dbConnect);
                DbSystemSetting ss = ssm.GetById(1);  
                bank = ss.Bank.Split(',');
            }
            catch (Exception ex)
            {
                _res.Error("查询异常");
               
                NLogHelper._.Error("查询会员可选账户出错", ex);
            }
            return bank;
        }
    }
}

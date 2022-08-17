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
    public class UsersAddressController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public UsersAddressController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 添加地址
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
                string add_sheng = Convert.ToString(data["add_sheng"]);
                string add_shi = Convert.ToString(data["add_shi"]);
                string add_xian = Convert.ToString(data["add_xian"]);
                string add_address = Convert.ToString(data["add_address"]);
                string add_username = Convert.ToString(data["add_username"]);
                string add_usertel = Convert.ToString(data["add_usertel"]);
                string userid = Convert.ToString(data["userid"]);
                bool isdefault = Convert.ToBoolean(data["isdefault"]);
                string areaCode = Convert.ToString(data["areaCode"]);
                UsersMethod um = new UsersMethod(_dbConnect);
                if (PublicUtils.NotNull(add_sheng) && PublicUtils.NotNull(add_shi) && PublicUtils.NotNull(add_xian) && PublicUtils.NotNull(add_address) && PublicUtils.NotNull(add_username) && PublicUtils.NotNull(add_usertel))   //判断输入值
                {

                    DbUsers users = um.GetByUsersid(userid);     //查询用户是否存在
                    if (users != null)
                    {
                        int Ismr = 0;
                        if (isdefault)
                        {
                            Ismr = 1;
                        }
                        else
                        {
                            Ismr = 0;
                        }
                        DbUsersAddress tk = new DbUsersAddress()//添加地址
                        {
                            Uid = users.Id,
                            Userid = userid,
                            Username = add_username,
                            Usertel = add_usertel,
                            Sheng = add_sheng,
                            Shi = add_shi,
                            Xian = add_xian,
                            Address = add_address,

                            Isdefault = Ismr,
                            Odate = DateTime.Now,
                            AreaCode = areaCode
                        };
                        UsersAddressMethod uam = new UsersAddressMethod(_dbConnect);
                        uam.Add(tk);
                        _dbConnect.SaveChanges();
                        if (tk.Isdefault == 1)
                        {
                            List<DbUsersAddress> ualist =uam.GetUidList(users.Id);
                            foreach (DbUsersAddress ua in ualist)
                            {
                                if (ua.Id == tk.Id)
                                {
                                    ua.Isdefault = 1;
                                }
                                else
                                {
                                    ua.Isdefault = 0;
                                }
                            }
                            _dbConnect.SaveChanges();
                        }


                        _res.Done(null, "地址添加成功");
                    }
                    else
                    {
                        _res.Fail("用户不存在");
                    }
                }
                else
                {
                    _res.Fail("请将地址信息填写完整");
                   
                }
            }
            catch (Exception ex)
            {
                _res.Error("添加地址异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除地址
        /// </summary>
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
                UsersAddressMethod uam = new UsersAddressMethod(_dbConnect);
                DbUsersAddress ua = _dbConnect.DbUsersAddress.FirstOrDefault(a => a.Id == delete_id && a.Userid.Equals(userid));
                if (ua != null) //判断条数
                {
                    uam.Remove(ua.Id);//删除
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
        /// 修改地址
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
                string sheng = Convert.ToString(data["sheng"]);
                string shi = Convert.ToString(data["shi"]);
                string xian = Convert.ToString(data["xian"]);
                string address = Convert.ToString(data["address"]);
                string username = Convert.ToString(data["username"]);
                string usertel = Convert.ToString(data["usertel"]);
                string areaCode = Convert.ToString(data["areaCode"]);
                bool isdefault = Convert.ToBoolean(data["isdefault"]);
                int aid = Convert.ToInt32(data["aid"]);
                if (PublicUtils.NotNull(sheng) && PublicUtils.NotNull(shi) && PublicUtils.NotNull(xian) && PublicUtils.NotNull(address) && PublicUtils.NotNull(username) && PublicUtils.NotNull(usertel))
                {

                   
                    DbUsersAddress up_ua = _dbConnect.DbUsersAddress.FirstOrDefault(u => u.Id == aid && u.Userid.Equals(userid));
                    if (up_ua == null) { _res.Fail("地址信息错误");return _res; }
                    up_ua.Username = username;
                    up_ua.Usertel = usertel;
                    up_ua.Sheng = sheng;
                    up_ua.Shi = shi;
                    up_ua.Xian = xian;
                    up_ua.Address = address;
                    up_ua.AreaCode = areaCode;

                    if (isdefault)
                    {
                        up_ua.Isdefault = 1;
                        List<DbUsersAddress> ualist = _dbConnect.DbUsersAddress.Where(u => u.Userid.Equals(userid)).ToList();
                        foreach (DbUsersAddress ua in ualist)
                        {
                            if (ua.Id == aid)
                            {
                                ua.Isdefault = 1;
                            }
                            else
                            {
                                ua.Isdefault = 0;
                            }
                        }
                    }
                    else
                    {
                        up_ua.Isdefault = 0;
                    }
                    _dbConnect.SaveChanges();//保存结果

                    _res.Done(null, "修改成功");
                }
                else
                {
                    _res.Fail("请将地址内容填写完整");
                   
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
        /// 设置默认地址
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result Default(JObject data)
        {
            try
            {
                int set_uid = Convert.ToInt32(data["set_uid"]);
                int set_id = Convert.ToInt32(data["set_id"]);
                UsersAddressMethod uam = new UsersAddressMethod(_dbConnect);
                List<DbUsersAddress> ualist =uam.GetUidList(set_uid);
                foreach (DbUsersAddress ua in ualist)
                {
                    if (ua.Id == set_id)
                    {
                        ua.Isdefault = 1;
                    }
                    else
                    {
                        ua.Isdefault = 0;
                    }
                }
                _dbConnect.SaveChanges();
                _res.Done(null, "设置默认地址成功");
               
            }
            catch (Exception ex)
            {
                _res.Error("设置默认地址异常");
             
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询用户地址集
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
                int uid = Convert.ToInt32(data["uid"]);
                UsersAddressMethod uam = new UsersAddressMethod(_dbConnect);
                List<DbUsersAddress> uaList = uam.GetList();
                uaList = uaList.OrderByDescending(m => m.Isdefault).ThenByDescending(m => m.Id).Where(m => m.Uid == uid).ToList();
                _res.Done(uaList, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询数据异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询单条地址信息
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
                int aid = Convert.ToInt32(data["aid"]);
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                Dictionary<string, string> dic = new Dictionary<string, string>();
               
                DbUsersAddress ua = _dbConnect.DbUsersAddress.FirstOrDefault(u => u.Id == aid && u.Userid.Equals(userid));
                if (ua != null)
                {
                    _res.Done(null, "查询成功");
                    dic.Add("id", ua.Id.ToString());
                    dic.Add("uid", ua.Uid.ToString());
                    dic.Add("userid", ua.Userid);
                    dic.Add("username", ua.Username);
                    dic.Add("usertel", ua.Usertel);
                    dic.Add("sheng", ua.Sheng);
                    dic.Add("shi", ua.Shi);
                    dic.Add("xian", ua.Xian);
                    dic.Add("address", ua.Address);
                    dic.Add("areaCode", ua.AreaCode);
                    dic.Add("isdefault", ua.Isdefault.ToString());

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
                _res.Error("查询地址异常");
             
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

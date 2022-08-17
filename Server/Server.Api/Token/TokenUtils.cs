using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;

namespace Server.Utils.Token_Utils
{
    public static class TokenUtils
    {
        public static string Token_add(int Uid, string Userid, int Isa, string Ip, string Os)
        {
            string Token = "";
            using (DbConnect dbConnect = DbConnectUtils.GetDbContext())
            {
                try
                {
                    //生成token
                    TokenMod tm = new TokenMod
                    {
                        Ip = Ip,
                        Os = Os,
                        Userid = Userid
                    };
                    string Token_str = tm.Get_Encrypt_Token(tm);
                    //写入token
                    DbToken tk = new DbToken
                    {
                        Uid = Uid,
                        Userid = Userid,
                        Tokenstr = Token_str,
                        Odate = DateTime.Now.AddDays(3),
                        Isa = Isa
                    };
                    dbConnect.DbToken.Add(tk);
                    if (dbConnect.SaveChanges() > 0)
                    {
                        Token = Token_str;
                    }
                }
                catch (Exception ex)
                {
                    NLogHelper._.Error("写入token出错", ex);
                }
            }
            return Token;
        }

        public static Result Token_check(string Token_str, string Userid, string Ip, string Os)
        {
            Result res = new Result();

            if (Token_str is null)
            {
                throw new ArgumentNullException(nameof(Token_str));
            }

            using (DbConnect dbConnect = DbConnectUtils.GetDbContext())
            {
                try
                {
                    DbSystemSetting ss = dbConnect.DbSystemSetting.FirstOrDefault();
                    if (ss == null)
                    {
                        res.Code = -1;
                        res.Msg = "获取系统信息出错";
                        return res;
                    }
                    if (ss.Switchsystem == 0)
                    {
                        res.Code = -1;
                        res.Msg = ss.Systemclosemsg;
                        return res;
                    }

                    DateTime Now = DateTime.Now;
                    if (!(Now.Hour >= ss.Timestart && Now.Hour <= ss.Timeend))
                    {
                        res.Code = -1;
                        res.Msg = ss.Timeclosemsg;
                        return res;
                    }

                    if (string.IsNullOrWhiteSpace(Token_str))
                    {
                        res.Code = -1;
                        res.Msg = "登录信息失效,请重新登录";
                        return res;
                    }

                    TokenMod tkm = new TokenMod
                    {
                        Userid = Userid,
                        Ip = Ip,
                        Os = Os
                    };
                    string Nowtoken = tkm.Get_Encrypt_Token(tkm);
                    if (!Token_str.Equals(Nowtoken))
                    {
                        res.Code = -1;
                        res.Msg = "登录信息失效,请重新登录";
                        return res;
                    }

                    DbToken token = dbConnect.DbToken.OrderByDescending(t => t.Id).FirstOrDefault(t => t.Userid.Equals(Userid) && t.Isa == 0);
                    if (token != null)
                    {
                        if (DateTime.Now > token.Odate)
                        {
                            dbConnect.DbToken.Remove(token);//删除
                            dbConnect.SaveChanges();//保存结果
                            res.Code = -1;
                            res.Msg = "登录信息失效,请重新登录";
                            return res;
                        }

                        token.Odate = DateTime.Now.AddMinutes(30);
                        dbConnect.SaveChanges();
                        res.Code = 100;
                    }
                    else
                    {
                        res.Code = -1;
                        res.Msg = "登录信息失效,请重新登录";
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    NLogHelper._.Error("验证用户出错", ex);
                }
            }
            return res;
        }

        public static string Token_admin_add(int Uid, string Userid, int Isa, string Ip, string Os)
        {
            string Token = "";
            using (DbConnect dbConnect = DbConnectUtils.GetDbContext())
            {
                try
                {
                    //生成token
                    TokenMod_Admin tm = new TokenMod_Admin
                    {
                        Ip = Ip,
                        Os = Os,
                        Userid = Userid
                    };
                    string Token_str = tm.Get_Encrypt_Token(tm);
                    //写入token
                    DbToken tk = new DbToken
                    {
                        Uid = Uid,
                        Userid = Userid,
                        Tokenstr = Token_str,
                        Odate = DateTime.Now.AddDays(3),
                        Isa = Isa,
                        Ip = Ip,
                        Os = Os
                    };
                    dbConnect.DbToken.Add(tk);
                    if (dbConnect.SaveChanges() > 0)
                    {
                        Token = Token_str;
                    }
                }
                catch (Exception ex)
                {
                    NLogHelper._.Error("写入token出错", ex);
                }
            }
            return Token;
        }

        public static Result Token_admin_check(string Token_str, string Userid_admin, string Ip, string Os)
        {
            Result res = new Result();
            try
            {
                if (Token_str == null || Token_str.Equals(""))
                {
                    res.Code = -1;
                    res.Msg = "登录信息失效,请重新登录";
                }
                else
                {
                    TokenMod_Admin tkm = new TokenMod_Admin
                    {
                        Userid = Userid_admin,
                        Ip = Ip,
                        Os = Os
                    };
                    string Nowtoken = tkm.Get_Encrypt_Token(tkm);
                    if (!Token_str.Equals(Nowtoken))
                    {
                        res.Code = -1;
                        res.Msg = "登录信息失效,请重新登录";
                    }
                    else
                    {
                        using DbConnect dbConnect = DbConnectUtils.GetDbContext();
                        DateTime dt = DateTime.Now;
                        DbToken token = dbConnect.DbToken.OrderByDescending(t => t.Id).FirstOrDefault(t => t.Userid.Equals(Userid_admin) && t.Isa == 1 && t.Odate > dt);
                        if (token != null)
                        {
                            res.Code = 100;
                        }
                        else
                        {
                            res.Code = -1;
                            res.Msg = "登录信息失效,请重新登录";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NLogHelper._.Error("验证admintoken出错", ex);
            }
            return res;
        }
    }
}
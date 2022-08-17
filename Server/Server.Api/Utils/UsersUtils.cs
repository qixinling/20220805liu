using Microsoft.EntityFrameworkCore;
using Server.Bonus.Utils;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Http_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Server.Logs;
using Server.Utils.Configuration_Utils;
using Server.Utils.Crypto_Utils;
using Server.Utils.Msg_Utils;
using Server.Bill.Utils;
using Server.Wallet.Utils;
using Server.Api.Level;
using Server.Api.Utils;

namespace Server.Api.Method
{
    public static class UsersUtils
    {
        /// <summary>
        /// 登录密码解密
        /// </summary>
        /// <param name="password">被AES加密的字符串</param>
        /// <returns></returns>
        public static string Decode(string password)
        {
            try
            {
                string project = ConfigUtils.Configuration["Project"];//获取项目编号
                string key = MD5Utils.MD5Encrypt(project, 32).ToUpper();//将项目编号转成32位字母都是大写的MD5
                password = AESUtils.AES_Decrypt(password, key);//将密码进行AES解密获得原始密码
            }
            catch (Exception Ex)
            {
                NLogHelper._.Error("登录密码解密出错", Ex);
            }
            return password;
        }

        /// <summary>
        /// 激活会员通用方法
        /// </summary>
        /// <param name="Cid"></param>
        /// <param name="Id">会员id</param>
        /// <param name="Userlx">操作人类型0服务中心,1管理员</param>
        /// <param name="Userid">操作人编号</param>
        /// <param name="_dbConnect"></param>
        /// <returns></returns>
        public static string Jihuo(int Cid, int Id, int Userlx, string Userid, DbConnect _dbConnect = null)
        {
            if (_dbConnect == null)
            {
                _dbConnect = DbConnectUtils.GetDbContext();
            }
            string Msg = "";
            DbUsers us = _dbConnect.DbUsers.FirstOrDefault(u => u.Id == Id && u.Ispay == 0);
            if (us != null)
            {
                try
                {
                    //变更会员状态 0-未激活,1-激活,2-空单
                    us.Ispay = 1;
                    us.Pdt = DateTime.Now;

                    List<DbSystemSettingBonus> systemSettingBonusList = new SystemSettingBonusMethod(_dbConnect).GetList();
                    foreach (DbSystemSettingBonus systemSettingBonus in systemSettingBonusList)
                    {
                        switch (systemSettingBonus.Code)
                        {
                            case "yeji1":
                                systemSettingBonus.Value += us.Lsk;
                                break;
                            case "yeji2":
                                systemSettingBonus.Value += us.Lsk;
                                break;
                            case "yeji3":
                                systemSettingBonus.Value += us.Lsk;
                                break;
                        }
                    }

                    if (_dbConnect.SaveChanges() > 0)
                    {
                        //业绩统计
                        YejiUtils ym = new YejiUtils(_dbConnect);
                        //ym.AddUser(1, us.Lsk);

                        Dictionary<string, decimal> bonusDic = SystemSettingBonusUtils.GetBonusParameter(_dbConnect);

                        //尾部参数0为首次激活,升级页面,购物页面传1
                        AddReteamYeji(us.Reid, us.Repath, us.Lsk, 1, 0);
                        DbUsersFteam uf = _dbConnect.DbUsersFteam.FirstOrDefault(f => f.Uid == us.Id);
                        if (uf != null)
                        {
                            //尾部参数0为首次激活,升级页面,购物页面传1
                            AddFteamYeji(uf.Ftreeplace, uf.Fpath, us.Lsk, 0);
                        }

                        //异步执行对碰,防止未结算完成时其他激活行为触发该函数
                        //判断有没有挂起,挂起时不触发
                        if (RepeatedCheckUtils.GetCacheValue("b6bonus") == null)
                        {
                            Task.Run(() =>
                            {
                                //挂起5秒,期间内收到的任何请求都不会触发挂起
                                RepeatedCheckUtils.SetChacheValue("b6bonus", "b6bonus", 5);
                                //5秒后执行
                                Thread.Sleep(5000);
                            });
                        }

                        Msg = us.Userid + "激活成功";

                        MsgUtils.Send(0, "账户激活", "您的账户已成功激活", 0, "系统消息", us.Id, us.Userid);

                        //管理员操作时写入操作日志
                        if (Userlx == 1)
                        {
                            SystemLogMethod.Add(Userid, HttpInfoUtils.GetIP(), 1, "激活会员:" + us.Userid);
                        }
                        else if (Userlx == 0)
                        {
                            //服务中心操作时写入激活记录
                            DbWalletsCoin coin = _dbConnect.DbWalletsCoin.FirstOrDefault(c => c.Id == Cid);
                            if (coin != null)
                            {
                                DbUsers user = _dbConnect.DbUsers.FirstOrDefault(u => u.Userid.Equals(Userid));
                                if (user != null)
                                {
                                    Result res = WalletsUtils.UpdateBalance(us.Id, coin.Id, us.Lsk, _dbConnect);
                                    if (res.Code == 0) { return res.Msg; }

                                    //创建账单
                                    IBill bill = new BillPay();
                                    bill.Create(us.Id, new Dictionary<int, decimal>
                                    {
                                        {coin.Id,us.Lsk}
                                    }, _dbConnect, "激活");
                                }
                                else
                                {
                                    Msg = Userid + "用户信息错误";
                                }
                            }
                            else
                            {
                                Msg = Cid + "货币不存在";
                            }
                        }
                    }
                    else
                    {
                        Msg = us.Userid + "激活失败";
                    }
                }
                catch (Exception ex)
                {
                    Msg = us.Userid + "激活出错";
                    NLogHelper._.Error(Msg, ex);
                }
            }
            else
            {
                Msg = "会员不存在";
            }
            return Msg;
        }

        /// <summary>
        /// 三轨小公排
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <param name="reid"></param>
        public static Result Gongpai(int uid, string userid, string username, int reid)
        {
            Result res = new Result();
            try
            {
                using DbConnect _dbConnect = DbConnectUtils.GetDbContext();
                DbUsers reus = _dbConnect.DbUsers.FirstOrDefault(u => u.Id == reid);
                if (reus == null) { res.Code = 0; res.Msg = "推荐人不存在"; return res; }
                int ceng = reus.Relevel;
                DbUsersFteam uf = new DbUsersFteam
                {
                    Uid = uid,
                    Userid = userid,
                    Username = username,
                    Fdate = DateTime.Now
                };

                while (1 == 1)
                {
                    List<DbUsersFteam> jlist = new List<DbUsersFteam>();
                    jlist = _dbConnect.DbUsersFteam.Where(f => (EF.Functions.Like(f.Fpath, "%," + reus.Id + ",%") || f.Uid == reus.Id) && f.Flevel == ceng && f.Ch1 == 0).OrderBy(f => f.Id).ToList();
                    foreach (DbUsersFteam juf in jlist)
                    {
                        uf.Fatherid = juf.Uid;
                        uf.Fathername = juf.Userid;
                        uf.Ftreeplace = 1;
                        uf.Fpath = string.Format("{0}{1},", juf.Fpath, juf.Uid);
                        uf.Flevel = juf.Flevel + 1;
                        juf.Ch1 = uf.Uid;
                        goto find;
                    }

                    jlist = _dbConnect.DbUsersFteam.Where(f => (EF.Functions.Like(f.Fpath, "%," + reus.Id + ",%") || f.Uid == reus.Id) && f.Flevel == ceng && f.Ch2 == 0).OrderBy(f => f.Id).ToList();
                    foreach (DbUsersFteam juf in jlist)
                    {
                        uf.Fatherid = juf.Uid;
                        uf.Fathername = juf.Userid;
                        uf.Ftreeplace = 2;
                        uf.Fpath = string.Format("{0}{1},", juf.Fpath, juf.Uid);
                        uf.Flevel = juf.Flevel + 1;
                        juf.Ch2 = uf.Uid;
                        goto find;
                    }

                    jlist = _dbConnect.DbUsersFteam.Where(f => (EF.Functions.Like(f.Fpath, "%," + reus.Id + ",%") || f.Uid == reus.Id) && f.Flevel == ceng && f.Ch3 == 0).OrderBy(f => f.Id).ToList();
                    foreach (DbUsersFteam juf in jlist)
                    {
                        uf.Fatherid = juf.Uid;
                        uf.Fathername = juf.Userid;
                        uf.Ftreeplace = 3;
                        uf.Fpath = string.Format("{0}{1},", juf.Fpath, juf.Uid);
                        uf.Flevel = juf.Flevel + 1;
                        juf.Ch3 = uf.Uid;
                        goto find;
                    }
                    ceng += 1;
                }
                find:
                _dbConnect.DbUsersFteam.Add(uf);
                if (_dbConnect.SaveChanges() > 0)
                {
                    res.Code = 100;
                }
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(res.Msg = "公排出错", ex);
            }
            return res;
        }


        /// <summary>
        /// 推广团队业绩
        /// </summary>
        /// <param name="Reid">推荐人id</param>
        /// <param name="Repath">团队路径</param>
        /// <param name="Jine">业绩金额</param>
        /// <param name="Dan">单数</param>
        /// <param name="Lx">用于判断是否增加recount和teamcount 0-首次激活(增加团队人数) 1-已激活(不增加团队人数)</param>
        /// <param name="bonusDic"></param>
        /// <param name="_dbConnect"></param>
        public static Result AddReteamYeji(int Reid, string Repath, decimal Jine, int Dan, int Lx,DbConnect _dbConnect = null)
        {
            Result res = new Result();
            try
            {
                _dbConnect ??= DbConnectUtils.GetDbContext();
                List<DbUsers> uList = _dbConnect.DbUsers.Where(u => EF.Functions.Like(Repath, "%," + u.Id + ",%")).OrderByDescending(u => u.Relevel).ToList();

                foreach (DbUsers us in uList)
                {
                    if (us.Id == Reid) { if (Lx == 0) { us.Recount += 1; } us.Reyeji += Jine; }
                    if (Lx == 0) { us.Teamcount += 1; }
                    us.Teamyeji += Jine;
                    us.Riteamyeji += Jine;
                    //us.Monthyeji += Jine;
                    us.Tdan += Dan;
                    //大小区业绩统计
                    /*List<DbUsers> brelist = _dbConnect.DbUsers.Where(u => u.Reid == us.Id).OrderByDescending(u => u.Lsk + u.Teamyeji).ToList();
                    us.Daquyeji = brelist[0].Lsk + brelist[0].Teamyeji;
                    brelist.RemoveAt(0);
                    us.Xiaoquyeji = brelist.Sum(u => u.Lsk + u.Teamyeji);*/
                }

                _dbConnect.SaveChanges();

                res.Code = 100;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(res.Msg = "新增推荐业绩出错", ex);
            }
            return res;
        }


        /// <summary>
        /// 接点团队业绩
        /// </summary>
        /// <param name="Tp">会员所在区域</param>
        /// <param name="Fpath">团队路径</param>
        /// <param name="Jine">业绩金额</param>
        /// <param name="Lx">用于判断是否增加teamcount和narea 0-首次激活(增加团队人数) 1-已激活(不增加团队人数)</param>
        public static Result AddFteamYeji(int Tp, string Fpath, decimal Jine, int Lx)
        {
            Result res = new Result();
            try
            {
                using DbConnect _dbConnect = DbConnectUtils.GetDbContext();
                List<DbUsersFteam> ufList = _dbConnect.DbUsersFteam.Where(f => EF.Functions.Like(Fpath, "%," + f.Uid + ",%")).OrderByDescending(f => f.Flevel).ToList();
                foreach (DbUsersFteam uf in ufList)
                {
                    switch (Tp)
                    {
                        case 1:
                            uf.Area1 += Jine;
                            uf.Yarea1 += Jine;
                            if (Lx == 0) { uf.Narea1 += 1; }
                            break;
                        case 2:
                            uf.Area2 += Jine;
                            uf.Yarea2 += Jine;
                            if (Lx == 0) { uf.Narea2 += 1; }
                            break;
                        case 3:
                            uf.Area3 += Jine;
                            uf.Yarea3 += Jine;
                            if (Lx == 0) { uf.Narea3 += 1; }
                            break;
                        case 4:
                            uf.Area4 += Jine;
                            uf.Yarea4 += Jine;
                            if (Lx == 0) { uf.Narea4 += 1; }
                            break;
                        case 5:
                            uf.Area5 += Jine;
                            uf.Yarea5 += Jine;
                            if (Lx == 0) { uf.Narea5 += 1; }
                            break;
                    }

                    if (Lx == 0) { uf.Teamcount += 1; }
                    uf.Teamyeji += Jine;

                    Tp = uf.Ftreeplace;
                }

                _dbConnect.SaveChanges();

                res.Code = 100;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(res.Msg = "新增接点业绩出错", ex);
            }
            return res;
        }

        public static string Delete(int Duid, int Del_uid, string Del_userid, int Del_lx)
        {
            bool Keyidel = false;
            string Msg = "";
            string Duerid = "";
            try
            {
                if (Duid == 1) { Msg = "不能删除管理员"; return Msg; }
                using DbConnect _dbConnect = DbConnectUtils.GetDbContext();
                DbUsers dur = _dbConnect.DbUsers.FirstOrDefault(u => u.Id == Duid);
                Duerid = dur.Userid;
                if (Del_lx == 1)
                {
                    //lx为1时作为前台删除
                    if (dur == null)
                    {
                        Msg = "会员不存在";
                    }
                    else
                    {
                        if (dur.Ispay > 0)
                        {
                            Msg = "会员" + Duerid + "已激活,无法删除";
                        }
                        else
                        {
                            if (dur.Bdid == Del_uid || dur.Reid == Del_uid)
                            {
                                Keyidel = true;
                            }
                            else
                            {
                                Msg = "没有删除" + Duerid + "的资格";
                            }
                        }
                    }
                }
                else if (Del_lx == 0)
                {
                    //lx为0时作为管理员删除
                    DbSystemAdmin ua = _dbConnect.DbSystemAdmin.FirstOrDefault(a => a.Userid.Equals(Del_userid));
                    if (ua != null)
                    {
                        Del_uid = ua.Id;
                        Keyidel = true;
                    }
                    else
                    {
                        Msg = "没有删除资格";
                    }
                }

                if (_dbConnect.DbUsersFteam.Where(f => f.Fatherid == Duid).ToList().Count() > 0)
                {
                    Msg = Duerid + "伞下存在会员,无法删除";
                    Keyidel = false;
                }

                if (Keyidel)
                {
                    //开始删除
                    DbUsersDelete ud = new DbUsersDelete
                    {
                        Uid = dur.Id,
                        Userid = dur.Userid,
                        Username = dur.Username,
                        Reid = dur.Reid,
                        Rename = dur.Rename,
                        Relevel = dur.Relevel,
                        Repath = dur.Repath,
                        Recount = dur.Recount,
                        Teamcount = dur.Teamcount,
                        Ddate = DateTime.Now,
                        Deluid = Del_uid,
                        Deluserid = Del_userid,
                        Delip = HttpInfoUtils.GetIP(),
                        Dellx = Del_lx
                    };
                    _dbConnect.DbUsersDelete.Add(ud);

                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_bill` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_bonus_source` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_wallets_chongzhi` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_wallets_tixian` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_wallets_zhuanzhang` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_wallets_zhuanhuan` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_wallets_zengjian` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_fwzx_apply` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_jihuo_record` where jid=" + Duid + " or uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_msg` where fid=" + Duid + " or sid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_shop_order_child` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_shop_order` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_address` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_bank` where uid=" + Duid + "");

                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_fteam` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("update `db_users_fteam` set ch1=0 where ch1=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("update `db_users_fteam` set ch2=0 where ch2=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("update `db_users_fteam` set ch3=0 where ch3=" + Duid + "");

                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_levelup` where uid=" + Duid + "");
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_wallets` where uid=" + Duid + "");

                    //更改下级推荐人
                    List<DbUsers> ulist = _dbConnect.DbUsers.Where(u => u.Reid == Duid).ToList();
                    foreach (DbUsers us in ulist)
                    {
                        us.Reid = dur.Reid;
                        us.Rename = dur.Rename;
                        us.Reusername = dur.Reusername;
                    }

                    //更新推荐团队路径
                    string Rep = "," + Duid + ",";
                    List<DbUsers> urlist = _dbConnect.DbUsers.Where(r => EF.Functions.Like(r.Repath, "%," + Duid + ",%")).ToList();
                    foreach (DbUsers ur in urlist)
                    {
                        ur.Relevel -= 1;
                        ur.Repath = ur.Repath.Replace("," + Duid, "");
                    }
                    _dbConnect.Database.ExecuteSqlRaw("delete from `db_users` where id=" + Duid + "");
                    _dbConnect.SaveChanges();
                    Msg = "删除" + Duerid + "成功";
                    SystemLogMethod.Add(Del_userid, HttpInfoUtils.GetIP(), 1, "删除会员:" + dur.Userid);
                }
            }
            catch (Exception ex)
            {
                Msg = "删除会员" + Duerid + "出错";
                NLogHelper._.Error(Msg, ex);
            }
            return Msg;
        }

        /// <summary>
        /// 树状图
        /// </summary>
        /// <param name="_dbConnect">数据库上下文</param>
        /// <param name="loopur">上级</param>
        /// <param name="looprt">下级</param>
        /// <returns>树状图</returns>
        public static TreeMod Tree(DbConnect _dbConnect, DbUsers loopur, TreeMod looprt)
        {
            List<Ulevel> uLevelList = new Ulevel().GetLevels();
            List<ILevel> xLevelList = new Xlevel().GetLevels();
            foreach (DbUsers urt in _dbConnect.DbUsers.Where(u => u.Reid == loopur.Id).ToList())
            {
                string uxName = " [";
               
                //if (urt.Ulevel > 0)
                //{
                    uxName += "  " + uLevelList[urt.Ulevel].Name;
                //// }
                //if (urt.Xlevel > 0)
                //{
                //    uxName += "  " + xLevelList[urt.Xlevel].Name;
                //}
                uxName += "  " + urt.Teamcount;
                uxName += "  " + urt.Lsk;
                uxName += "  " + urt.Riteamyeji + "]";
                TreeMod rtt = new TreeMod
                {
                    Id = urt.Id,
                    Label = urt.Usertel + uxName,
                    Icon = "el-icon-s-custom ispay" + urt.Ispay
                };
                rtt = Tree(_dbConnect, urt, rtt);
                looprt.Children.Add(rtt);
            }
            return looprt;
        }

        /// <summary>
        /// 网络图
        /// </summary>
        /// <param name="_dbConnect">数据库上下文</param>
        /// <param name="loopur">上级</param>
        /// <param name="looprt">下级</param>
        /// <param name="Areacount">开几条线</param>
        /// <returns>网络图</returns>
        public static NetworkMod Network(DbConnect _dbConnect, DbUsersFteam loopur, NetworkMod looprt, int Areacount)
        {
            for (int i = 1; i <= Areacount; i++)
            {
                DbUsersFteam urt = _dbConnect.DbUsersFteam.FirstOrDefault(u => u.Fatherid == loopur.Uid && u.Ftreeplace == i);
                NetworkMod rtt = new NetworkMod
                {

                    //switch表达式
                    Area = i switch
                    {
                        1 => "A区",
                        2 => "B区",
                        3 => "C区",
                        4 => "D区",
                        5 => "E区",
                        _ => "超范围",
                    },
                    Treeplace = i,

                    Fname = loopur.Userid
                };

                if (urt != null)
                {
                    rtt.Id = urt.Uid;
                    rtt.Label = urt.Userid;


                    rtt.Teamcount = urt.Teamcount;
                    rtt.Teamyeji = urt.Teamyeji;
                    rtt.Area1 = urt.Area1;
                    rtt.Area2 = urt.Area2;
                    rtt.Area3 = urt.Area3;
                    rtt.Area4 = urt.Area4;
                    rtt.Area5 = urt.Area5;



                    rtt = Network(_dbConnect, urt, rtt, Areacount);
                }
                else
                {
                    rtt.Id = 0;
                    rtt.Label = "注册";
                    rtt.Teamcount = 0;
                    rtt.Teamyeji = 0;
                    rtt.Area1 = 0;
                    rtt.Area2 = 0;
                    rtt.Area3 = 0;
                    rtt.Area4 = 0;
                    rtt.Area5 = 0;
                }

                looprt.children.Add(rtt);  //此处children必须为小写，否则网络图无法显示
            }
            return looprt;
        }

        /// <summary>
        /// 用户激活状态
        /// </summary>
        /// <param name="Ispay"></param>
        /// <returns></returns>
        public static string Getuserispayname(int Ispay)
        {

            string Name = "";
            if (Ispay == 0)
            {
                Name = "未激活";
            }
            else if (Ispay == 1)
            {
                Name = "已激活";
            }
            else if (Ispay == 2)
            {
                Name = "空单";
            }
            return Name;
        }

        /// <summary>
        /// 用户锁定状态
        /// </summary>
        /// <param name="Islock"></param>
        /// <returns></returns>
        public static string Getuserislockname(int Islock)
        {
            string Name = "";
            if (Islock == 0)
            {
                Name = "正常";
            }
            else if (Islock == 1)
            {
                Name = "[已锁定]";
            }
            return Name;
        }
    }
}

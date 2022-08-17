using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Api.Method;
using Server.Bonus.Utils;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Wallet.Utils;
using Server.Bill.Utils;
using Server.Api.Level;
using Server.Api.Utils;

namespace Server.Api.Controllers.UsersControllers.LevelupControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersLevelupController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public UsersLevelupController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 申请升级
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

                int ulevel = Convert.ToInt32(data["ulevel"]);
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();

                using var transaction = _dbConnect.Database.BeginTransaction();

                WalletsCoinMethod wcm = new WalletsCoinMethod(_dbConnect);
                List<DbWalletsCoin> clist =wcm.GetList();
                if (clist.Count() < 1) { _res.Fail("货币信息错误"); return _res; }

                //升级的时候暂时定死货币id
               
                DbWallets uw = _dbConnect.DbWallets.Include(u => u.UidNavigation).FirstOrDefault(u => u.Userid.Equals(userid) && u.Cid == clist[1].Id);
                if (uw == null) { _res.Fail("钱包信息错误"); return _res; }


                if (uw.UidNavigation.Ulevel >= ulevel) { _res.Fail("您的级别大于升级级别,请勿重复升级"); return _res; }

                SystemSettingBonusMethod ssbm = new SystemSettingBonusMethod(_dbConnect);

                Dictionary<string, decimal> bonusDic = SystemSettingBonusUtils.GetBonusParameter(_dbConnect);

                decimal Lsk = bonusDic["lsk" + ulevel];
                
                decimal Cha = Lsk - uw.UidNavigation.Lsk;

                if (Cha <= 0) { _res.Fail("差额错误,该账户无法升级,请联系管理员"); return _res; }

                if (uw.Jine < Cha) { _res.Fail("您的余额不足,请充值"); return _res; }

                List<DbSystemSettingBonus> systemSettingBonusList = ssbm.GetList();
                foreach (DbSystemSettingBonus systemSettingBonus in systemSettingBonusList)
                {
                    switch (systemSettingBonus.Code)
                    {
                        case "yeji1":
                            systemSettingBonus.Value += Cha;
                            break;
                        case "yeji2":
                            systemSettingBonus.Value += Cha;
                            break;
                        case "yeji3":
                            systemSettingBonus.Value += Cha;
                            break;
                    }
                }

                DbUsersLevelup sj = new DbUsersLevelup
                {
                    Userid = userid,
                    Username = uw.UidNavigation.Username,
                    Uid = uw.Uid,
                    Ylevel = uw.UidNavigation.Ulevel,
                    Level = ulevel,
                    Jine = Cha,
                    Sdate = DateTime.Now,
                    State = 1
                };

                uw.UidNavigation.Ulevel = ulevel;
                uw.UidNavigation.Ylevel = ulevel;

                uw.UidNavigation.Lsk += Cha;

                UsersLevelupMethod ulm = new UsersLevelupMethod( _dbConnect );
                ulm.Add(sj);

                Result res = WalletsUtils.UpdateBalance(uw.Uid, uw.Cid, Cha, _dbConnect);
                if (res.Code == 0) { return res; }

                //创建账单
                IBill bill = new BillPay();
                bill.Create(uw.Uid, new Dictionary<int, decimal>
                    {
                        {uw.Cid,Cha}
                    }, _dbConnect,"升级");

                _dbConnect.SaveChanges();
                transaction.Commit();

                //业绩统计
                YejiUtils ym = new YejiUtils(_dbConnect);
                ym.AddUser(0, Cha);

                //尾部参数0为首次激活,升级页面,购物页面传1



                UsersUtils.AddReteamYeji(uw.UidNavigation.Reid, uw.UidNavigation.Repath, Cha, 0, 1);
                UsersFteamMethod ufm = new UsersFteamMethod(_dbConnect);
                DbUsersFteam uf =ufm.GetByUid(uw.UidNavigation.Id);
                if (uf != null)
                {
                    //尾部参数0为首次激活,升级页面,购物页面传1
                    UsersUtils.AddFteamYeji(uf.Ftreeplace, uf.Fpath, Cha, 1);
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

                _res.Done(null, "升级成功");


                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<Ulevel> uLevelList = new Ulevel().GetLevels();
                dic.Add("newulevel", ulevel.ToString());
                dic.Add("newulevelname", uLevelList[ulevel].Name);
                diclist.Add(dic);
                _res.Done(diclist, "升级成功");

            }
            catch (Exception ex)
            {
                _res.Error("申请升级异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        ///  查询用户升级记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenCheckFilters]
        [SignCheckFilters]
        public Result List()
        {

            try
            {
                UsersLevelupMethod ulm = new UsersLevelupMethod(_dbConnect);
                List<Ulevel> uLevelList = new Ulevel().GetLevels();
                var uulist = ulm.GetList().OrderByDescending(u => u.Id).Select(u => new
                {
                    u.Userid,
                    u.Username,
                    u.Ylevel,
                    ylevelname = uLevelList[u.Ylevel].Name,
                    u.Level,
                    xulevelname = uLevelList[u.Level].Name,
                    u.Jine,
                    u.Sdate,
                    ispay = u.State
                });
                _res.Done(uulist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询升级记录异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

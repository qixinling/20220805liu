using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Api.Method;
using Server.Bonus;
using Server.Api.Bonus.Algorithm;

namespace Server.Api.Controllers.BonusController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Bonus_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public Bonus_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }


        /// <summary>
        /// 查询奖金名称集合
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result Name()
        {
            return _res.Done(BonusUtils.BonusList, "");
        }

        /// <summary>
        /// 查询奖金记录
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
                int id = Convert.ToInt32(data["id"]);

                BonusSourceMethod bonusSourceMethod = new BonusSourceMethod(_dbConnect);

                List<BonusGroupMod> bgmList = new List<BonusGroupMod>();
                List<DbBonusSource> bsList = bonusSourceMethod.GetList();
                bsList = bsList.Where(b => b.Btid == id && b.State == 1).OrderBy(b => b.Uid).ThenBy(b => b.Lx).ToList();
                int index = 0;
                foreach (DbBonusSource bs in bsList)
                {
                    if (bgmList.Count == 0)
                    {
                        BonusGroupMod bgm = new BonusGroupMod
                        {
                            Uid = bs.Uid,
                            Userid = bs.Userid,
                            Username = bs.Username
                        };
                        bgm.jine[0] += bs.Jine;
                        bgm.jine[bs.Lx] = bs.Jine;

                        bgmList.Add(bgm);
                    }
                    else
                    {
                        if (bgmList[index].Uid == bs.Uid)
                        {
                            bgmList[index].jine[0] += bs.Jine;
                            bgmList[index].jine[bs.Lx] += bs.Jine;
                        }
                        else
                        {
                            BonusGroupMod bgm = new BonusGroupMod
                            {
                                Uid = bs.Uid,
                                Userid = bs.Userid,
                                Username = bs.Username
                            };
                            bgm.jine[0] += bs.Jine;
                            bgm.jine[bs.Lx] = bs.Jine;

                            bgmList.Add(bgm);
                            index++;
                        }
                    }
                }

                _res.Done(bgmList, "查询成功");

            }
            catch (Exception ex)
            {
                _res.Error("查询bonus异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询奖金来源
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Source_List()
        {
            try
            {
                List<IBonus> bonusList = BonusUtils.BonusList;
                BonusSourceMethod bonusSourceMethod = new BonusSourceMethod(_dbConnect);
                List<DbBonusSource> bsList = bonusSourceMethod.GetList();
                var bslist = bsList.Where(b => b.State == 1).OrderByDescending(m => m.Id).Select(s => new
                {
                    s.Id,
                    s.Uid,
                    s.Userid,
                    s.Yuserid,
                    bonusList[s.Lx].BonusName,
                    s.Jine,
                    s.Bz,
                    s.Sdate
                }).ToList();
                _res.Done(bslist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("获取奖金来源异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询每日奖金记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Time_List()
        {
            try
            {
                List<DbBonus> btlist = new List<DbBonus>();
                BonusMethod bm = new BonusMethod(_dbConnect);

                btlist = bm.List().OrderByDescending(m => m.Id).ToList();
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                foreach (DbBonus bt in btlist)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "id", bt.Id.ToString() },
                        { "btdate", bt.Btdate.ToString("yyyy-MM-dd") },
                        { "b0", bt.DbBonusSource.Sum(b => b.Jine).ToString() }
                    };

                    for (int i = 1; i <= 15; i++)
                    {
                        dic.Add("b" + i.ToString(), bt.DbBonusSource.Where(s => s.Lx == i && s.State == 1).Sum(b => b.Jine).ToString());
                    }

                    diclist.Add(dic);
                }
                _res.Done(diclist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询bonustime异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

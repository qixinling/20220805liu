using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Http_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using Server.Wallet.Utils;
using Server.Bill.Utils;
using Server.Bonus.Utils;

namespace Server.Api.Controllers.WalletsControllers.ChongzhiControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsChongzhi_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsChongzhi_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 查询充值记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List()
        {
            try
            {
                _res.Done(new WalletsChongzhiMethod(_dbConnect).GetList().Where(b => b.Isdelete == 0).OrderByDescending(m => m.Cdate).OrderBy(m => m.Ispay).ToList(), "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询所有充值记录数据异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 充值审核
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Pass(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }

                string ids = data["ids"].ToString();

                using var transaction = _dbConnect.Database.BeginTransaction();

                decimal czjine = 0;
                WalletsChongzhiMethod wcm = new WalletsChongzhiMethod(_dbConnect);
                List<DbWalletsChongzhi> wcList = wcm.GetList().Where(c => ids.Contains(c.Id.ToString()) && c.Isdelete == 0 && c.Ispay == 0).ToList();
                foreach (DbWalletsChongzhi wc in wcList)
                {
                    Result res = WalletsUtils.UpdateBalance(wc.Uid, wc.Cid, wc.Jine, _dbConnect);
                    if (res.Code == 0) { return res; }

                    //创建账单
                    IBill bill = new BillChongZhi();
                    bill.Create(wc.Uid, new Dictionary<int, decimal>
                    {
                        {wc.Cid,wc.Jine}
                    }, _dbConnect);


                    wc.Ispay = 1;
                    czjine += wc.Jine;

                    if (wc.Cid == 2)
                    {
                        decimal kou = 200;
                        DbWallets u = _dbConnect.DbWallets.Include(c => c.UidNavigation).FirstOrDefault(c => c.Uid == wc.Uid && c.Cid == wc.Cid);
                        if (u.UidNavigation.Djxyz < kou)
                        {
                            decimal cha = kou - u.UidNavigation.Djxyz;
                            if (u.Jine >= cha)
                            {
                                u.Jine -= cha;
                                u.UidNavigation.Djxyz += cha;
                            }
                        }
                    }
                }

                _dbConnect.SaveChanges();

                transaction.Commit();

                YejiUtils ym = new YejiUtils();
                ym.AddChongzhi(czjine);

                _res.Done(null, "审核成功");
                
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 2, "审核充值");
            }
            catch (Exception ex)
            {
                _res.Error("审核充值异常");
             
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 撤销充值申请记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Revoke(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }

                string cid = data["cid"].ToString();

                string[] Dllist = cid.Split(',');
                List<string> Cname = new List<string>();
                List<decimal> Cjine = new List<decimal>();
                string Msg = "";
              
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbWalletsChongzhi c = _dbConnect.DbWalletsChongzhi.FirstOrDefault(b => b.Id == Id && b.Isdelete == 0);
                    if (c != null)
                    {
                        if (c.Ispay == 0)
                        {
                            c.Ispay = 2;
                            Msg += c.Userid + "撤销成功. ";

                            Cname.Add(c.Username);
                            Cjine.Add(c.Jine);
                        }
                        else if (c.Ispay == 1)
                        {
                            Msg += c.Userid + "已通过审核，无法撤销. ";
                        }
                        else if (c.Ispay == 2)
                        {
                            Msg += c.Userid + "已撤销，无法再次撤销. ";
                        }
                    }
                    else
                    {
                        Msg += Id + "记录不存在，撤销失败. ";
                    }
                }

                _dbConnect.SaveChanges();
                _res.Done(null, Msg);
               

                int i = 0;
                foreach (string name in Cname)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 2, "撤销充值申请:" + name);
                    i++;
                }
            }
            catch (Exception ex)
            {
                _res.Error("撤销充值申请异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除充值申请记录
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
                if (RepeatedCheckUtils.Rc(userid_admin, 2)) { _res.Fail("请勿重复提交"); return _res; }

                string delete_id = data["delete_id"].ToString();

                string[] Dllist = delete_id.Split(',');
                string msg = "";
                
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbWalletsChongzhi c = _dbConnect.DbWalletsChongzhi.FirstOrDefault(b => b.Id == Id && b.Isdelete == 0);
                    if (c != null)
                    {
                        c.Isdelete = 1;
                        if (_dbConnect.SaveChanges() > 0)
                        {
                            msg += c.Userid + "删除成功. ";
                            SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 2, "删除充值记录:" + c.Username);
                        }
                        else
                        {
                            msg += c.Userid + "删除失败. ";
                        }
                    }
                }
                _res.Done(null, msg);
                
            }
            catch (Exception ex)
            {
                _res.Error("删除充值申请异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
   
    }
}
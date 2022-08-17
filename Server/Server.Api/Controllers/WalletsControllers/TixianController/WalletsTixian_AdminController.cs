using Microsoft.AspNetCore.Mvc;
using Server.Api.Method;
using Server.Bonus.Utils;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Http_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Wallet.Utils;
using Server.Bill.Utils;
using Server.Utils.Msg_Utils;

namespace Server.Api.Controllers.WalletsControllers.TixianController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WalletsTixian_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public WalletsTixian_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 提现撤销
        /// </summary>
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

                string chexiaoyuanyin = data["chexiaoyuanyin"].ToString();
                string chexiao_id = data["chexiao_id"].ToString();

                using var transaction = _dbConnect.Database.BeginTransaction();

                string[] Cxlist = chexiao_id.Split(',');
                List<string> Cname = new List<string>();
                List<decimal> Cjine = new List<decimal>();
                string Msg = "";
              
                foreach (string Cx in Cxlist)
                {
                    int Id = Convert.ToInt32(Cx);
                    DbWalletsTixian t = _dbConnect.DbWalletsTixian.FirstOrDefault(b => b.Id == Id && b.Isdelete == 0);
                    if (t != null)
                    {
                        if (t.Ispay == 0)
                        {
                            Result res = WalletsUtils.UpdateBalance(t.Uid, t.Cid, t.Jine,_dbConnect);
                            if (res.Code == 0) { return res; }

                            //创建账单
                            IBill bill = new BillTiXian();
                            bill.Create(t.Uid, new Dictionary<int, decimal>
                            {
                                {t.Cid,t.Jine}
                            }, _dbConnect,"提现撤销");

                            t.Ispay = 2;
                            t.Chexiaoyuanyin = chexiaoyuanyin;

                            Msg += t.Userid + "撤销成功. ";
                            Cname.Add(t.Username);
                            Cjine.Add(t.Jine);

                            MsgUtils.Send(0, "提现", "您的提现申请:" + t.Jine + "已撤销", 0, "系统消息", t.Uid, t.Userid);

                        }
                        else if (t.Ispay == 1)
                        {
                            Msg += t.Userid + "已经过审核，撤销失败. ";
                        }
                        else if (t.Ispay == 2)
                        {
                            Msg += t.Userid + "已撤销，无法再次撤销. ";
                        }
                    }
                    else
                    {
                        Msg += Id + "不存在. ";
                    }
                }

                _dbConnect.SaveChanges();
                transaction.Commit();

                _res.Done(null, Msg);

                int i = 0;
                foreach (string Name in Cname)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 3, "撤销提现:" + Name);
                    i++;
                }
            }
            catch (Exception ex)
            {
                _res.Error("提现撤销异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询所有提现记录数据
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
                WalletsTixianMethod wtm = new WalletsTixianMethod(_dbConnect);
                List<DbWalletsTixian> tlist = wtm.GetList().Where(b => b.Isdelete == 0).OrderByDescending(m => m.Tdate).OrderBy(m => m.Ispay).ToList();
                _res.Done(tlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询提现记录异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除提现申请记录
        /// </summary>
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
                List<string> Cname = new List<string>();
                List<decimal> Cjine = new List<decimal>();
                string Msg = "";
               
               
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbWalletsTixian t = _dbConnect.DbWalletsTixian.FirstOrDefault(b => b.Id == Id && b.Isdelete == 0);
                    if (t != null)
                    {
                        if (t.Ispay == 0)
                        {
                            DbWallets uw = _dbConnect.DbWallets.FirstOrDefault(u => u.Uid == t.Uid && u.Cid == t.Cid);
                            if (uw == null) { Msg += t.Userid + "钱包信息错误，删除失败. "; continue; }
                            uw.Jine += t.Jine;
                            t.Ispay = 2;
                        }

                        t.Isdelete = 1;
                        Cname.Add(t.Username);
                        Cjine.Add(t.Jine);
                        if (_dbConnect.SaveChanges() > 0)
                        {
                            Msg += t.Userid + "删除成功. ";
                            SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 3, "删除提现记录:" + t.Username);
                        }
                        else
                        {
                            Msg += t.Userid + "删除失败. ";
                        }



                    }
                    else
                    {
                        Msg += Id + "不存在. ";
                    }
                }
                _res.Done(null, Msg);

                int i = 0;
                foreach (string Name in Cname)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 3, "删除提现:" + Name);
                    i++;
                }
            }
            catch (Exception ex)
            {
                _res.Error("删除提现申请异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 提现审核
        /// </summary>
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

                string txpass_id = data["txpass_id"].ToString();

                using var transaction = _dbConnect.Database.BeginTransaction();

                string[] Dllist = txpass_id.Split(',');
                List<string> Cname = new List<string>();
                List<decimal> Cjine = new List<decimal>();
                decimal Ztx = 0;
                string Msg = "";
                
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbWalletsTixian t = _dbConnect.DbWalletsTixian.FirstOrDefault(b => b.Id == Id && b.Isdelete == 0);
                    if (t != null)
                    {
                        if (t.Ispay == 0)
                        {
                            t.Ispay = 1;
                            decimal zheng = t.Jine;
                            Ztx += zheng;

                            Cname.Add(t.Username);
                            Cjine.Add(t.Jine);

                            MsgUtils.Send(0, "提现", "您的提现申请:" + t.Jine + "已通过审核", 0, "系统消息", t.Uid, t.Userid);
                            Msg += "审核通过";
                        }
                        else if (t.Ispay == 1)
                        {
                            Msg += t.Userid + "已通过审核，无法再次审核. ";
                        }
                        else if (t.Ispay == 2)
                        {
                            Msg += t.Userid + "已撤销，无法审核. ";
                        }
                    }
                    else
                    {
                        Msg += Id + "不存在. ";
                    }
                }
                _dbConnect.SaveChanges();
                _res.Done(null, Msg);
                int i = 0;
                foreach (string Name in Cname)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 3, "审核提现:" + Name);
                    i++;
                }
                YejiUtils ym = new YejiUtils(_dbConnect);
                ym.AddTixian(Ztx);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                _res.Error("审核提现申请异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

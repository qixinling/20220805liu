using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Configuration_Utils;
using Server.Utils.Http_Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Utils.Token_Utils;
using Server.Utils.Permission_Utils;

namespace Server.Api.Controllers.SystemControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DataBase_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;

        private readonly string _path;
        private readonly string _server;
        private readonly string _user;
        private readonly string _pwd;
        private readonly string _database;
        public DataBase_AdminController(DbConnect dbConnect,Result res)
        {
            _dbConnect = dbConnect;
            _res = res;

            _path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BackUp");
#if DEBUG
            _server = ConfigUtils.Configuration["AppSettings:server_debug"];
#else
            _server = ConfigUtils.Configuration["AppSettings:server"];
#endif
            _user = ConfigUtils.Configuration["AppSettings:user"];
            _pwd = ConfigUtils.Configuration["AppSettings:pwd"];
            _database = ConfigUtils.Configuration["AppSettings:database"];
        }

        /// <summary>
        /// 重置数据库
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Reset(JObject data)
        {

            string userid_admin = data["userid_admin"].ToString();
            try
            {
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_bonus_source`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_bonus_jiesuan`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_bonus`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_msg`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_checkcode`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_shop_collection`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_shop_order_child`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_shop_order`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_system_log`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_system_achievement`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_address` where uid>1");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_bank` where uid>1");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_delete`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_fteam` where id>1");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_fwzx_apply`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_jihuo_record`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_users_levelup`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_wallets` where uid>1");
                _dbConnect.Database.ExecuteSqlRaw("update `db_wallets` set jine=0");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_bill_amount`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_bill`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_wallets_chongzhi`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_wallets_tixian`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_wallets_zhuanzhang`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_wallets_zhuanhuan`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_wallets_zengjian`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_yuyue`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_hold`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_shoudan`");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_token` where isa=0 or uid>1");
                _dbConnect.Database.ExecuteSqlRaw("delete from `db_users` where id>1");

                _dbConnect.Database.ExecuteSqlRaw("update `db_users` set ulevel=0,ylevel=0,xlevel=4,lsk=0,ylsk=0,pv=0,dan=0,tdan=0,isbd=1,bdlevel=1,recount=0,teamcount=0,reyeji=0,teamyeji=0,daquyeji=0,xiaoquyeji=0,monthyeji=0,shouru=0");
                _dbConnect.Database.ExecuteSqlRaw("update `db_users_fteam` set fcount=0,teamcount=0,teamyeji=0,area1=0,area2=0,area3=0,area4=0,area5=0,yarea1=0,yarea2=0,yarea3=0,yarea4=0,yarea5=0,narea1=0,narea2=0,narea3=0,narea4=0,narea5=0,ch1=0,ch2=0,ch3=0,ch4=0,ch5=0");

                _res.Done(null, "清空数据库成功");
              
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 9, "清空数据库");
            }

            catch (Exception ex)
            {
                _res.Error("清空数据库异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 备份
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Backup(JObject data)
        {
            string userid_admin = data["userid_admin"].ToString();
            string backup_name = data["backup_name"].ToString();

            if (BackupUtils.Backup(_server, _user, _pwd, _database, _path, backup_name))
            {
                _res.Done(null, "备份成功");
               
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 9, "备份数据:" + backup_name);
            }
            else
            {
                _res.Fail("备份失败");
                
            }
            return _res;
        }

        /// <summary>
        /// 还原数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Restore(JObject data)
        {


            string userid_admin = data["userid_admin"].ToString();
            string restore_name = data["restore_name"].ToString();

            if (BackupUtils.Restore(_server, _user, _pwd, _database, _path, restore_name))
            {
                _res.Done(null, "还原成功");
             
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 9, "还原数据:" + restore_name);
            }
            else
            {
                _res.Fail("还原失败");
               
            }
            return _res;
        }

        /// <summary>
        /// 删除备份
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Delete(JObject data)
        {

            string userid_admin = data["userid_admin"].ToString();
            string del_name = data["del_name"].ToString();

            if (BackupUtils.Delete(_path, del_name))
            {
                _res.Done(null, "删除成功");
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 9, "删除备份:" + del_name);
            }
            else
            {
                _res.Fail("删除失败");
            }
            return _res;
        }

        /// <summary>
        ///  获取备份列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List()
        {
            
            

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            //获得备份文件路径
            DirectoryInfo dir = new DirectoryInfo(_path);
            FileInfo[] lstFile = dir.GetFiles();
           
            List<FileInfo> list = new List<FileInfo>(lstFile);
            List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();

            foreach (FileInfo subdir in list.OrderByDescending(s=>s.CreationTime))
            {
                if (!subdir.Name.Equals(".gitignore"))
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "name", subdir.Name },
                        { "date", subdir.CreationTime.ToString() }
                    };
                    if (subdir.Length == 0)
                    {
                        dic.Add("length", "文件损坏");
                    }
                    else
                    {
                        decimal kb = subdir.Length / 1000;
                        dic.Add("length", kb.ToString() + " KB");
                    }

                    diclist.Add(dic);
                    
                }
            }
            _res.Done(diclist, "查询成功");
            return _res;
        }


        /// <summary>
        /// 下载备份
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet] 
        public IActionResult Download(JObject data)
        {
            EmptyResult er = new EmptyResult();

            string token_admin = data["token_admin"].ToString();
            string userid_admin = data["userid_admin"].ToString();
            string download_name = data["download_name"].ToString();

            Result res = TokenUtils.Token_admin_check(token_admin, userid_admin, HttpInfoUtils.GetIP(), HttpInfoUtils.GetOSVersion());
            if (res.Code == 0) { return er; }

            if (!PermissionUtils.CheckPermission(userid_admin)) { _res.Fail("没有权限"); return er; }

            try
            {
                
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 9, "下载备份:" + download_name);

                string DownUrl = Path.Combine(_path, download_name);

                var Stream = System.IO.File.OpenRead(DownUrl);
                return File(Stream, "application/vnd.android.package-archive", Path.GetFileName(DownUrl));
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(res.Msg, ex);
            }
            return er;
        }
    }
}

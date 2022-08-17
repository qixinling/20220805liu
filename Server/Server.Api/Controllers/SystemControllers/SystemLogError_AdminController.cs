using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;

using System;
using System.IO;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.SystemControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SystemLogError_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public SystemLogError_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 删除错误日志
        /// </summary>
        /// <param name="userid_admin"></param>
        /// <param name="token_admin"></param>
        /// <param name="delete_name"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Error_Delete(JObject data)
        {
            
            

            try
            {
                
                string delete_name = data["delete_name"].ToString();
                string[] Dllist = delete_name.Split(',');

                foreach (string Name in Dllist)
                {
                    string FilePath = AppContext.BaseDirectory + "/Log/" + Name + ".txt";
                    FileInfo file = new FileInfo(FilePath);

                    if ((file.Attributes & FileAttributes.ReadOnly) > 0)
                    {
                        file.Attributes ^= FileAttributes.ReadOnly; // 必须去除只读属性才能进行设置
                    }
                    file.Delete();
                    _res.Done(null, "删除成功");
                }

            }
            catch (Exception ex)
            {
                _res.Error("删除错误日志异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询错误日志表所有内容
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Error_List()
        {
            
            


            try
            {
                //string backupFile = System.Web.Hosting.HostingEnvironment.MapPath("~/") + "/log/";
                //获得备份文件路径
                //DirectoryInfo dir = new DirectoryInfo(backupFile);
                //foreach (FileInfo subdir in dir.GetFiles())
                //{
                //    Dictionary<string, string> dic = new Dictionary<string, string>();
                //    dic.Add("name", subdir.Name.Substring(0, subdir.Name.Length - 4));
                //    dic.Add("date", subdir.LastWriteTime.ToString());
                //    jrm.diclist.Add(dic);
                //}
            }
            catch (Exception ex)
            {
                _res.Error("获取错误日志列表异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }

            return _res;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Logs;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using static Server.Api.Filters;

namespace Server.Api.Controllers.TeachersControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Teachers_AdminController:ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;

        public Teachers_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;

        }

        /// <summary>
        /// 查询单个教资信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {
            int id = Convert.ToInt32(data["id"]);

            try
            {
                DbTeachers teachers = _dbConnect.DbTeachers.FirstOrDefault(t => t.Id == id);
                if (teachers == null) { _res.Fail("师资信息出错"); return _res; }

                _res.Done(teachers, "查询成功");
            }

            catch (Exception ex)
            {
                _res.Error("查询单个师资信息异常");
                NLogHelper._.Error(_res.Msg, ex);
            }

            return _res;

        }

        /// <summary>
        /// 查询全部教资
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result List(JObject data)
        {
           

            try
            {
                List<DbTeachers> tlist = _dbConnect.DbTeachers.ToList();

                _res.Done(tlist, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询全部师资异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;

        }

        /// <summary>
        /// 删除教资
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Delete(JObject data)
        {
            string ids = data["ids"].ToString();
            try
            {
                string[] Dllist = ids.Split(',');

                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbTeachers teachers = _dbConnect.DbTeachers.FirstOrDefault(c => c.Id == Id);
                    if (teachers != null)
                    {
                        string img = teachers.Tximg;
                        _dbConnect.DbTeachers.Remove(teachers);

                        
                    }
                }
                _dbConnect.SaveChanges();
                _res.Done(null, "删除成功");
            }
            catch (Exception ex)
            {
                _res.Error("删除师资异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;

        }


        /// <summary>
        /// 后台添加教资
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Add(JObject data)
        {
            string name = data["name"].ToString();
            string introduce = data["introduce"].ToString();
            string img = data["img"].ToString();

            try
            {
                DbTeachers teachers = new DbTeachers
                {
                    Name = name,
                    Introduce = introduce,
                    Tximg = img
                };
                _dbConnect.DbTeachers.Add(teachers);
                _dbConnect.SaveChanges();

                _res.Done(null, "添加成功");

            }
            catch (Exception ex)
            {
                _res.Error("添加师资异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改教资信息
        /// </summary>
        /// <returns></returns> 
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Update(JObject data)
        {
            int id = Convert.ToInt32(data["id"]);
            int ispay = Convert.ToInt32(data["ispay"]);
            
            string name = data["name"].ToString();
            string introduce = data["introduce"].ToString();
            string img = data["img"].ToString();

            try
            {
                DbTeachers si = _dbConnect.DbTeachers.FirstOrDefault(s => s.Id == id);
                if (si == null) { _res.Fail("师资信息错误"); return _res; }
                si.Ispay = ispay;
                si.Name = name;
                si.Introduce = introduce;
                si.Tximg = img;

                _dbConnect.SaveChanges();
                _res.Done(null, "修改成功");

            }
            catch (Exception ex)
            {
                _res.Error("修改师资信息异常");

                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

    }
}

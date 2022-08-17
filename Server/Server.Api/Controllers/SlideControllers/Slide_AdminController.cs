using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DataBaseModels;


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;
using Server.Api.Method;

namespace Server.Api.Controllers.SlideControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Slide_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public Slide_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        public string[] LxName { get; set; } = new[] {"", "无关联", "商品" , "新闻", "外部网址", "商品分类" };
        public string[] PagelxName { get; set; } = new[] { "", "商城", "公告" };
        /// <summary>
        /// 后台添加幻灯片
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Add(JObject data)
        {
            
           
            try
            {
                
            
                int lx = Convert.ToInt32(data["lx"]);
                int gid = Convert.ToInt32(data["gid"]);
                string url = data["url"].ToString();
                string img = data["img"].ToString();
                int pagelx = Convert.ToInt32(data["pagelx"]);

                DbSlide slide = new DbSlide
                {
                    Lx = lx,
                    Pagelx = pagelx,
                    Gid = gid
                };
                if (Url == null || url == "")
                {
                    slide.Url = "/";
                }
                else
                {
                    slide.Url = url;
                }
                slide.Img = img;
                SlideMethod sm = new SlideMethod(_dbConnect);
                sm.Add(slide);
                if (_dbConnect.SaveChanges() > 0)
                {
                    _res.Done(null, "添加成功");
                }
                else
                {
                    _res.Fail("添加失败");

                }
            }
            catch (Exception ex)
            {
                _res.Error("添加幻灯片异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 删除幻灯片
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
                string delete_id = data["delete_id"].ToString();

                string[] Dllist = delete_id.Split(',');
                SlideMethod sm = new SlideMethod(_dbConnect);
                foreach (string Dl in Dllist)
                {
                    int Id = Convert.ToInt32(Dl);
                    DbSlide slide =sm.GetById(Id);
                    if (slide != null)
                    {
                        string img = slide.Img;
                       sm.Remove(Id);
                        if (_dbConnect.SaveChanges() > 0)
                        {
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload");
                            DirectoryInfo folder = new DirectoryInfo(path);
                            foreach (FileInfo fileinfo in folder.GetFiles(img))
                            {
                                fileinfo.Delete();
                            }
                        }
                    }
                }
                _res.Done(null, "删除成功");
            }
            catch (Exception ex)
            {
                _res.Error("删除幻灯片异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 查询单条幻灯片数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Get(JObject data)
        {
            
            
            try
            {
                int select_id = Convert.ToInt32(data["select_id"]);
                SlideMethod sm = new SlideMethod(_dbConnect);
                DbSlide slide =sm.GetById(select_id);
                if (slide != null)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "id", slide.Id.ToString() },
                        { "lx", slide.Lx.ToString() },
                        { "pagelx", slide.Pagelx.ToString() },
                        { "gid", slide.Gid.ToString() },
                        { "url", slide.Url },
                        { "img", slide.Img }
                    };
                    _res.Done(dic, "查询成功");
                }
                else
                {
                    _res.Fail("没有对应id");
                 
                }
            }
            catch (Exception ex)
            {
                _res.Error("查询单条幻灯片信息异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }

            return _res;
        }

        /// <summary>
        /// 查询全部幻灯片
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
                SlideMethod sm = new SlideMethod(_dbConnect);
                var list = sm.GetList().OrderBy(m => m.Id).Select(s => new
                {
                   id=s.Id,
                   lx=s.Lx,
                   lxname=LxName[s.Lx],
                    pagelxname=PagelxName[s.Pagelx],
                   url=s.Url,
                   gid=s.Gid,
                   img=s.Img
                });
                _res.Done(list, "查询成功");
            }
            catch (Exception ex)
            {
                _res.Error("查询全部幻灯片异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改幻灯片
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Update(JObject data)
        {
            
            
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                int id = Convert.ToInt32(data["id"]);
                int lx = Convert.ToInt32(data["lx"]);
                int gid = Convert.ToInt32(data["gid"]);
                string url = data["url"].ToString();
                string img = data["img"].ToString();
                int pagelx = Convert.ToInt32(data["pagelx"]);

                SlideMethod sm = new SlideMethod(_dbConnect);
                DbSlide slide = sm.GetById(id);

                //string Yimg = slide.Img;

                if (slide == null) { _res.Fail("幻灯片错误"); return _res; }
                slide.Lx = lx;
                slide.Gid = gid;
                slide.Url = url;
                slide.Img = img;
                slide.Pagelx = pagelx;

                if (_dbConnect.SaveChanges() > 0)
                {
                   // if (Yimg != img)
                   // {
                        //string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload");
                        //DirectoryInfo folder = new DirectoryInfo(path);
                        //foreach (FileInfo fileinfo in folder.GetFiles(Yimg))
                        //{
                        //    fileinfo.Delete();
                        //}
                   // }
                }
                _res.Done(null, "修改成功");
            }
            catch (Exception ex)
            {
                _res.Error("修改幻灯片异常");
              
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
    }
}

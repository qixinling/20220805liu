using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Utils.Http_Utils;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Server.Logs;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Newtonsoft.Json.Linq;
using Server.Utils.Token_Utils;

namespace Server.Api.Controllers
{
    /// <summary>
    /// 公共上传接口
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UploadController : ControllerBase
    {
        /// <summary>
        /// 前台上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result UploadImages(string token,string userid,string width,string height)
        {
            Result res = new Result();
            try
            {
                
                string[] PictureFormatArray = { "png", "jpg", "jpeg", "gif", "PNG", "JPG", "JPEG", "GIF" };

                int _width = 0;
                int _height = 0;

                if (width != null && height != null)
                {
                    _width = width.Equals(string.Empty) ? 0 : Convert.ToInt32(width);
                    _height = height.Equals(string.Empty) ? 0 : Convert.ToInt32(height);
                }

                res = TokenUtils.Token_check(token, userid, HttpInfoUtils.GetIP(), HttpInfoUtils.GetOSVersion());
                if (res.Code == 100)
                {
                    int Maxsize = 5; //M
                    var Files = Request.Form.Files;
                    long Size = Files.Sum(f => f.Length);
                    if (Size > Maxsize * 1024 * 1024) { res.Code = 0; res.Msg = "文件不能超过" + Maxsize + "M"; return res; }
                    foreach (var File in Files)
                    {
                        var FileName = ContentDispositionHeaderValue.Parse(File.ContentDisposition).FileName.Trim('"');
                        //if (FileName.Split('.').Length > 2) { res.Code = 0; res.Msg = "不要搞事"; return res; }
                        bool pass = false;

                        string Suffix = "";

                        foreach (string str in FileName.Split('.'))
                        {
                            if (PictureFormatArray.Contains(str))
                            {
                                pass = true;
                                Suffix = "." + str;
                            }
                        }

                        if (!pass)
                        {
                            res.Code = 0;
                            string type = "";
                            foreach (string str in PictureFormatArray)
                            {
                                type += str + ",";
                            }
                            res.Msg = "请上传图片类型为" + type + "的文件";
                            return res;
                        }
                        FileName = userid + Guid.NewGuid() + "." + Suffix;
                        string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload");
                        if (!Directory.Exists(FilePath)) { Directory.CreateDirectory(FilePath); }
                        string FileFullName = Path.Combine(FilePath, FileName);

                        //压缩图片
                        using var image = Image.Load<Rgba32>(File.OpenReadStream());
                        if (_width != 0 || _height != 0)
                        {
                            image.Mutate(x => x.Resize(_width, _height));
                        }
                        image.Save(FileFullName);

                        Dictionary<string, string> dic = new Dictionary<string, string>
                        {
                            { "imgname", FileName }
                        };

                        res.Done(dic, "上传成功");
                    }
                }
            }
            catch (Exception ex)
            {
                res.Error("前台上传图片出错");
                NLogHelper._.Error(res.Msg, ex);
            }
            return res;
        }

        /// <summary>
        /// 后台上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result UploadImagesAdmin(string token_admin,string userid_admin,string width,string height)
        {
            Result res = new Result();
            try
            {
                
                string[] PictureFormatArray = { "png", "jpg", "jpeg", "gif", "PNG", "JPG", "JPEG", "GIF" };

                int _width = 0;
                int _height = 0;

                if (width != null && height != null)
                {
                    _width = width.Equals(string.Empty) ? 0 : Convert.ToInt32(width);
                    _height = height.Equals(string.Empty) ? 0 : Convert.ToInt32(height);
                }

                res = TokenUtils.Token_admin_check(token_admin, userid_admin, HttpInfoUtils.GetIP(), HttpInfoUtils.GetOSVersion());
                if (res.Code == 100)
                {
                    int Maxsize = 5; //M
                    var Files = Request.Form.Files;
                    long Size = Files.Sum(f => f.Length);
                    if (Size > Maxsize * 1024 * 1024) { res.Code = 0; res.Msg = "文件不能超过" + Maxsize + "M"; return res; }
                    foreach (var File in Files)
                    {
                        var FileName = ContentDispositionHeaderValue.Parse(File.ContentDisposition).FileName.Trim('"');
                        //if (FileName.Split('.').Length > 2) { res.Code = 0; res.Msg = "不要搞事"; return res; }
                        bool pass = false;

                        string Suffix = "";

                        foreach (string str in FileName.Split('.'))
                        {
                            if (PictureFormatArray.Contains(str))
                            {
                                pass = true;
                                Suffix = "." + str;
                            }
                        }

                        if (!pass)
                        {
                            res.Code = 0;
                            string type = "";
                            foreach (string str in PictureFormatArray)
                            {
                                type += str + ",";
                            }
                            res.Msg = "请上传图片类型为" + type + "的文件";
                            return res;
                        }
                        FileName = userid_admin + Guid.NewGuid() + "." + Suffix;
                        string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload");
                        if (!Directory.Exists(FilePath)) { Directory.CreateDirectory(FilePath); }
                        string FileFullName = Path.Combine(FilePath, FileName);

                        //压缩图片
                        using var image = Image.Load<Rgba32>(File.OpenReadStream());
                        if (_width != 0 || _height != 0)
                        {
                            image.Mutate(x => x.Resize(_width, _height));
                        }
                        image.Save(FileFullName);

                        
                        Dictionary<string, string> dic = new Dictionary<string, string>
                        {
                            { "imgname", FileName }
                        };

                        res.Done(dic, "上传成功");
                    }
                }
            }
            catch (Exception ex)
            {
                res.Error("后台上传文件出错");
                NLogHelper._.Error(res.Msg, ex);
            }
            return res;
        }

        /// <summary>
        /// 后台上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result UploadFiles(string token_admin,string userid_admin)
        {
            Result res = new Result();
            try
            {
               

                string[] PictureFormatArray = { "xls", "xlsx" };
                res = TokenUtils.Token_admin_check(token_admin, userid_admin, HttpInfoUtils.GetIP(), HttpInfoUtils.GetOSVersion());
                if (res.Code == 100)
                {
                    int Maxsize = 5; //M
                    var Files = Request.Form.Files;
                    long Size = Files.Sum(f => f.Length);
                    if (Size > Maxsize * 1024 * 1024) { res.Code = 0; res.Msg = "文件不能超过" + Maxsize + "M"; return res; }
                    foreach (var File in Files)
                    {
                        var FileName = ContentDispositionHeaderValue.Parse(File.ContentDisposition).FileName.Trim('"');
                        if (FileName.Split('.').Length > 2) { res.Code = 0; res.Msg = "不要搞事"; return res; }
                        string Suffix = FileName.Split('.')[1];
                        if (!PictureFormatArray.Contains(Suffix))
                        {
                            res.Code = 0;
                            string type = "";
                            foreach (string str in PictureFormatArray)
                            {
                                type += str + ",";
                            }
                            res.Msg = "请上传图片类型为" + type + "的文件";
                            return res;
                        }
                        FileName = userid_admin + Guid.NewGuid() + "." + Suffix;
                        string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload");
                        if (!Directory.Exists(FilePath)) { Directory.CreateDirectory(FilePath); }
                        string FileFullName = Path.Combine(FilePath, FileName);

                        using (FileStream fs = System.IO.File.Create(FileFullName))
                        {
                            File.CopyTo(fs);
                            fs.Flush();
                        }

                        Dictionary<string, string> dic = new Dictionary<string, string>
                        {
                            { "imgname", FileName }
                        };

                        res.Done(dic, "上传成功");
                    }
                }
            }
            catch (Exception ex)
            {
                res.Error("后台上传文件出错");
                NLogHelper._.Error(res.Msg, ex);
            }
            return res;
        }
    }
}

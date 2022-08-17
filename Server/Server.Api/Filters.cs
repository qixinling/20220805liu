using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Models;
using Server.Utils.Configuration_Utils;
using Server.Utils.Crypto_Utils;
using Server.Utils.Http_Utils;
using Server.Utils.Permission_Utils;
using Server.Utils.Token_Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Server.Api
{
    public class Filters
    {
        /// <summary>
        /// 前台登录检测过滤器
        /// </summary>
        public class TokenCheckFilters : ActionFilterAttribute
        {
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
            public override async void OnActionExecuting(ActionExecutingContext context)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
            {
                //获取body内容
                var request = context.HttpContext.Request;
                request.EnableBuffering();
                request.Body.Position = 0;
                var requestReader = new StreamReader(request.Body);
                var requestContent = await requestReader.ReadToEndAsync();
                request.Body.Position = 0;
                JObject content = JObject.Parse(requestContent);

                string Userid = content.ContainsKey("userid") ? content["userid"].ToString() : "";
                string Token = content.ContainsKey("token") ? content["token"].ToString() : "";

                Result res = TokenUtils.Token_check(Token, Userid, HttpInfoUtils.GetIP(), HttpInfoUtils.GetOSVersion());
                if (res.Code != 100)
                {
                    context.Result = new JsonResult(res);
                    return;
                }
            }
        }

        /// <summary>
        /// 后台登录检测过滤器
        /// </summary>
        public class TokenAdminCheckFilters : ActionFilterAttribute
        {
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
            public override async void OnActionExecuting(ActionExecutingContext context)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
            {
                //获取body内容
                var request = context.HttpContext.Request;
                request.EnableBuffering();
                request.Body.Position = 0;
                var requestReader = new StreamReader(request.Body);
                var requestContent = await requestReader.ReadToEndAsync();
                request.Body.Position = 0;
                JObject content = JObject.Parse(requestContent);

                string Userid_admin = content.ContainsKey("userid_admin") ? content["userid_admin"].ToString() : "";
                string Token_admin = content.ContainsKey("token_admin") ? content["token_admin"].ToString() : "";

                Result res = TokenUtils.Token_admin_check(Token_admin, Userid_admin, HttpInfoUtils.GetIP(), HttpInfoUtils.GetOSVersion());

                if (res.Code != 100)
                {
                    context.Result = new JsonResult(res);
                    return;
                }
            }
        }

        /// <summary>
        /// 后台权限检测
        /// </summary>
        public class PermissionCheckFilters : ActionFilterAttribute
        {
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
            public override async void OnActionExecuting(ActionExecutingContext context)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
            {
                //获取body内容
                var request = context.HttpContext.Request;
                request.EnableBuffering();
                request.Body.Position = 0;
                var requestReader = new StreamReader(request.Body);
                var requestContent = await requestReader.ReadToEndAsync();
                request.Body.Position = 0;
                JObject content = JObject.Parse(requestContent);

                string Userid_admin = content.ContainsKey("userid_admin") ? content["userid_admin"].ToString() : "";

                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

                if (!PermissionUtils.CheckPermission(Userid_admin, descriptor.ControllerName, descriptor.ActionName))
                {
                    Result res = new Result();
                    res.Fail("没有权限");

                    context.Result = new JsonResult(res);
                    return;
                }
            }
        }

        /// <summary>
        /// 签名检测
        /// </summary>
        public class SignCheckFilters : ActionFilterAttribute
        {
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
            public override async void OnActionExecuting(ActionExecutingContext context)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
            {
                //获取body内容
                var request = context.HttpContext.Request;
                request.EnableBuffering();
                request.Body.Position = 0;
                var requestReader = new StreamReader(request.Body);
                var requestContent = await requestReader.ReadToEndAsync();
                request.Body.Position = 0;
                JObject content = JObject.Parse(requestContent);

                Result res = new Result();
                bool pass = false;

                string projectid = ConfigUtils.Configuration["Project"]; // 获取项目编号
                string postSign = "";
                StringBuilder str = new StringBuilder(projectid);

                List<string> keys = new List<string>();
                foreach (var item in content)
                {
                    keys.Add(item.Key);
                }
                string[] arrKeys = keys.ToArray();
                Array.Sort(arrKeys, string.CompareOrdinal);
                foreach (var key in arrKeys)
                {
                    string value = content[key].ToString();
                    if (key.Equals("sign"))
                    {
                        postSign = value;
                    }
                    else
                    {
                    //前端如果出现bool传值,会导致true/false变成大写开头,导致验签失败
                        switch (value)
                        {
                            case "True":
                                value = "true";
                                break;
                            case "False":
                                value = "false";
                                break;
                        }
                        str.Append(key + "=" + value + "&");
                    }
                }

                if (str.Equals(projectid)) { pass = true; goto check; } //没有post数据时不需要验证sign

                //有post数据的情况下,判断postsign是否为空,空则验证失败
                if (string.IsNullOrEmpty(postSign)) { goto check; }

                //去掉最后一位的&并拼接项目编号,生成签名进行比对
                string s = MD5Utils.MD5Encrypt(str.ToString().Substring(0, str.Length - 1) + projectid, 32).ToUpper();
                if (postSign.Equals(s)) { pass = true; goto check; }

            check:
                if (!pass)
                {
                    res.Fail("签名验证失败");
                    context.Result = new JsonResult(res);
                    return;
                }
            }
        }
    }
}

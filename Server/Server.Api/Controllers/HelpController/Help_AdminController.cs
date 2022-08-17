using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Api.Method;
using Server.Models;
using Server.Models.DataBaseModels;
using Server.Utils.Http_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Logs;
using static Server.Api.Filters;
using Newtonsoft.Json.Linq;

namespace Server.Api.Controllers.HelpController
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Help_AdminController : ControllerBase
    {
        private readonly DbConnect _dbConnect;
        private readonly Result _res;
        public Help_AdminController(DbConnect dbConnect, Result res)
        {
            _dbConnect = dbConnect;
            _res = res;
        }

        /// <summary>
        /// 修改排序
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result ChangeRank(JObject data)
        {
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                int qid = Convert.ToInt32(data["qid"]);
                int rank = Convert.ToInt32(data["rank"]);

                
                HelpMethod hm = new HelpMethod(_dbConnect );
                DbHelp DbHelp = hm.GetById(qid);

                if (DbHelp == null) { _res.Fail("该常见问题不存在"); return _res; }
                DbHelp.Rank = rank;
                if (_dbConnect.SaveChanges() > 0) { _res.Done(null, "修改已保存"); return _res; }

                _res.Fail("修改失败");

                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 11, "常见问题排序:" + DbHelp.Question);
            }
            catch (Exception ex)
            {
                _res.Error("修改排序出错");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改显示开关
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result ChangeShow(JObject data)
        {
            
           
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                int qid = Convert.ToInt32(data["qid"]);


                HelpMethod hm = new HelpMethod(_dbConnect);
                DbHelp help = hm.GetById(qid);
     
                if (help == null) { _res.Fail("该常见问题不存在"); return _res; }

                if (help.Show == 0) { help.Show = 1; }
                else { help.Show = 0; }
                if (_dbConnect.SaveChanges() > 0) { _res.Done(null, "修改已保存"); return _res; }
                _res.Fail("修改失败");
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 11, "常见问题显示开关:" + help.Question);
            }
            catch (Exception ex)
            {
                _res.Error("修改显示开关异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改置顶开关
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result ChangeTop(JObject data)
        {
            
           
            try
            {
                string userid_admin = data["userid_admin"].ToString();
                int qid = Convert.ToInt32(data["qid"]);

                HelpMethod hm = new HelpMethod(_dbConnect);
                DbHelp help = hm.GetById(qid);
                if (help == null) { _res.Fail("该常见问题不存在"); return _res; }

                if (help.Hlevel == 0) { help.Hlevel = 1; }
                else { help.Hlevel = 0; }
                if (_dbConnect.SaveChanges() > 0) { _res.Done(null,"修改已保存"); }
                _res.Fail("修改失败");
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 11, "修改常见问题置顶:" + help.Question);
            }
            catch (Exception ex)
            {
                _res.Error("修改置顶开关异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 添加常见问题
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
                string userid_admin = data["userid_admin"].ToString();
                string jsonhelp = data["jsonhelp"].ToString();

                DbHelp help = JsonConvert.DeserializeObject<DbHelp>(jsonhelp);
                if (help.Gpath.Equals("")) { help.Gpath = ","; }
                else { help.Gpath = "," + help.Gpath + ","; }
               
                HelpMethod hm = new HelpMethod(_dbConnect);
                hm.Add(help);


                if (_dbConnect.SaveChanges() > 0 && help.Gpath.Equals(",")) { _res.Done(null, "添加成功"); return _res; }
                foreach (string gid in help.Gpath.Split(',').Where(s => !string.IsNullOrEmpty(s)).ToArray())
                {
                    DbHelp help2 = hm.GetById(Convert.ToInt32(gid));
                    if (help2 == null) { continue; }
                    help2.Hpath = help2.Hpath + help.Id + ",";
                }
                if (_dbConnect.SaveChanges() > 0) { _res.Done(null, "添加成功"); return _res; }
                _res.Fail("添加失败");
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 11, "添加常见问题:" + help.Question);
            }
            catch (Exception ex)
            {
                _res.Error("添加常见问题异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 修改回复
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
                string jsonhelp = data["jsonhelp"].ToString();
                DbHelp help = JsonConvert.DeserializeObject<DbHelp>(jsonhelp);
                if (help.Gpath.Equals("")) { help.Gpath = ","; }
                else { help.Gpath = "," + help.Gpath + ","; }

                HelpMethod hm = new HelpMethod(_dbConnect);
                DbHelp help2 = hm.GetById(help.Id);
                if (help2 == null) { _res.Fail("该常见问题不存在");  return _res; }
                help2.Gpath = help.Gpath;
                help2.Zan = help.Zan;
                help2.Cai = help.Cai;
                help2.Hlevel = help.Hlevel;
                help2.Question = help.Question;
                help2.Answer = help.Answer;
                help2.Show = help.Show;
                help2.Rank = help.Rank;

                foreach (string Gid in help.Gpath.Split(',').Where(s => !string.IsNullOrEmpty(s)).ToArray())
                {
                    DbHelp help3 = hm.GetById(Convert.ToInt32(Gid));
                    if (help3 == null) { continue; }
                    if (help3.Hpath.Contains("," + help2.Id + ",")) { continue; }
                    help3.Hpath = help3.Hpath + help2.Id + ",";
                }
                if (_dbConnect.SaveChanges() > 0) { _res.Done(null, "修改成功"); return _res; }
                _res.Fail("修改失败");
                SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 11, "修改问题回复:" + help.Question);
            }
            catch (Exception ex)
            {
                _res.Error("修改回复异常");
               
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }
        /// <summary>
        /// 删除
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
                string delete_id = data["delete_id"].ToString();

                string Msg = "";
                int i = 0;
                
                string[] Dellist = delete_id.Split(',');
                List<string> Cname = new List<string>();

                HelpMethod hm = new HelpMethod(_dbConnect);

                foreach (string Id in Dellist)
                {
                    if (i > 0) { Msg += ","; }
                    i++;
                    string Path_id = "," + Id + ",";
                    List<DbHelp> hlist =hm.GetList().Where(h => h.Hpath.Contains(Path_id)).ToList();
                    foreach (DbHelp h in hlist)
                    {
                        h.Hpath = h.Hpath.Replace(Path_id, ",");
                    }

                    DbHelp hp = hm.GetById(Convert.ToInt32(Id));
                    if (hp != null)
                    {
                        Cname.Add(hp.Question);
                        hm.Remove(Convert.ToInt32(Id));
                        if (_dbConnect.SaveChanges() > 0)
                        {
                            Msg += "ID：" + Id + "删除成功";
                        }
                        else
                        {
                            Msg += "ID：" + Id + "删除失败";
                        }
                    }
                }
                _res.Done(null, Msg);
                foreach (string Name in Cname)
                {
                    SystemLogMethod.Add(userid_admin, HttpInfoUtils.GetIP(), 11, "删除问题:" + Name);
                }
            }
            catch (Exception ex)
            {
                _res.Error("删除异常");
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 常见问题列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Question_List()
        {
           
            try
            {
                HelpMethod hm = new HelpMethod(_dbConnect);
                List<DbHelp> hlsit = hm.GetList().OrderBy(h => h.Rank).ToList();
                List<Dictionary<string, string>> diclist = new List<Dictionary<string, string>>();
                foreach (DbHelp h in hlsit)
                {
                  
                    diclist.Add(HelpUtils.GetDic(h));
                   
                }
                _res.Done(diclist, "查询成功");
              
            }
            catch (Exception ex)
            {
                _res.Error("求助列表异常");
            
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 获取单个常见问题
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [TokenAdminCheckFilters]
        [PermissionCheckFilters]
        [SignCheckFilters]
        public Result Question_Get(JObject data)
        {
            
          try
            {
                int qid =Convert.ToInt32(data["did"]);
                HelpMethod hm = new HelpMethod(_dbConnect);
                DbHelp hp = hm.GetById(qid);
                if (hp == null) { _res.Fail("该常见问题不存在"); return _res; }
                _res.Done(HelpUtils.GetDic(hp), "查询成功");
                
            }
            catch (Exception ex)
            {
                _res.Error("获取单个常见问题异常");
                
                NLogHelper._.Error(_res.Msg, ex);
            }
            return _res;
        }

        /// <summary>
        /// 关联选择
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result Select()
        {
            HelpMethod hm = new HelpMethod(_dbConnect);
            List<DbHelp> hlsit =hm.GetList().Where(h => h.Id > 0).ToList();
            foreach (DbHelp h in hlsit)
            {
                _res.Done(HelpUtils.GetDic(h),"");
            }
            return _res;
        }
        

    }
}

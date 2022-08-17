using Server.Logs;
using Server.Models;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class SystemLogMethod : IDbModMethod<DbSystemLog>
    {
        public DbConnect _dbConnect { get; }

        public SystemLogMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public static string GetLxName(int Lx)
        {
            return Lx switch
            {
                0 => "登录",
                1 => "会员",
                2 => "充值",
                3 => "提现",
                4 => "转账",
                5 => "转换",
                6 => "增减",
                7 => "商城",
                8 => "订单",
                9 => "系统",
                10 => "权限",
                11 => "其他操作",
                _ => "未知操作",
            };
        }

        /// <summary>
        /// 写入操作日志
        /// </summary>
        public static void Add(string Userid, string IP, int Lx, string Bz = null, string OldData = null, string NewData = null, int? Uid = null)
        {
            try
            {
                using DbConnect _dbConnect = DbConnectUtils.GetDbContext();
                DbSystemLog systemLog = new DbSystemLog
                {
                    Userid = Userid,
                    Ip = IP,
                    Lx = Lx,
                    LxName = GetLxName(Lx),
                    Bz = Bz,
                    Ldate = DateTime.Now,
                    OldData = OldData,
                    NewData = NewData,
                    Uid = Uid
                };
                _dbConnect.DbSystemLog.Add(systemLog);
                _dbConnect.SaveChanges();
            }
            catch (Exception ex)
            {
                NLogHelper._.Error("添加操作日志出错", ex);
            }
        }

        public DbSystemLog Add(DbSystemLog dbData)
        {
            _dbConnect.DbSystemLog.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            //此处执行isDel=1操作,如无该字段,则不实现此接口
            DbSystemLog obj = GetById(Id);
            if (obj == null) { return; }
            obj.IsDel = 1;
        }

        public DbSystemLog GetById(int Id)
        {
            DbSystemLog obj = _dbConnect.DbSystemLog.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbSystemLog> GetList()
        {
            return _dbConnect.DbSystemLog.ToList();
        }

        public void Remove(int Id)
        {
            DbSystemLog obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbSystemLog.Remove(obj);
        }
    }
}

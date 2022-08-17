using Microsoft.EntityFrameworkCore;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class BillMethod : IDbModMethod<DbBill>
    {
        public DbConnect _dbConnect { get; }

        public BillMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbBill Add(DbBill dbData)
        {
            _dbConnect.DbBill.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            //此处执行isDel=1操作,如无该字段,则不实现此接口
            DbBill obj = GetById(Id);
            if (obj == null) { return; }
            obj.Isdel = 1;
        }

        public DbBill GetById(int Id)
        {
            DbBill obj = _dbConnect.DbBill.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbBill> GetList()
        {
            return _dbConnect.DbBill.Include(b => b.DbBillAmount).OrderByDescending(b => b.Id).ToList();
        }

        public List<DbBill> GetListByUid(int Uid)
        {
            return _dbConnect.DbBill.Include(b => b.DbBillAmount).Where(b => b.Uid == Uid).OrderByDescending(b => b.Id).ToList();
        }

        public List<DbBill> GetListByUidYearMonth(int Uid, int Year, int Month)
        {
            return _dbConnect.DbBill.Include(b => b.DbBillAmount).Where(b => b.Uid == Uid && b.Bdate.Year == Year && b.Bdate.Month == Month && b.State == 1 && b.Isdel == 0).OrderByDescending(b => b.Id).ToList();
        }


        public void Remove(int Id)
        {
            DbBill obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbBill.Remove(obj);
        }
    }
}

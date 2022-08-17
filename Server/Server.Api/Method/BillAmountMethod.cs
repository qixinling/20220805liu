using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class BillAmountMethod : IDbModMethod<DbBillAmount>
    {
        public DbConnect _dbConnect { get; }

        public BillAmountMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbBillAmount Add(DbBillAmount dbData)
        {
            _dbConnect.DbBillAmount.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbBillAmount GetById(int Id)
        {
            DbBillAmount objAmount = _dbConnect.DbBillAmount.FirstOrDefault(b => b.Id == Id);
            return objAmount;
        }

        public List<DbBillAmount> GetList()
        {
            return _dbConnect.DbBillAmount.ToList();
        }

        public void Remove(int Id)
        {
            DbBillAmount objAmount = GetById(Id);
            if (objAmount == null) { return; }
            _dbConnect.DbBillAmount.Remove(objAmount);
        }
    }
}

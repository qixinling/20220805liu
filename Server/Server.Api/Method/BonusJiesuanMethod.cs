using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class BonusJiesuanMethod : IDbModMethod<DbBonusJiesuan>
    {
        public DbConnect _dbConnect { get; }

        public BonusJiesuanMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbBonusJiesuan Add(DbBonusJiesuan dbData)
        {
            _dbConnect.DbBonusJiesuan.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbBonusJiesuan GetById(int Id)
        {
            DbBonusJiesuan obj = _dbConnect.DbBonusJiesuan.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbBonusJiesuan> GetList()
        {
            return _dbConnect.DbBonusJiesuan.ToList();
        }

        public void Remove(int Id)
        {
            DbBonusJiesuan obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbBonusJiesuan.Remove(obj);
        }
    }
}

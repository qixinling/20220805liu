using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class BonusSourceMethod : IDbModMethod<DbBonusSource>
    {
        public DbConnect _dbConnect { get; }

        public BonusSourceMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbBonusSource Add(DbBonusSource dbData)
        {
            _dbConnect.DbBonusSource.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbBonusSource GetById(int Id)
        {
            DbBonusSource obj = _dbConnect.DbBonusSource.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbBonusSource> GetList()
        {
            return _dbConnect.DbBonusSource.ToList();
        }

        public void Remove(int Id)
        {
            DbBonusSource obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbBonusSource.Remove(obj);
        }
    }
}

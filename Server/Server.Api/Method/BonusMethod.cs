using Microsoft.EntityFrameworkCore;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class BonusMethod : IDbModMethod<DbBonus>
    {
        public DbConnect _dbConnect { get; }

        public BonusMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public List<DbBonus> List()
        {
            return _dbConnect.DbBonus.Include(t => t.DbBonusSource).ToList();
        }
        public DbBonus Add(DbBonus dbData)
        {
            _dbConnect.DbBonus.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbBonus GetById(int Id)
        {
            DbBonus obj = _dbConnect.DbBonus.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbBonus> GetList()
        {
            return _dbConnect.DbBonus.ToList();
        }

        public List<DbBonus> GetList(int a,int b)
        {
            return _dbConnect.DbBonus.ToList();
        }

        public void Remove(int Id)
        {
            DbBonus obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbBonus.Remove(obj);
        }
    }
}

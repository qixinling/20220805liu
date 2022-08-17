using Microsoft.EntityFrameworkCore;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class UsersMethod : IDbModMethod<DbUsers>
    {
        public DbConnect _dbConnect { get; }

        public UsersMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public List<DbUsers> List()
        {
            return _dbConnect.DbUsers.Include(u => u.DbWallets).ToList();
        }
        public DbUsers Add(DbUsers dbData)
        {
            _dbConnect.DbUsers.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbUsers GetById(int Id)
        {
            DbUsers obj = _dbConnect.DbUsers.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public DbUsers GetByUsersid(string Userid)
        {
            DbUsers obj = _dbConnect.DbUsers.FirstOrDefault(b => b.Userid == Userid);
            return obj;
        }

        public List<DbUsers> GetList()
        {
            return _dbConnect.DbUsers.ToList();
        }

        public void Remove(int Id)
        {
            DbUsers obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbUsers.Remove(obj);
        }
    }
}

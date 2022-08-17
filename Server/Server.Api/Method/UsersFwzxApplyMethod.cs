using Microsoft.EntityFrameworkCore;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class UsersFwzxApplyMethod : IDbModMethod<DbUsersFwzxApply>
    {
        public DbConnect _dbConnect { get; }

        public UsersFwzxApplyMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public List<DbUsersFwzxApply> List()
        {
            return _dbConnect.DbUsersFwzxApply.Include(f => f.UidNavigation).ToList();
        }
        public DbUsersFwzxApply Add(DbUsersFwzxApply dbData)
        {
            _dbConnect.DbUsersFwzxApply.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbUsersFwzxApply GetById(int Id)
        {
            DbUsersFwzxApply obj = _dbConnect.DbUsersFwzxApply.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbUsersFwzxApply> GetList()
        {
            return _dbConnect.DbUsersFwzxApply.ToList();
        }

        public void Remove(int Id)
        {
            DbUsersFwzxApply obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbUsersFwzxApply.Remove(obj);
        }
    }
}

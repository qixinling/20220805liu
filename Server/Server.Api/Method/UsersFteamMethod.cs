using Microsoft.EntityFrameworkCore;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class UsersFteamMethod : IDbModMethod<DbUsersFteam>
    {
        public DbConnect _dbConnect { get; }

        public UsersFteamMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }
        public List<DbUsersFteam> List()
        {
            return _dbConnect.DbUsersFteam.Include(uf => uf.UidNavigation).ToList();
        }
        public DbUsersFteam Add(DbUsersFteam dbData)
        {
            _dbConnect.DbUsersFteam.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbUsersFteam GetById(int Id)
        {
            DbUsersFteam obj = _dbConnect.DbUsersFteam.FirstOrDefault(b => b.Id == Id);
            return obj;
        }
        public DbUsersFteam GetByUid(int Uid)
        {
            DbUsersFteam obj = _dbConnect.DbUsersFteam.FirstOrDefault(b => b.Uid == Uid);
            return obj;
        }

        public DbUsersFteam GetByUserid(string Userid)
        {
            DbUsersFteam obj = _dbConnect.DbUsersFteam.FirstOrDefault(f => f.Userid.Equals(Userid));
            return obj;
        }
        public List<DbUsersFteam> GetList()
        {
            return _dbConnect.DbUsersFteam.ToList();
        }

        public void Remove(int Id)
        {
            DbUsersFteam obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbUsersFteam.Remove(obj);
        }
    }
}

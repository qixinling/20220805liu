using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class UsersBankMethod : IDbModMethod<DbUsersBank>
    {
        public DbConnect _dbConnect { get; }

        public UsersBankMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbUsersBank Add(DbUsersBank dbData)
        {
            _dbConnect.DbUsersBank.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbUsersBank GetById(int Id)
        {
            DbUsersBank obj = _dbConnect.DbUsersBank.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbUsersBank> GetList()
        {
            return _dbConnect.DbUsersBank.ToList();
        }
        public List<DbUsersBank> GetUidList(int Uid)
        {
            return _dbConnect.DbUsersBank.Where(u => u.Uid == Uid).ToList();
        }

        public List<DbUsersBank> GetUseridList(string Userid)
        {
            return _dbConnect.DbUsersBank.Where(u => u.Userid.Equals(Userid)).ToList();
        }
        public void Remove(int Id)
        {
            DbUsersBank obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbUsersBank.Remove(obj);
        }
    }
}

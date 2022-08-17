using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class UsersAddressMethod : IDbModMethod<DbUsersAddress>
    {
        public DbConnect _dbConnect { get; }

        public UsersAddressMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbUsersAddress Add(DbUsersAddress dbData)
        {
            _dbConnect.DbUsersAddress.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbUsersAddress GetById(int Id)
        {
            DbUsersAddress obj = _dbConnect.DbUsersAddress.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbUsersAddress> GetList()
        {
            return _dbConnect.DbUsersAddress.ToList();
        }

        public List<DbUsersAddress> GetUidList(int Uid)
        {
            return _dbConnect.DbUsersAddress.Where(u => u.Uid == Uid).ToList();
        }

        public void Remove(int Id)
        {
            DbUsersAddress obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbUsersAddress.Remove(obj);
        }
    }
}

using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class UsersLevelupMethod : IDbModMethod<DbUsersLevelup>
    {
        public DbConnect _dbConnect { get; }

        public UsersLevelupMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbUsersLevelup Add(DbUsersLevelup dbData)
        {
            _dbConnect.DbUsersLevelup.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbUsersLevelup GetById(int Id)
        {
            DbUsersLevelup obj = _dbConnect.DbUsersLevelup.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbUsersLevelup> GetList()
        {
            return _dbConnect.DbUsersLevelup.ToList();
        }

        public void Remove(int Id)
        {
            DbUsersLevelup obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbUsersLevelup.Remove(obj);
        }
    }
}

using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class UsersDeleteMethod : IDbModMethod<DbUsersDelete>
    {
        public DbConnect _dbConnect { get; }

        public UsersDeleteMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbUsersDelete Add(DbUsersDelete dbData)
        {
            _dbConnect.DbUsersDelete.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbUsersDelete GetById(int Id)
        {
            DbUsersDelete obj = _dbConnect.DbUsersDelete.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbUsersDelete> GetList()
        {
            return _dbConnect.DbUsersDelete.ToList();
        }

        public void Remove(int Id)
        {
            DbUsersDelete obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbUsersDelete.Remove(obj);
        }
    }
}

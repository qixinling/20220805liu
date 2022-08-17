using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class SystemAdminGroupMethod : IDbModMethod<DbSystemAdminGroup>
    {
        public DbConnect _dbConnect { get; }

        public SystemAdminGroupMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbSystemAdminGroup Add(DbSystemAdminGroup dbData)
        {
            _dbConnect.DbSystemAdminGroup.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbSystemAdminGroup GetById(int Id)
        {
            DbSystemAdminGroup obj = _dbConnect.DbSystemAdminGroup.FirstOrDefault(b => b.Id == Id);
            return obj;
        }
        public DbSystemAdminGroup GetByGroupname(string Groupname)
        {
            DbSystemAdminGroup obj = _dbConnect.DbSystemAdminGroup.FirstOrDefault(b => b.Groupname == Groupname);
            return obj;
        }
        public List<DbSystemAdminGroup> GetList()
        {
            return _dbConnect.DbSystemAdminGroup.ToList();
        }

        public void Remove(int Id)
        {
            DbSystemAdminGroup obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbSystemAdminGroup.Remove(obj);
        }
    }
}

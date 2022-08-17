using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class SystemAdminMethod : IDbModMethod<DbSystemAdmin>
    {
        public DbConnect _dbConnect { get; }

        public SystemAdminMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbSystemAdmin Add(DbSystemAdmin dbData)
        {
            _dbConnect.DbSystemAdmin.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbSystemAdmin GetById(int Id)
        {
            DbSystemAdmin obj = _dbConnect.DbSystemAdmin.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbSystemAdmin> GetList()
        {
            return _dbConnect.DbSystemAdmin.ToList();
        }

        public void Remove(int Id)
        {
            DbSystemAdmin obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbSystemAdmin.Remove(obj);
        }
    }
}

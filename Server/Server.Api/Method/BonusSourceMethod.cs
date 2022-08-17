using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class CheckcodeMethod : IDbModMethod<DbCheckcode>
    {
        public DbConnect _dbConnect { get; }

        public CheckcodeMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbCheckcode Add(DbCheckcode dbData)
        {
            _dbConnect.DbCheckcode.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbCheckcode GetById(int Id)
        {
            DbCheckcode obj = _dbConnect.DbCheckcode.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbCheckcode> GetList()
        {
            return _dbConnect.DbCheckcode.ToList();
        }

        public void Remove(int Id)
        {
            DbCheckcode obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbCheckcode.Remove(obj);
        }
    }
}

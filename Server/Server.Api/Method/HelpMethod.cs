using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class HelpMethod : IDbModMethod<DbHelp>
    {
        public DbConnect _dbConnect { get; }

        public HelpMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbHelp Add(DbHelp dbData)
        {
            _dbConnect.DbHelp.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbHelp GetById(int Id)
        {
            DbHelp obj = _dbConnect.DbHelp.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbHelp> GetList()
        {
            return _dbConnect.DbHelp.ToList();
        }

        public void Remove(int Id)
        {
            DbHelp obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbHelp.Remove(obj);
        }
    }
}

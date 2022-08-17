using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class NewsMethod : IDbModMethod<DbNews>
    {
        public DbConnect _dbConnect { get; }

        public NewsMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbNews Add(DbNews dbData)
        {
            _dbConnect.DbNews.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbNews GetById(int Id)
        {
            DbNews obj = _dbConnect.DbNews.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbNews> GetList()
        {
            return _dbConnect.DbNews.ToList();
        }

        public void Remove(int Id)
        {
            DbNews obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbNews.Remove(obj);
        }
    }
}

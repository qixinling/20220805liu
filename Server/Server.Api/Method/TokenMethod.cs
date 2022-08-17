using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class TokenMethod : IDbModMethod<DbToken>
    {
        public DbConnect _dbConnect { get; }

        public TokenMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbToken Add(DbToken dbData)
        {
            _dbConnect.DbToken.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbToken GetById(int Id)
        {
            DbToken obj = _dbConnect.DbToken.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbToken> GetList()
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            DbToken obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbToken.Remove(obj);
        }
    }
}

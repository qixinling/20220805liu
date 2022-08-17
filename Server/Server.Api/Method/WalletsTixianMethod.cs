using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class WalletsTixianMethod : IDbModMethod<DbWalletsTixian>
    {
        public DbConnect _dbConnect { get; }

        public WalletsTixianMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbWalletsTixian Add(DbWalletsTixian dbData)
        {
            _dbConnect.DbWalletsTixian.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbWalletsTixian GetById(int Id)
        {
            DbWalletsTixian obj = _dbConnect.DbWalletsTixian.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbWalletsTixian> GetList()
        {
            return _dbConnect.DbWalletsTixian.ToList();
        }

        public void Remove(int Id)
        {
            DbWalletsTixian obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbWalletsTixian.Remove(obj);
        }
    }
}

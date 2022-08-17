using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class WalletsTixianSelectMethod : IDbModMethod<DbWalletsTixianSelect>
    {
        public DbConnect _dbConnect { get; }

        public WalletsTixianSelectMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbWalletsTixianSelect Add(DbWalletsTixianSelect dbData)
        {
            _dbConnect.DbWalletsTixianSelect.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbWalletsTixianSelect GetById(int Id)
        {
            DbWalletsTixianSelect obj = _dbConnect.DbWalletsTixianSelect.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public DbWalletsTixianSelect GetByCid(int Cid)
        {
            DbWalletsTixianSelect obj = _dbConnect.DbWalletsTixianSelect.FirstOrDefault(b => b.Cid == Cid);
            return obj;
        }

        public List<DbWalletsTixianSelect> GetList()
        {
            return _dbConnect.DbWalletsTixianSelect.ToList();
        }

        public void Remove(int Id)
        {
            DbWalletsTixianSelect obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbWalletsTixianSelect.Remove(obj);
        }
    }
}

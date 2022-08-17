using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class WalletsCoinMethod : IDbModMethod<DbWalletsCoin>
    {
        public DbConnect _dbConnect { get; }

        public WalletsCoinMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbWalletsCoin Add(DbWalletsCoin dbData)
        {
            _dbConnect.DbWalletsCoin.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbWalletsCoin GetById(int Id)
        {
            DbWalletsCoin obj = _dbConnect.DbWalletsCoin.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbWalletsCoin> GetList()
        {
            return _dbConnect.DbWalletsCoin.ToList();
        }

        public void Remove(int Id)
        {
            DbWalletsCoin obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbWalletsCoin.Remove(obj);
        }
    }
}

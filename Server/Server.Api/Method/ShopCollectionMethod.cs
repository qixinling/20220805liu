using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class ShopCollectionMethod : IDbModMethod<DbShopCollection>
    {
        public DbConnect _dbConnect { get; }

        public ShopCollectionMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbShopCollection Add(DbShopCollection dbData)
        {
            _dbConnect.DbShopCollection.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbShopCollection GetById(int Id)
        {
            DbShopCollection obj = _dbConnect.DbShopCollection.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public DbShopCollection GetByUid(int Uid)
        {
            DbShopCollection obj = _dbConnect.DbShopCollection.FirstOrDefault(b => b.Uid == Uid);
            return obj;
        }

        public List<DbShopCollection> GetList()
        {
            return _dbConnect.DbShopCollection.ToList();
        }

        public void Remove(int Id)
        {
            DbShopCollection obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbShopCollection.Remove(obj);
        }
    }
}

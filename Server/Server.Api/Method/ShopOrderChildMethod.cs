using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class ShopOrderChildMethod : IDbModMethod<DbShopOrderChild>
    {
        public DbConnect _dbConnect { get; }

        public ShopOrderChildMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbShopOrderChild Add(DbShopOrderChild dbData)
        {
            _dbConnect.DbShopOrderChild.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbShopOrderChild GetById(int Id)
        {
            DbShopOrderChild obj = _dbConnect.DbShopOrderChild.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbShopOrderChild> GetList()
        {
            return _dbConnect.DbShopOrderChild.ToList();
        }

        public void Remove(int Id)
        {
            DbShopOrderChild obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbShopOrderChild.Remove(obj);
        }
    }
}

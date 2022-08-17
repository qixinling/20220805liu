using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class ShopGoodsMethod : IDbModMethod<DbShopGoods>
    {
        public DbConnect _dbConnect { get; }

        public ShopGoodsMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbShopGoods Add(DbShopGoods dbData)
        {
            _dbConnect.DbShopGoods.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbShopGoods GetById(int Id)
        {
            DbShopGoods obj = _dbConnect.DbShopGoods.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbShopGoods> GetList()
        {
            return _dbConnect.DbShopGoods.ToList();
        }

        public void Remove(int Id)
        {
            DbShopGoods obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbShopGoods.Remove(obj);
        }
    }
}

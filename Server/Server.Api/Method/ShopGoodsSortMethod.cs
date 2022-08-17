using Microsoft.EntityFrameworkCore;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class ShopGoodsSortMethod : IDbModMethod<DbShopGoodsSort>
    {
        public DbConnect _dbConnect { get; }

        public ShopGoodsSortMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbShopGoodsSort Add(DbShopGoodsSort dbData)
        {
            _dbConnect.DbShopGoodsSort.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbShopGoodsSort GetById(int Id)
        {
            DbShopGoodsSort obj = _dbConnect.DbShopGoodsSort.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbShopGoodsSort> GetList()
        {
            return _dbConnect.DbShopGoodsSort.ToList();
        }

        
        public void Remove(int Id)
        {
            DbShopGoodsSort obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbShopGoodsSort.Remove(obj);
        }
    }
}

using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class ShopGoodsSortChildMethod : IDbModMethod<DbShopGoodsSortChild>
    {
        public DbConnect _dbConnect { get; }

        public ShopGoodsSortChildMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbShopGoodsSortChild Add(DbShopGoodsSortChild dbData)
        {
            _dbConnect.DbShopGoodsSortChild.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbShopGoodsSortChild GetById(int Id)
        {
            DbShopGoodsSortChild obj = _dbConnect.DbShopGoodsSortChild.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbShopGoodsSortChild> GetList()
        {
            return _dbConnect.DbShopGoodsSortChild.ToList();
        }

        public List<DbShopGoodsSortChild> GetSidList(int Sid)
        {
            return _dbConnect.DbShopGoodsSortChild.Where(s => s.Sid == Sid).ToList();
        }

        public void Remove(int Id)
        {
            DbShopGoodsSortChild obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbShopGoodsSortChild.Remove(obj);
        }
    }
}

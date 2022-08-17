using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class ShopImgMethod : IDbModMethod<DbShopImg>
    {
        public DbConnect _dbConnect { get; }

        public ShopImgMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbShopImg Add(DbShopImg dbData)
        {
            _dbConnect.DbShopImg.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbShopImg GetById(int Id)
        {
            DbShopImg obj = _dbConnect.DbShopImg.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbShopImg> GetList()
        {
            return _dbConnect.DbShopImg.ToList();
        }

        public void Remove(int Id)
        {
            DbShopImg obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbShopImg.Remove(obj);
        }
    }
}

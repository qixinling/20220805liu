using Microsoft.EntityFrameworkCore;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class ShopOrderMethod : IDbModMethod<DbShopOrder>
    {
        public DbConnect _dbConnect { get; }

        public ShopOrderMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public List<DbShopOrder> List()
        {
            return _dbConnect.DbShopOrder.Include(b => b.Bill).ThenInclude(b => b.DbBillAmount).ToList();
        }
        public List<DbShopOrder> List2()
        {
            return _dbConnect.DbShopOrder.Include(or => or.UidNavigation).Include(b => b.Bill).ToList();
        }
        public List<DbShopOrder> List3()
        {
            return _dbConnect.DbShopOrder.Include(r => r.DbShopOrderChild).Include(b => b.Bill).ThenInclude(b => b.DbBillAmount).ToList();
        }
        public DbShopOrder Get(int Id)
        {
            DbShopOrder obj = _dbConnect.DbShopOrder.Include(b => b.Bill).ThenInclude(b => b.DbBillAmount).FirstOrDefault(b => b.Id == Id);
            return obj;
        }
      
        public DbShopOrder Add(DbShopOrder dbData)
        {
            _dbConnect.DbShopOrder.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbShopOrder GetById(int Id)
        {
            DbShopOrder obj = _dbConnect.DbShopOrder.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbShopOrder> GetList()
        {
            return _dbConnect.DbShopOrder.ToList();
        }

        public void Remove(int Id)
        {
            DbShopOrder obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbShopOrder.Remove(obj);
        }
    }
}

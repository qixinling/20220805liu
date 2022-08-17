using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class WalletsZhuanhuanSelectMethod : IDbModMethod<DbWalletsZhuanhuanSelect>
    {
        public DbConnect _dbConnect { get; }

        public WalletsZhuanhuanSelectMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbWalletsZhuanhuanSelect Add(DbWalletsZhuanhuanSelect dbData)
        {
            _dbConnect.DbWalletsZhuanhuanSelect.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbWalletsZhuanhuanSelect GetById(int Id)
        {
            DbWalletsZhuanhuanSelect obj = _dbConnect.DbWalletsZhuanhuanSelect.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbWalletsZhuanhuanSelect> GetList()
        {
            return _dbConnect.DbWalletsZhuanhuanSelect.ToList();
        }

        public void Remove(int Id)
        {
            DbWalletsZhuanhuanSelect obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbWalletsZhuanhuanSelect.Remove(obj);
        }
    }
}

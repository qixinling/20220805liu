using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class WalletsZhuanzhangSelectMethod : IDbModMethod<DbWalletsZhuanzhangSelect>
    {
        public DbConnect _dbConnect { get; }

        public WalletsZhuanzhangSelectMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbWalletsZhuanzhangSelect Add(DbWalletsZhuanzhangSelect dbData)
        {
            _dbConnect.DbWalletsZhuanzhangSelect.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbWalletsZhuanzhangSelect GetById(int Id)
        {
            DbWalletsZhuanzhangSelect obj = _dbConnect.DbWalletsZhuanzhangSelect.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public DbWalletsZhuanzhangSelect GetByCid(int Cid)
        {
            DbWalletsZhuanzhangSelect obj = _dbConnect.DbWalletsZhuanzhangSelect.FirstOrDefault(b => b.Cid == Cid);
            return obj;
        }

        public List<DbWalletsZhuanzhangSelect> GetList()
        {
            return _dbConnect.DbWalletsZhuanzhangSelect.ToList();
        }

        public void Remove(int Id)
        {
            DbWalletsZhuanzhangSelect obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbWalletsZhuanzhangSelect.Remove(obj);
        }
    }
}

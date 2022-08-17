using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class WalletsZhuanzhangMethod : IDbModMethod<DbWalletsZhuanzhang>
    {
        public DbConnect _dbConnect { get; }

        public WalletsZhuanzhangMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbWalletsZhuanzhang Add(DbWalletsZhuanzhang dbData)
        {
            _dbConnect.DbWalletsZhuanzhang.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbWalletsZhuanzhang GetById(int Id)
        {
            DbWalletsZhuanzhang obj = _dbConnect.DbWalletsZhuanzhang.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbWalletsZhuanzhang> GetList()
        {
            return _dbConnect.DbWalletsZhuanzhang.ToList();
        }

        public void Remove(int Id)
        {
            DbWalletsZhuanzhang obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbWalletsZhuanzhang.Remove(obj);
        }
    }
}

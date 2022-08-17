using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class WalletsZhuanhuanMethod : IDbModMethod<DbWalletsZhuanhuan>
    {
        public DbConnect _dbConnect { get; }

        public WalletsZhuanhuanMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbWalletsZhuanhuan Add(DbWalletsZhuanhuan dbData)
        {
            _dbConnect.DbWalletsZhuanhuan.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbWalletsZhuanhuan GetById(int Id)
        {
            DbWalletsZhuanhuan obj = _dbConnect.DbWalletsZhuanhuan.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbWalletsZhuanhuan> GetList()
        {
            return _dbConnect.DbWalletsZhuanhuan.ToList();
        }

        public void Remove(int Id)
        {
            DbWalletsZhuanhuan obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbWalletsZhuanhuan.Remove(obj);
        }
    }
}

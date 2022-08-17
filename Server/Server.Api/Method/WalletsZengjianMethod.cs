using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class WalletsZengjianMethod : IDbModMethod<DbWalletsZengjian>
    {
        public DbConnect _dbConnect { get; }

        public WalletsZengjianMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbWalletsZengjian Add(DbWalletsZengjian dbData)
        {
            _dbConnect.DbWalletsZengjian.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbWalletsZengjian GetById(int Id)
        {
            DbWalletsZengjian obj = _dbConnect.DbWalletsZengjian.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbWalletsZengjian> GetList()
        {
            return _dbConnect.DbWalletsZengjian.ToList();
        }

        public void Remove(int Id)
        {
            DbWalletsZengjian obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbWalletsZengjian.Remove(obj);
        }
    }
}

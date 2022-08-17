using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class WalletsChongzhiMethod : IDbModMethod<DbWalletsChongzhi>
    {
        public DbConnect _dbConnect { get; }

        public WalletsChongzhiMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbWalletsChongzhi Add(DbWalletsChongzhi dbData)
        {
            _dbConnect.DbWalletsChongzhi.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbWalletsChongzhi GetById(int Id)
        {
            DbWalletsChongzhi obj = _dbConnect.DbWalletsChongzhi.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbWalletsChongzhi> GetList()
        {
            return _dbConnect.DbWalletsChongzhi.ToList();
        }

        public void Remove(int Id)
        {
            DbWalletsChongzhi obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbWalletsChongzhi.Remove(obj);
        }
    }
}

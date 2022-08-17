using Microsoft.EntityFrameworkCore;
using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class WalletsMethod : IDbModMethod<DbWallets>
    {
        public DbConnect _dbConnect { get; }

        public WalletsMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public List<DbWallets> GetListIncludeCoin()
        {
            return _dbConnect.DbWallets.Include(u => u.CidNavigation).ToList();
        }
        public List<DbWallets> GetListIncludeUser()
        {
            return _dbConnect.DbWallets.Include(u => u.UidNavigation).ToList();
        }
        public DbWallets Add(DbWallets dbData)
        {
            _dbConnect.DbWallets.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbWallets GetById(int Id)
        {
            DbWallets obj = _dbConnect.DbWallets.FirstOrDefault(b => b.Id == Id);
            return obj;
        }
        public DbWallets GetByUserid(string Userid)
        {
            DbWallets obj = _dbConnect.DbWallets.FirstOrDefault(u => u.Userid.Equals(Userid));
            return obj;
        }

        public List<DbWallets> GetList()
        {
            return _dbConnect.DbWallets.ToList();
        }

        public void Remove(int Id)
        {
            DbWallets obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbWallets.Remove(obj);
        }
    }
}

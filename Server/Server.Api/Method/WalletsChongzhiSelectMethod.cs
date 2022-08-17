using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class WalletsChongzhiSelectMethod : IDbModMethod<DbWalletsChongzhiSelect>
    {
        public DbConnect _dbConnect { get; }

        public WalletsChongzhiSelectMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbWalletsChongzhiSelect Add(DbWalletsChongzhiSelect dbData)
        {
            _dbConnect.DbWalletsChongzhiSelect.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbWalletsChongzhiSelect GetById(int Id)
        {
            DbWalletsChongzhiSelect obj = _dbConnect.DbWalletsChongzhiSelect.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public DbWalletsChongzhiSelect GetByCid(int Cid)
        {
            DbWalletsChongzhiSelect obj = _dbConnect.DbWalletsChongzhiSelect.FirstOrDefault(b => b.Cid == Cid);
            return obj;
        }

        public List<DbWalletsChongzhiSelect> GetList()
        {
            return _dbConnect.DbWalletsChongzhiSelect.ToList();
        }

        public void Remove(int Id)
        {
            DbWalletsChongzhiSelect obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbWalletsChongzhiSelect.Remove(obj);
        }
    }
}

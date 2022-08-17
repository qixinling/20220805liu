using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Api.Method
{
    public class SystemSettingBonusMethod : IDbModMethod<DbSystemSettingBonus>
    {
        public DbConnect _dbConnect { get; }

        public SystemSettingBonusMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbSystemSettingBonus Add(DbSystemSettingBonus dbData)
        {
            _dbConnect.DbSystemSettingBonus.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbSystemSettingBonus GetById(int Id)
        {
            DbSystemSettingBonus obj = _dbConnect.DbSystemSettingBonus.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public DbSystemSettingBonus GetByIndex(int Index)
        {
            DbSystemSettingBonus obj = _dbConnect.DbSystemSettingBonus.FirstOrDefault(b => b.Index == Index);
            return obj;
        }

        public List<DbSystemSettingBonus> GetList()
        {
            return _dbConnect.DbSystemSettingBonus.OrderBy(b => b.Index).ToList();
        }

        public void Remove(int Id)
        {
            DbSystemSettingBonus obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbSystemSettingBonus.Remove(obj);
        }
    }
}

using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class SystemSettingMethod : IDbModMethod<DbSystemSetting>
    {
        public DbConnect _dbConnect { get; }

        public SystemSettingMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }


        public DbSystemSetting Get()
        {
            return _dbConnect.DbSystemSetting.FirstOrDefault();
        }


        public DbSystemSetting Add(DbSystemSetting dbData)
        {
            _dbConnect.DbSystemSetting.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbSystemSetting GetById(int Id)
        {
            DbSystemSetting obj = _dbConnect.DbSystemSetting.FirstOrDefault(b => b.Id == Id);
            return obj;
        }


       
        public List<DbSystemSetting> GetList()
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            DbSystemSetting obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbSystemSetting.Remove(obj);
        }
    }
}

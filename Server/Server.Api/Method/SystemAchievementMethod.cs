using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class SystemAchievementMethod : IDbModMethod<DbSystemAchievement>
    {
        public DbConnect _dbConnect { get; }

        public SystemAchievementMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbSystemAchievement Add(DbSystemAchievement dbData)
        {
            _dbConnect.DbSystemAchievement.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbSystemAchievement GetById(int Id)
        {
            DbSystemAchievement obj = _dbConnect.DbSystemAchievement.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbSystemAchievement> GetList()
        {
            return _dbConnect.DbSystemAchievement.ToList();
        }

        public void Remove(int Id)
        {
            DbSystemAchievement obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbSystemAchievement.Remove(obj);
        }
    }
}

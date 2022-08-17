using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class UsersJihuoRecordMethod : IDbModMethod<DbUsersJihuoRecord>
    {
        public DbConnect _dbConnect { get; }

        public UsersJihuoRecordMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbUsersJihuoRecord Add(DbUsersJihuoRecord dbData)
        {
            _dbConnect.DbUsersJihuoRecord.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbUsersJihuoRecord GetById(int Id)
        {
            DbUsersJihuoRecord obj = _dbConnect.DbUsersJihuoRecord.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public DbUsersJihuoRecord GetByJid(int Jid)
        {
            DbUsersJihuoRecord obj = _dbConnect.DbUsersJihuoRecord.FirstOrDefault(b => b.Jid == Jid);
            return obj;
        }

        public List<DbUsersJihuoRecord> GetList()
        {
            return _dbConnect.DbUsersJihuoRecord.ToList();
        }

        public List<DbUsersJihuoRecord> GetUidList(int Uid)
        {
            return _dbConnect.DbUsersJihuoRecord.Where(f => f.Uid == Uid).ToList();
        }

        public void Remove(int Id)
        {
            DbUsersJihuoRecord obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbUsersJihuoRecord.Remove(obj);
        }
    }
}

using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class MsgMethod : IDbModMethod<DbMsg>
    {
        public DbConnect _dbConnect { get; }

        public MsgMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbMsg Add(DbMsg dbData)
        {
            _dbConnect.DbMsg.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbMsg GetById(int Id)
        {
            DbMsg obj = _dbConnect.DbMsg.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbMsg> GetList()
        {
            return _dbConnect.DbMsg.ToList();
        }

        public void Remove(int Id)
        {
            DbMsg obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbMsg.Remove(obj);
        }
    }
}

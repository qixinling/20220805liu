using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class SlideMethod : IDbModMethod<DbSlide>
    {
        public DbConnect _dbConnect { get; }

        public SlideMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbSlide Add(DbSlide dbData)
        {
            _dbConnect.DbSlide.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbSlide GetById(int Id)
        {
            DbSlide obj = _dbConnect.DbSlide.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbSlide> GetList()
        {
            return _dbConnect.DbSlide.ToList();
        }

        public void Remove(int Id)
        {
            DbSlide obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbSlide.Remove(obj);
        }
    }
}

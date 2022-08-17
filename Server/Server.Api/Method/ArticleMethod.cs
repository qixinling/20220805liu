using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public class ArticleMethod : IDbModMethod<DbArticle>
    {
        public DbConnect _dbConnect { get; }

        public ArticleMethod(DbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public DbArticle Add(DbArticle dbData)
        {
            _dbConnect.DbArticle.Add(dbData);
            return dbData;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public DbArticle GetById(int Id)
        {
            DbArticle obj = _dbConnect.DbArticle.FirstOrDefault(b => b.Id == Id);
            return obj;
        }

        public List<DbArticle> GetList()
        {
            return _dbConnect.DbArticle.ToList();
        }

        public void Remove(int Id)
        {
            DbArticle obj = GetById(Id);
            if (obj == null) { return; }
            _dbConnect.DbArticle.Remove(obj);
        }
    }
}

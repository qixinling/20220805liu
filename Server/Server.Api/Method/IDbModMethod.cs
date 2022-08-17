using Server.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Method
{
    public interface IDbModMethod<T>
    {
        /// <summary>
        /// 数据上下文
        /// </summary>
        DbConnect _dbConnect { get; }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dbData">Add对象</param>
        /// <returns>Add对象</returns>
        T Add(T dbData);
        /// <summary>
        /// 伪删除,只改变数据的isDel属性,如无该字段,则不实现此接口
        /// </summary>
        /// <param name="Id">伪删除的id</param>
        void Delete(int Id);
        /// <summary>
        /// 真删除,移除数据
        /// </summary>
        /// <param name="Id"></param>
        void Remove(int Id);
        /// <summary>
        /// 根据id查询数据
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns>数据</returns>
        T GetById(int Id);
        /// <summary>
        /// 获取所有数据集合
        /// </summary>
        /// <returns>数据集</returns>
        List<T> GetList();
    }
}

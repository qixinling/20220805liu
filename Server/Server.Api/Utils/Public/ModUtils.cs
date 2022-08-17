using AutoMapper;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Server.Api.Utils.Public
{
    /// <summary>
    /// 对象操作工具类
    /// </summary>
    public static class ModUtils
    {
        /// <summary>
        /// 更新object属性值
        /// </summary>
        /// <typeparam name="TSource">来源属性类型(例如前端传来的)</typeparam>
        /// <typeparam name="TDestination">目标属性类型(例如后端查出来的)</typeparam>
        /// <param name="source">来源object(例如前端传来的)</param>
        /// <param name="destination">目标object(例如后端查出来的)</param>
        public static void ObjUpdateObj<TSource, TDestination>(object source, object destination)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            mapper.Map(source, destination);
        }

        /// <summary>
        /// 根据key查询对象中对应该key的value
        /// </summary>
        /// <param name="param">任意对象</param>
        /// <param name="keyName">key</param>
        /// <returns></returns>
        public static object GetValueByKey(object param, string keyName)
        {
            return param.GetType().GetProperty(keyName).GetValue(param, null);
        }

        /// <summary>
        /// 将对象中的指定属性初始化(移除)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="keys">要初始化的属性名</param>
        /// <returns></returns>
        public static T ModPropertyInitialize<T>(T t, string[] keys)
        {
            T newT = Activator.CreateInstance<T>();
            foreach (string key in keys)
            {
                t.GetType().GetProperty(key).SetValue(t, GetValueByKey(newT, key), null);
            }
            return t;
        }

        /// <summary>
        /// 对象转Dictionary并添加额外属性
        /// </summary>
        /// <param name="t">原对象</param>
        /// <param name="adds">需要添加的属性</param>
        /// <returns></returns>
        public static Dictionary<string, object> ModToDic<T>(T t, Dictionary<string, object> adds)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            //遍历该类属性集合
            foreach (PropertyInfo info in t.GetType().GetProperties())
            {
                //键值对添加到dic中
                dic.Add(info.Name, info.GetValue(t, null));
            }

            //添加额外字段
            foreach (KeyValuePair<string, object> kvp in adds)
            {
                if (dic.ContainsKey(kvp.Key))
                {
                    dic[kvp.Key] = kvp.Value;
                }
                else
                {
                    dic.Add(kvp.Key, kvp.Value);
                }
            }
            return dic;
        }
    }
}

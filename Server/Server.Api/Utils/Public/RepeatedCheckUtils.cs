using Microsoft.Extensions.Caching.Memory;
using System;

namespace Server.Api.Method
{
    public static class RepeatedCheckUtils
    {
        private static readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions()); //定义cache

        /// <summary>
        /// 验证重复提交
        /// 用法说明:
        /// admin提交某申请到接口,调用该方法,传入a的在整个数据库里唯一身份标识,比如userid
        /// 在Sec秒范围内如果是首次提交,证明没有重复,可以继续流程
        /// </summary>
        /// <param name="CacheToken">固定身份标识</param>
        /// <param name="Sec">保持时间</param>
        /// <returns>true重复提交,false没有重复</returns>
        public static bool Rc(string CacheToken, int Sec)
        {
            bool IsRepeated = false;
            if (CacheToken == null)
            {
                IsRepeated = true;
            }
            else
            {
                if (GetCacheValue(CacheToken) == null)
                {
                    SetChacheValue(CacheToken, "get it", Sec);
                }
                else
                {
                    IsRepeated = true;
                }
            }
            return IsRepeated;
        }

        public static object GetCacheValue(string Key)//获取值
        {
            if (Key != null && cache.TryGetValue(Key, out object Val))
            {
                return Val;
            }
            else
            {
                return default;
            }
        }

        public static void SetChacheValue(string Key, object Value, int Sec)//设置值
        {
            if (Key != null)
            {
                cache.Set(Key, Value, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Sec),  //绝对过期时间
                    //SlidingExpiration = TimeSpan.FromSeconds(Sec) //滑动过期时间,如果期间有get请求则延顺
                });
            }
        }
    }
}
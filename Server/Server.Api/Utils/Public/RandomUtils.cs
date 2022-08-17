using System;
namespace Server.Api.Method
{
    public static class RandomUtils
    {
        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string StrRandom(int Length, bool Sleep = false)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int N = Pattern.Length;
            Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int Rnd = random.Next(0, N);
                result += Pattern[Rnd];
            }
            return result;
        }

        #region 订单编号
        private static readonly object obj = new object();
        private static int GuidInt { get { return Guid.NewGuid().GetHashCode(); } }
        private static string GuidIntStr { get { return Math.Abs(GuidInt).ToString(); } }



        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="Mark">前缀</param>
        /// <param name="TimeType">时间精确类型  1 日,2 时,3 分，4 秒(默认) </param>
        /// <param name="Id">id 小于或等于0则随机生成id</param>
        /// <returns></returns>
        public static string OrderNo(string Mark, int TimeType = 4, int Id = 0)
        {
            lock (obj)
            {
                var Number = Mark;
                var Ticks = (DateTime.Now.Ticks - GuidInt).ToString();


                Number += GetTimeStr(TimeType, out int fillCount);//填充位数
                if (Id > 0)
                {
                    Number += Ticks[^(fillCount + 3)..] + Id.ToString().PadLeft(5, '0');
                }
                else
                {
                    Number += Ticks[^(fillCount + 3)..] + GuidIntStr.PadLeft(5, '0');
                }
                return Number;
            }
        }

        public static object _lock = new object();
        public static string GetRandom3()
        {
             lock(_lock)
             {
                 Random ran = new Random();
                 return  DateTime.Now.ToString("yyyyMMddHHmmssfff") + ran.Next(1000, 9999).ToString();
             }
         }

/// <summary>
/// 生成
/// </summary>
/// <param name="Mark">前缀</param>
/// <param name="TimeType">时间精确类型  1 日,2 时,3 分，4 秒(默认)</param>
/// <param name="Id">id 小于或等于0则随机生成id</param>
/// <returns></returns>
public static string GenerLong(string Mark, int TimeType = 4, long Id = 0)
        {
            lock (obj)
            {
                var Number = Mark;
                var Ticks = (DateTime.Now.Ticks - GuidInt).ToString();

                Number += GetTimeStr(TimeType, out int fillCount);//填充位数
                if (Id > 0)
                {
                    Number += Ticks[^fillCount..] + Id.ToString().PadLeft(19, '0');
                }
                else
                {
                    Number += GuidIntStr.PadLeft(10, '0') + Ticks[^(9 + fillCount)..];
                }
                return Number;
            }
        }

        /// <summary>
        /// 获取时间字符串
        /// </summary>
        /// <param name="TimeType">时间精确类型  1 日,2 时,3 分，4 秒(默认)</param>
        /// <param name="FillCount">填充位数</param>
        /// <returns></returns>
        private static string GetTimeStr(int TimeType, out int FillCount)
        {
            var Time = DateTime.Now;
            if (TimeType == 1)
            {
                FillCount = 6;
                return Time.ToString("yyyyMMdd");
            }
            else if (TimeType == 2)
            {
                FillCount = 4;
                return Time.ToString("yyyyMMddHH");
            }
            else if (TimeType == 3)
            {
                FillCount = 2;
                return Time.ToString("yyyyMMddHHmm");
            }
            else
            {
                FillCount = 0;
                return Time.ToString("yyyyMMddHHmmss");
            }
        }
        #endregion
    }
}

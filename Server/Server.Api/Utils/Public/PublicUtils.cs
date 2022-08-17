namespace Server.Api.Method
{
    public static class PublicUtils
    {
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static bool NotNull(object Str)
        {
            bool N = false;
            if (Str != null)
            {
                if (!Str.ToString().Trim().Equals(""))
                {
                    N = true;
                }
            }
            return N;
        }
    }
}
using System.Collections;
using System.Collections.Generic;

namespace Server.Models
{
    public class Result
    {
        /// <summary>
        /// 状态码
        /// -1=登录信息失效
        /// 0=执行失败
        /// 100=执行成功
        /// 500=执行异常
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 初始化构造
        /// </summary>
        public Result()
        {
            Code = 0;
        }

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="msg">消息</param>
        public Result Done(object data, string msg)
        {
            Code = 100;
            Msg = msg;
            Data = data;
            return this;
        }

        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="msg">消息</param>
        public Result Fail(string msg = "")
        {
            Code = 0;
            Msg = msg;
            return this;
        }

        /// <summary>
        /// 返回服务端异常消息
        /// </summary>
        /// <param name="msg">消息</param>
        public Result Error(string msg = "")
        {
            Code = 500;
            Msg = msg;
            return this;
        }

        
    }
}

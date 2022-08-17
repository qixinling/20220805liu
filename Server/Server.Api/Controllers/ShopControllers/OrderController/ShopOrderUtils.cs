using Newtonsoft.Json;
using Server.Models.DataBaseModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Controllers.ShopControllers.OrderController
{
    public static class ShopOrderUtils
    {
        /// <summary>
        /// 获取订单内容的方法
        /// 注意:不是接口
        /// </summary>
        /// <param name="or"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetOrderContent(DbShopOrder or)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>
            {
                { "id", or.Id.ToString() },
                { "orderNo", or.OrderNo },
                { "uid", or.Uid.ToString() },
                { "userid", or.Userid },
                { "username", or.Username },
                { "usertel", or.Usertel },
                { "sheng", or.Sheng },
                { "shi", or.Shi },
                { "xian", or.Xian },
                { "useraddress", or.Useraddress },
                { "sjine", or.Sjine.ToString() },
                { "yjine", or.Yjine.ToString() },
                { "goodslist", or.Goodslist.ToString() },
                { "goodsnum", or.Goodsnum.ToString() },
                { "odate", or.Odate.ToString() },
                { "orderstate", or.Orderstate.ToString() }
            };
            if (or.Orderstate == 0)
            {
                dic.Add("orderstatename", "待付款");
            }
            else if (or.Orderstate == 1)
            {
                dic.Add("orderstatename", "待发货");
            }
            else if (or.Orderstate == 2)
            {
                dic.Add("orderstatename", "已发货");
            }
            else if (or.Orderstate == 3)
            {
                dic.Add("orderstatename", "已收货");
            }
            else if (or.Orderstate == 4)
            {
                dic.Add("orderstatename", "已退款");
            }

            dic.Add("kuaidiname", or.Kuaidiname);
            dic.Add("kuaidiNo", or.KuaidiNo);
            dic.Add("fdate", DateTime.Now.ToString());
            dic.Add("beizhu", or.Beizhu);
            dic.Add("cost", or.Cost.ToString());


            //待发货存在一个倒计时，随着时间的推移时间差会越来越大，使用Convert.ToInt32转换时会报错
            if (or.Orderstate == 0)
            {
                DateTime lastLoginTime = or.Odate.AddMinutes(10);//数据时间
                DateTime nowTime = DateTime.Now;//现在在时间
                TimeSpan ts = lastLoginTime.Subtract(nowTime);//时间差
                int Ss = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ts.TotalMilliseconds)));
                dic.Add("timer", Ss.ToString());
            }

            ArrayList oclist = new ArrayList();
            foreach (DbShopOrderChild oc in or.DbShopOrderChild)
            {
                var o = new
                {
                    id = oc.Id,
                    orderno = oc.OrderNo,
                    oid = oc.Oid,
                    gid = oc.Gid,
                    goodsname = oc.Goodsname,
                    uid = oc.Uid,
                    userid = oc.Userid,
                    sjine = oc.Sjine,
                    yjine = oc.Yjine,
                    num = oc.Num,
                    zjine = oc.Zjine,
                    goodsimg = oc.Goodsimg,
                    cdate = oc.Cdate
                };

                oclist.Add(o);
            }

            dic.Add("oclist", JsonConvert.SerializeObject(oclist));
            dic.Add("bill", or.Bill);

            return dic;
        }
    }
}

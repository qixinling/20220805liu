using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbShopOrder
    {
        public DbShopOrder()
        {
            DbShopOrderChild = new HashSet<DbShopOrderChild>();
        }

        public int Id { get; set; }
        public int? BillId { get; set; }
        public string OrderNo { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public string Usertel { get; set; }
        public string Sheng { get; set; }
        public string Shi { get; set; }
        public string Xian { get; set; }
        public string Useraddress { get; set; }
        public decimal Sjine { get; set; }
        public decimal Yjine { get; set; }
        public decimal Pv { get; set; }
        public int Goodsnum { get; set; }
        public string Goodslist { get; set; }
        public DateTime Odate { get; set; }
        public string Kuaidiname { get; set; }
        public string KuaidiNo { get; set; }
        public DateTime? Fdate { get; set; }
        public string Beizhu { get; set; }
        public decimal Cost { get; set; }
        public int Orderstate { get; set; }
        public int Isdelete { get; set; }
        public int Lx { get; set; }
        public decimal? Zsjine { get; set; }

        public virtual DbBill Bill { get; set; }
        public virtual DbUsers UidNavigation { get; set; }
        public virtual ICollection<DbShopOrderChild> DbShopOrderChild { get; set; }
    }
}

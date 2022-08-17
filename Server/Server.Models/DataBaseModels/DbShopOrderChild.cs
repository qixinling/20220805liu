using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbShopOrderChild
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public int Oid { get; set; }
        public int Gid { get; set; }
        public string Goodsname { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public decimal Sjine { get; set; }
        public decimal Yjine { get; set; }
        public int Num { get; set; }
        public decimal Zjine { get; set; }
        public string Goodsimg { get; set; }
        public DateTime Cdate { get; set; }

        public virtual DbShopOrder OidNavigation { get; set; }
    }
}

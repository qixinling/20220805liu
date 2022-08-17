using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbShopGoodsSortChild
    {
        public int Id { get; set; }
        public int? Sid { get; set; }
        public string Xiaoleiname { get; set; }
        public int? Xiaoleiorder { get; set; }
        public int? Pagemark { get; set; }
        public string Xiaoleiimg { get; set; }
        public DateTime Adate { get; set; }
        public int Putaway { get; set; }

        public virtual DbShopGoodsSort SidNavigation { get; set; }
    }
}

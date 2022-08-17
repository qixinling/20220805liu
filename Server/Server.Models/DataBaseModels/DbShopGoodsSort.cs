using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbShopGoodsSort
    {
        public DbShopGoodsSort()
        {
            DbShopGoodsSortChild = new HashSet<DbShopGoodsSortChild>();
        }

        public int Id { get; set; }
        public string Daleiname { get; set; }
        public int? Daleiorder { get; set; }
        public int? Putaway { get; set; }
        public int? Pagemark { get; set; }
        public int? Pagemarklx { get; set; }
        public string Daleiimg { get; set; }
        public DateTime Adate { get; set; }

        public virtual ICollection<DbShopGoodsSortChild> DbShopGoodsSortChild { get; set; }
    }
}

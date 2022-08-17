using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbBill
    {
        public DbBill()
        {
            DbBillAmount = new HashSet<DbBillAmount>();
            DbShopOrder = new HashSet<DbShopOrder>();
        }

        public int Id { get; set; }
        public int? Uid { get; set; }
        public int Blx { get; set; }
        public string Bname { get; set; }
        public DateTime Bdate { get; set; }
        public string Bz { get; set; }
        public int State { get; set; }
        public int Isdel { get; set; }

        public virtual DbUsers UidNavigation { get; set; }
        public virtual ICollection<DbBillAmount> DbBillAmount { get; set; }
        public virtual ICollection<DbShopOrder> DbShopOrder { get; set; }
    }
}

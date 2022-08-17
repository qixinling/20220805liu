using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbWalletsCoin
    {
        public DbWalletsCoin()
        {
            DbBillAmount = new HashSet<DbBillAmount>();
            DbWallets = new HashSet<DbWallets>();
        }

        public int Id { get; set; }
        public string Codename { get; set; }
        public string Coinname { get; set; }
        public int State { get; set; }

        public virtual ICollection<DbBillAmount> DbBillAmount { get; set; }
        public virtual ICollection<DbWallets> DbWallets { get; set; }
    }
}

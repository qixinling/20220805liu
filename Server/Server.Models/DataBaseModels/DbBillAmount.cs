using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbBillAmount
    {
        public int Id { get; set; }
        public int? Bid { get; set; }
        public int? Cid { get; set; }
        public string Cname { get; set; }
        public decimal Amount { get; set; }
        public int State { get; set; }

        public virtual DbBill BidNavigation { get; set; }
        public virtual DbWalletsCoin CidNavigation { get; set; }
    }
}

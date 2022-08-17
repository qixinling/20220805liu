using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbWallets
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public int Cid { get; set; }
        public string Cname { get; set; }
        public string CnameZh { get; set; }
        public decimal Jine { get; set; }
        public decimal Yjine { get; set; }
        public DateTime Wdate { get; set; }

        public virtual DbWalletsCoin CidNavigation { get; set; }
        public virtual DbUsers UidNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbWalletsTixian
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public int Cid { get; set; }
        public string Codename { get; set; }
        public string Coinname { get; set; }
        public decimal Jine { get; set; }
        public decimal Shouxu { get; set; }
        public int Lx { get; set; }
        public string Bankname { get; set; }
        public string Bankcard { get; set; }
        public string Bankaddress { get; set; }
        public string Bankimg { get; set; }
        public string Usertel { get; set; }
        public DateTime Tdate { get; set; }
        public DateTime Fdate { get; set; }
        public int Ispay { get; set; }
        public int Isdelete { get; set; }
        public string Beizhu { get; set; }
        public string Chexiaoyuanyin { get; set; }

        public virtual DbUsers UidNavigation { get; set; }
    }
}

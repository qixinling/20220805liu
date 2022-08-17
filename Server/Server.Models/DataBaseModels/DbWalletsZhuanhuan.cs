using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbWalletsZhuanhuan
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public int Cid1 { get; set; }
        public string Codename1 { get; set; }
        public string Coinname1 { get; set; }
        public int Cid2 { get; set; }
        public string Codename2 { get; set; }
        public string Coinname2 { get; set; }
        public decimal Jine { get; set; }
        public int Lx { get; set; }
        public DateTime Zdate { get; set; }
        public int Isdelete { get; set; }
        public string Beizhu { get; set; }
    }
}

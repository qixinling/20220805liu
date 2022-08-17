using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbWalletsZengjian
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public int Cid { get; set; }
        public string Codename { get; set; }
        public string Coinname { get; set; }
        public int Lx { get; set; }
        public decimal Yjine { get; set; }
        public decimal Jine { get; set; }
        public decimal Xjine { get; set; }
        public string Bz { get; set; }
        public DateTime Zdate { get; set; }
        public int Isdelete { get; set; }
    }
}

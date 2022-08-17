using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbWalletsZhuanzhang
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public int Cid { get; set; }
        public string Codename { get; set; }
        public string Coinname { get; set; }
        public decimal Zqjine { get; set; }
        public decimal Zhjine { get; set; }
        public decimal Jine { get; set; }
        public int Lx { get; set; }
        public int Sid { get; set; }
        public string Suserid { get; set; }
        public string Susername { get; set; }
        public decimal Szqjine { get; set; }
        public decimal Szhjine { get; set; }
        public string Beizhu { get; set; }
        public DateTime Zdate { get; set; }
        public int Isdelete { get; set; }
    }
}

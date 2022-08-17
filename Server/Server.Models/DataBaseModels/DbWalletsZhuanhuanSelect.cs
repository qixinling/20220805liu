using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbWalletsZhuanhuanSelect
    {
        public int Id { get; set; }
        public int Cid1 { get; set; }
        public int Cid2 { get; set; }
        public string Codename1 { get; set; }
        public string Codename2 { get; set; }
        public string Coinname1 { get; set; }
        public string Coinname2 { get; set; }
        public decimal Jinemin { get; set; }
        public decimal Jinemax { get; set; }
        public int Jinebs { get; set; }
        public decimal Bili { get; set; }
    }
}

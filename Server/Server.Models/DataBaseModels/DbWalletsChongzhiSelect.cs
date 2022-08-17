using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbWalletsChongzhiSelect
    {
        public int Id { get; set; }
        public int Cid { get; set; }
        public string Codename { get; set; }
        public string Coinname { get; set; }
        public decimal Jinemin { get; set; }
        public decimal Jinemax { get; set; }
        public int Jinebs { get; set; }
    }
}

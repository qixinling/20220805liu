using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbBonusSource
    {
        public int Id { get; set; }
        public int? Btid { get; set; }
        public int Yid { get; set; }
        public string Yuserid { get; set; }
        public string Yusername { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public int Lx { get; set; }
        public decimal Jine { get; set; }
        public int Cid { get; set; }
        public string Bz { get; set; }
        public int State { get; set; }
        public DateTime Sdate { get; set; }
        public DateTime? Edate { get; set; }

        public virtual DbBonus Bt { get; set; }
    }
}

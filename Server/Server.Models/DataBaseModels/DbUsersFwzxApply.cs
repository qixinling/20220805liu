using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbUsersFwzxApply
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public int Bdlevel { get; set; }
        public decimal Jine { get; set; }
        public string Bdsheng { get; set; }
        public string Bdshi { get; set; }
        public string Bdxian { get; set; }
        public string Bdaddress { get; set; }
        public DateTime Fdate { get; set; }
        public int Ispay { get; set; }
        public DateTime? Sdate { get; set; }
        public int? Lx { get; set; }
        public string Bz { get; set; }

        public virtual DbUsers UidNavigation { get; set; }
    }
}

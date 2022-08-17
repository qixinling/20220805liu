using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbShoudan
    {
        public int Id { get; set; }
        public int Jid { get; set; }
        public string Jname { get; set; }
        public string Suserid { get; set; }
        public decimal Zprice { get; set; }
        public int? Znum { get; set; }
        public decimal Zyijia { get; set; }
        public DateTime Cdate { get; set; }
    }
}

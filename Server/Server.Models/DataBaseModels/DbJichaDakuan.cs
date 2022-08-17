using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbJichaDakuan
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public decimal Jine { get; set; }
        public int Suid { get; set; }
        public string Suserid { get; set; }
        public string Susername { get; set; }
        public string Liushuihao { get; set; }
        public string Dkimg { get; set; }
        public int State { get; set; }
        public DateTime Cdate { get; set; }
        public DateTime? Ddate { get; set; }
    }
}

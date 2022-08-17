using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbBonusJiesuan
    {
        public int Id { get; set; }
        public string Userid { get; set; }
        public int? Lx { get; set; }
        public DateTime Jdate { get; set; }
        public DateTime? Wdate { get; set; }
        public int State { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbHelp
    {
        public int Id { get; set; }
        public int? Zan { get; set; }
        public int? Cai { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int? Show { get; set; }
        public string Hpath { get; set; }
        public int? Hlevel { get; set; }
        public string Gpath { get; set; }
        public int Rank { get; set; }
    }
}

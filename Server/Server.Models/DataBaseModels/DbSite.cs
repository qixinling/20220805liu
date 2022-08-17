using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbSite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Prompt { get; set; }
        public TimeSpan? Sdate { get; set; }
        public TimeSpan? Edate { get; set; }
        public int? Ispay { get; set; }
        public int? Isyy { get; set; }
        public int? Maxnum { get; set; }
        public int? Iskf { get; set; }
    }
}

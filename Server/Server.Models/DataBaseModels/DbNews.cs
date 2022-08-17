using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbNews
    {
        public int Id { get; set; }
        public string Newscover { get; set; }
        public string Newstitle { get; set; }
        public string Newsoperator { get; set; }
        public string Newscontent { get; set; }
        public DateTime Newstime { get; set; }
        public int? Display { get; set; }
        public int? Clicks { get; set; }
    }
}

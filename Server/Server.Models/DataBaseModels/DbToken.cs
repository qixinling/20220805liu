using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbToken
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Tokenstr { get; set; }
        public string Ip { get; set; }
        public string Os { get; set; }
        public DateTime Odate { get; set; }
        public int Isa { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbSystemLog
    {
        public int Id { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public string Ip { get; set; }
        public int Lx { get; set; }
        public string LxName { get; set; }
        public string Bz { get; set; }
        public DateTime Ldate { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public int IsDel { get; set; }
        public int? Uid { get; set; }
    }
}

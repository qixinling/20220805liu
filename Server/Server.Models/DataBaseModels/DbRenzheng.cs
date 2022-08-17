using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbRenzheng
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public string Usercode { get; set; }
        public string Zimg { get; set; }
        public string Fimg { get; set; }
        public DateTime Sdate { get; set; }
        public int State { get; set; }
    }
}

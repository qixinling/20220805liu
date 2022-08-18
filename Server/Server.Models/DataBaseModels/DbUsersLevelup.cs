using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbUsersLevelup
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public int Lx { get; set; }
        public int Ylevel { get; set; }
        public int Level { get; set; }
        public decimal Jine { get; set; }
        public DateTime Sdate { get; set; }
        public int State { get; set; }

        public virtual DbUsers UidNavigation { get; set; }
    }
}

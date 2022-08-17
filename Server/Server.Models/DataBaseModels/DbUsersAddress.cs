using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbUsersAddress
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public string Usertel { get; set; }
        public string Sheng { get; set; }
        public string Shi { get; set; }
        public string Xian { get; set; }
        public string Address { get; set; }
        public int Isdefault { get; set; }
        public DateTime Odate { get; set; }
        public string AreaCode { get; set; }

        public virtual DbUsers UidNavigation { get; set; }
    }
}

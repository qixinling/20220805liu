using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbUsersBank
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public string Usertel { get; set; }
        public string Bankname { get; set; }
        public string Bankcard { get; set; }
        public string Bankaddress { get; set; }
        public int Isdefault { get; set; }
        public DateTime Bdate { get; set; }
        public string Bankimg { get; set; }

        public virtual DbUsers UidNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbSystemAdmin
    {
        public int Id { get; set; }
        public string Userid { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Logintime { get; set; }
        public string Loginip { get; set; }
        public int? Gid { get; set; }
        public string Permissionname { get; set; }
        public DateTime Adate { get; set; }

        public virtual DbSystemAdminGroup GidNavigation { get; set; }
    }
}

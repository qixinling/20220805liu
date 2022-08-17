using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbSystemAdminGroup
    {
        public DbSystemAdminGroup()
        {
            DbSystemAdmin = new HashSet<DbSystemAdmin>();
        }

        public int Id { get; set; }
        public string Groupname { get; set; }
        public string Permission { get; set; }

        public virtual ICollection<DbSystemAdmin> DbSystemAdmin { get; set; }
    }
}

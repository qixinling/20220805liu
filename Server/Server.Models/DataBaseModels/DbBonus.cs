using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbBonus
    {
        public DbBonus()
        {
            DbBonusSource = new HashSet<DbBonusSource>();
        }

        public int Id { get; set; }
        public DateTime Btdate { get; set; }

        public virtual ICollection<DbBonusSource> DbBonusSource { get; set; }
    }
}

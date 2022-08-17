using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbCheckcode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Usertel { get; set; }
        public DateTime? Cdate { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbMsg
    {
        public int Id { get; set; }
        public int? Fid { get; set; }
        public string Fuserid { get; set; }
        public int? Lx { get; set; }
        public string Title { get; set; }
        public string Msgcontent { get; set; }
        public int? Sid { get; set; }
        public string Suserid { get; set; }
        public DateTime Mdate { get; set; }
        public int? Isread { get; set; }
        public int? Sisdelete { get; set; }
    }
}

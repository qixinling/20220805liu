using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbUsersDelete
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public int Reid { get; set; }
        public string Rename { get; set; }
        public string Repath { get; set; }
        public int Relevel { get; set; }
        public int Recount { get; set; }
        public int Teamcount { get; set; }
        public DateTime Ddate { get; set; }
        public int? Deluid { get; set; }
        public string Deluserid { get; set; }
        public string Delip { get; set; }
        public int? Dellx { get; set; }
    }
}

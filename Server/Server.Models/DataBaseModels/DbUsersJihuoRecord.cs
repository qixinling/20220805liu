using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbUsersJihuoRecord
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public int Hblx { get; set; }
        public decimal Jine { get; set; }
        public int Cid { get; set; }
        public string Codename { get; set; }
        public string Coinname { get; set; }
        public int Jid { get; set; }
        public string Juserid { get; set; }
        public string Jusername { get; set; }
        public DateTime Jdate { get; set; }

        public virtual DbUsers JidNavigation { get; set; }
        public virtual DbUsers UidNavigation { get; set; }
    }
}

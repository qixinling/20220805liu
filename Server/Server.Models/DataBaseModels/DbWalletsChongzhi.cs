using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbWalletsChongzhi
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public int Cid { get; set; }
        public string Codename { get; set; }
        public string Coinname { get; set; }
        public decimal Jine { get; set; }
        public int Lx { get; set; }
        public string Usertel { get; set; }
        public string Beizhu { get; set; }
        public DateTime Cdate { get; set; }
        public DateTime Fdate { get; set; }
        public int Ispay { get; set; }
        public int Isdelete { get; set; }
        public string Czimg { get; set; }

        public virtual DbUsers UidNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbUsersFteam
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public int Fatherid { get; set; }
        public string Fathername { get; set; }
        public int Ftreeplace { get; set; }
        public string Fpath { get; set; }
        public int Flevel { get; set; }
        public int Fcount { get; set; }
        public int Teamcount { get; set; }
        public decimal Teamyeji { get; set; }
        public DateTime Fdate { get; set; }
        public decimal Area1 { get; set; }
        public decimal Area2 { get; set; }
        public decimal Area3 { get; set; }
        public decimal Area4 { get; set; }
        public decimal Area5 { get; set; }
        public decimal Yarea1 { get; set; }
        public decimal Yarea2 { get; set; }
        public decimal Yarea3 { get; set; }
        public decimal Yarea4 { get; set; }
        public decimal Yarea5 { get; set; }
        public int Narea1 { get; set; }
        public int Narea2 { get; set; }
        public int Narea3 { get; set; }
        public int Narea4 { get; set; }
        public int Narea5 { get; set; }
        public int Ch1 { get; set; }
        public int Ch2 { get; set; }
        public int Ch3 { get; set; }
        public int Ch4 { get; set; }
        public int Ch5 { get; set; }

        public virtual DbUsers UidNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbHold
    {
        public int Id { get; set; }
        public string Holdno { get; set; }
        public int? Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public string Usertel { get; set; }
        public int Jid { get; set; }
        public string Jimg { get; set; }
        public string Jname { get; set; }
        public decimal Jprice { get; set; }
        public decimal? Jsjbili { get; set; }
        public decimal? Jsybili { get; set; }
        public decimal Rishouyi { get; set; }
        public decimal Zshouyi { get; set; }
        public int? State { get; set; }
        public DateTime Hdate { get; set; }
        public string Path { get; set; }
        public int? Buid { get; set; }
        public string Buserid { get; set; }
        public string Busername { get; set; }
        public string Busertel { get; set; }
        public string Zhimg { get; set; }
        public DateTime? Dkdate { get; set; }
        public DateTime? Skdate { get; set; }
        public int Issj { get; set; }
        public int Isdelete { get; set; }
        public DateTime? Zrdate { get; set; }
        public DateTime? Edate { get; set; }
        public int? Buyissu { get; set; }
        public int? Sellissu { get; set; }
        public DateTime? Ssdate { get; set; }
        public DateTime? Zskdate { get; set; }
        public int Iscf { get; set; }
        public DateTime Zsdate { get; set; }
        public DateTime Scdate { get; set; }
        public DateTime Qgdate { get; set; }
        public int Zuid { get; set; }
        public string Zuserid { get; set; }
        public string Repath { get; set; }
        public string Sjimg { get; set; }
        public int Hsuid { get; set; }
        public int Oldhsuid { get; set; }
        public decimal Sjjine { get; set; }
        public int Isfc { get; set; }
        public string Version { get; set; }
        public DateTime Sjdate { get; set; }
        public int Isyy { get; set; }
        public int Pid { get; set; }
        public int Issd { get; set; }
    }
}

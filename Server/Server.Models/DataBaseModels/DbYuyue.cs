using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbYuyue
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public string Usertel { get; set; }
        public int Cid { get; set; }
        public string Cname { get; set; }
        public string Cimg { get; set; }
        public decimal Cminprice { get; set; }
        public decimal Cmaxprice { get; set; }
        public decimal? Cyyjindou { get; set; }
        public int? Czhouqi { get; set; }
        public decimal? Crishouri { get; set; }
        public TimeSpan? Cqdate { get; set; }
        public TimeSpan? Cedate { get; set; }
        public decimal Cjindan { get; set; }
        public int State { get; set; }
        public DateTime Ydate { get; set; }
        public int Isyy { get; set; }
        public int Isly { get; set; }
        public int Isdelete { get; set; }
        public int Iscx { get; set; }
    }
}

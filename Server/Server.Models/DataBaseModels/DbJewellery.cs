using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbJewellery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Prompt { get; set; }
        public string Img { get; set; }
        public decimal Minprice { get; set; }
        public decimal Maxprice { get; set; }
        public decimal Pjine { get; set; }
        public decimal Sjbili { get; set; }
        public decimal Sybili { get; set; }
        public int? State { get; set; }
    }
}

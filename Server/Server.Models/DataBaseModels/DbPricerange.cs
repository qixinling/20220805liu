using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbPricerange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Minprice { get; set; }
        public decimal Maxprice { get; set; }
        public decimal Minnum { get; set; }
    }
}

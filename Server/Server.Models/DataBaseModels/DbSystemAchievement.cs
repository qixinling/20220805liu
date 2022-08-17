using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbSystemAchievement
    {
        public int Id { get; set; }
        public int Userscount { get; set; }
        public decimal Usersjine { get; set; }
        public decimal Bonus { get; set; }
        public int Ordercount { get; set; }
        public int Gouwucount { get; set; }
        public decimal Gouwujine { get; set; }
        public decimal Chongzhijine { get; set; }
        public decimal Tixianjine { get; set; }
        public decimal Shangjiajine { get; set; }
        public decimal Xyzjine { get; set; }
        public decimal? Zhuanjine { get; set; }
        public DateTime Adate { get; set; }
    }
}

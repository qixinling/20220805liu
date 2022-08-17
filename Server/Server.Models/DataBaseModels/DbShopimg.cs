using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbShopImg
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public int Lx { get; set; }
        public string Bdlx { get; set; }
        public string Gid { get; set; }
    }
}

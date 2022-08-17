using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbSlide
    {
        public int Id { get; set; }
        public int Lx { get; set; }
        public int Gid { get; set; }
        public string Url { get; set; }
        public string Img { get; set; }
        public int Pagelx { get; set; }
    }
}

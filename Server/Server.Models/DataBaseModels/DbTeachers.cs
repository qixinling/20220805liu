using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Models.DataBaseModels
{
    public partial class DbTeachers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Introduce { get; set; }
        public string Tximg { get; set; }
        public int Ispay { get; set; }
        public DateTime Cdate { get; set; }
    }
}
